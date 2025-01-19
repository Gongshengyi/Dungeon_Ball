using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

public class selfDestruction : MonoBehaviour
{
    // Start is called before the first frame update
    public LayerMask destroyableLayer; 
    public LayerMask defaultLayer;
    public float rayDistance; 
    public bool destroySelf = false; 
    public bool destroyCollider = false; 
    public bool startDetection = false; 
    public bool isUsed;
    public bool isEnemyUsed;
    public GameObject enemyObj;
    public GameObject playerObj;
    private AudioSource audioSource;
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!startDetection)
        {
            return;
        }
    }
    public void ClearUseStatus()
    {
        isEnemyUsed = false;
        isUsed = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isUsed)
        {
            if (collision.transform.gameObject != playerObj.gameObject)
            {
                if (collision.gameObject.layer == 6 || collision.gameObject.layer == 7)
                {
                    if (destroyCollider)
                    {
                        InitParticle();
                        Destroy(collision.transform.gameObject, 0.1f);
                    }
                }
                if (collision.gameObject.layer == 0)
                {
                    if (destroySelf)
                    {
                        Debug.Log("碰撞物体销毁:" + collision.transform.name);
                        InitParticle();
                        Destroy(gameObject, 0.01f);
                    }
                }
            }
            return;
        }

        if (isEnemyUsed)
        {
            if (collision.transform.gameObject != enemyObj.gameObject)
            {
                if (collision.gameObject.layer == 0)
                {
                    if (destroySelf)
                    {
                        Debug.Log("碰撞物体销毁:"+collision.transform.name);
                        InitParticle();
                        Destroy(gameObject,0.01f);
                    }
                }
            }
        }
    }
    private void InitParticle()
    {
        GameObject soundObj = new GameObject("ExplosionSoundObj");
        AudioSource audioSource = soundObj.AddComponent<AudioSource>();
        audioSource.clip = ResourceManager.Instance.ExplosionSound;
        audioSource.volume = 1.0f; 
        audioSource.loop = false;  

        audioSource.Play();

        GameObject newParticle = Instantiate(ResourceManager.Instance.destroyParticle, this.transform);
        newParticle.transform.localPosition = Vector3.zero;
        newParticle.transform.SetParent(null, true);
    }


    
}