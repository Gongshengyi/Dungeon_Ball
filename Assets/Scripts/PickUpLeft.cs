using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PickUpLeft : MonoBehaviour
{
    public PlayerInputHandler inputHandler;
    public GameObject player;
    public GameObject prefabToSpawn;
    public Transform holdPos;
    public float throwForce = 500f; // Force to throw the object
    public float PickUpRange = 5f; // Pickup range
    public float rayDistance = 0.5f; // 射线的最大检测距离


    public LayerMask destroyableLayer; // 可销毁物体的层
    //public LayerMask defaultLayer;

    private GameObject heldObj; // The object being held

    private Rigidbody heldObjRb; // Rigidbody of the object being held
    //private int LayerNumber; // Layer index

    private bool isHoldingObject = false; // Whether the player is holding an object
    private bool shouldDestroyHeldObj = false;
    public AudioClip pickupSound;
    public AudioClip throwSound;
    private AudioSource audioSource;

    public Health Health;

    void Start()
    {
        //LayerNumber = LayerMask.NameToLayer("Enemy");
        LayerMask defaultLayer = LayerMask.GetMask("Default");
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    void Awake()
    {
        // Find the input handler
        inputHandler = GameObject.FindObjectOfType<PlayerInputHandler>();
    }

    void Update()
    {
        // Check if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            if (!isHoldingObject)
            {
                TryPickUpObject();
            }
            else if (shouldDestroyHeldObj)
            {
                DestroyEnemy();
            }
            else
            {
                ThrowObject();
            }
        }

        // If holding an object, keep it at the hold position
        if (isHoldingObject)
        {
            MoveObject();
        }
    }

    void TryPickUpObject()
    {
        // Perform a raycast to check if the player is looking at an object within range
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, PickUpRange))
        {
            Debug.Log("hitPoint:"+hit.transform.gameObject.name);
            if (hit.transform.gameObject.tag == "canPickUp" || hit.transform.gameObject.tag == "EnemycanPickUp" || hit.transform.gameObject.tag == "BloodBottle")
            {
                if (hit.transform.GetComponent<Rigidbody>().isKinematic)
                {
                    return;
                }
                PlayPickupSound();
                PickUpObject(hit.transform.gameObject);
            }
        }
    }
    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>())
        {
            heldObj = pickUpObj;
            heldObjRb = pickUpObj.GetComponent<Rigidbody>();
            heldObjRb.isKinematic = true;

            heldObjRb.transform.parent = holdPos.transform;
            heldObj.transform.parent = holdPos;
            //heldObj.layer = LayerNumber;
            // Ignore collision with the player
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
            // Update state to indicate the object is being held
            isHoldingObject = true;
        }
    }


    void MoveObject()
    {
        // Move the held object to the hold position
        if (heldObj != null)
        {
            if (heldObj.tag == "EnemycanPickUp" || heldObj.tag == "BloodBootle")
            {
                shouldDestroyHeldObj = true;
                heldObj.transform.position = holdPos.transform.position;
                DestroyEnemy();
            }
            else
            {
                shouldDestroyHeldObj = false;
                heldObj.transform.position = holdPos.transform.position;
            }
        }
    }

    void DestroyEnemy()
    {
        Health Health = player.GetComponent<Health>();
        if (heldObj != null)
        {
            if (heldObj.tag == "BloodBottle" && Health.CanPickup() && Health != null)
            {
                Health.Heal(60);
            }

            Debug.Log("即将销毁的对象名字是: " + heldObj.name);
            Destroy(heldObj);
            heldObj = null;
        }

        GameObject newObj = Instantiate(prefabToSpawn, holdPos.position, Quaternion.identity);
        Rigidbody newRb = newObj.GetComponent<Rigidbody>();
        if (newRb == null)
        {
            newRb = newObj.AddComponent<Rigidbody>();
        }
        newRb.isKinematic = true;

        //newObj.layer = LayerNumber;
        newObj.transform.parent = holdPos;
        newObj.transform.parent = null;

        heldObj = newObj;
        heldObjRb = newRb;
        isHoldingObject = true;
        shouldDestroyHeldObj = false;
    }

    void ThrowObject()
    {
        // Ensure there is an object being held before throwing
        if (heldObj != null && isHoldingObject && !shouldDestroyHeldObj)
        {
            PlayThrowSound();
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
            heldObj.layer = 6;
            heldObjRb.isKinematic = false;
            heldObj.transform.parent = null;

            heldObjRb.AddForce(transform.forward * throwForce);
            heldObj.transform.forward = transform.forward;
            heldObj.GetComponent<selfDestruction>().startDetection = true;
            heldObj.GetComponent<selfDestruction>().isUsed= true;
            heldObj.GetComponent<selfDestruction>().playerObj=this.transform.parent.gameObject;
            if (heldObj.CompareTag("BloodBottle"))
            {
                HealPlayer();
            }
            // Clear held object and update state
            heldObj = null;
            isHoldingObject = false;
        }
    }

    private void HealPlayer()
    {
        if (player != null)
        {
            // Get the Health component from the Player object
            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth != null)
            {
                // Heal the player by a certain amount (example: 50)
                playerHealth.Heal(50); // Adjust the amount as needed
            }
        }
    }

    private void PlayPickupSound()
    {
        if (pickupSound != null && audioSource != null)
        {
            audioSource.clip = pickupSound;
            audioSource.Play();
        }
    }

    private void PlayThrowSound()
    {
        if (throwSound != null && audioSource != null)
        {
            audioSource.clip = throwSound;
            audioSource.Play();
        }
    }
}