using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBoss : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float turnSpeed = 5;
    bool isScanning = false;

    public NavMeshAgent navMashAgent;

    float distanceToTarget = Mathf.Infinity;
    [SerializeField] LightAttack lightAttack;

    public Transform nextGrabObj;
    public GameObject[] grabObjs;
    public bool readyShot;

    bool findFirst;
    private float ShotTimer;
    private float refindTimer;
    
    void Start()
    {
        navMashAgent = GetComponent<NavMeshAgent>();
    
    }


    void Update()
    {
        if (distanceToTarget <= 15f)
        {

            if (nextGrabObj == null)
            {
                if (!findFirst)
                {
                findFirst = true;
                getNextGrabObjs();
                }
                else
                {
                    refindTimer += Time.deltaTime;
                    if (refindTimer >= 2f)
                    {
                         getNextGrabObjs();
                          refindTimer = 0f;
                    }
                }
            }
            
            if (nextGrabObj != null)
            {
                if (Vector3.Distance(this.transform.position, nextGrabObj.transform.position) < 2f && !readyShot)
                {
                    if (!SetGrabObj(nextGrabObj.transform))
                    {
                        nextGrabObj = null;
                    }
                }
                if (readyShot)
                {
                    FaceTarget();
                    ShotTimer += Time.deltaTime;
                    if (ShotTimer >= 2f)
                    {
                        SetGrabShot(nextGrabObj.transform);
                        ShotTimer = 0;
                        Invoke("getNextGrabObjs", 0.5f);
                    }
                    if (!navMashAgent.isStopped)
                    {
                        navMashAgent.isStopped = true;
                    }
                }
                else
                {
                    if (navMashAgent.isStopped)
                    {
                        navMashAgent.isStopped = false;
                    }
                    navMashAgent.SetDestination(nextGrabObj.position);
                }
            }
            else if (isScanning)
            {
                lightAttack.HandleSwing();
                isScanning = false;
                EngageTarget();
            }

        }
        else
        {
      
        }

        distanceToTarget = Vector3.Distance(target.position, transform.position);

    }


    void getNextGrabObjs()
    {
       // Debug.Log("GET GRAB");
        grabObjs = GameObject.FindGameObjectsWithTag("canPickUp");
        float minDistance = int.MaxValue;
        int minIndex = 0;
        for (int i = 0; i < grabObjs.Length; i++)
        {
            if (Vector3.Distance(this.transform.position, grabObjs[i].transform.position) < minDistance)
            {
                minDistance = Vector3.Distance(this.transform.position, grabObjs[i].transform.position);
                minIndex = i;
            }
        }
        if (grabObjs.Length != 0)
        {
            nextGrabObj = grabObjs[minIndex].transform;

        }
        else
        {
            nextGrabObj = null;
        }

    }
    private void EngageTarget()
    {
        FaceTarget();

        if (distanceToTarget >= navMashAgent.stoppingDistance)
        {
            ChaseTarget();
        }
    }

    private void ChaseTarget()
    {
        navMashAgent.SetDestination(target.position);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private bool SetGrabObj(Transform targetTrans)
    {
        if (targetTrans.GetComponent<Rigidbody>().isKinematic)
        {
            return false;
        }
        targetTrans.transform.parent = this.transform.GetChild(0).transform;
        targetTrans.localPosition = Vector3.zero;
        targetTrans.GetComponent<Rigidbody>().isKinematic = true;
        readyShot = true;
        return true;
    }
    private void SetGrabShot(Transform targetTrans)
    {
        nextGrabObj = null;
        targetTrans.transform.parent = null;
        //targetTrans.GetComponent<selfDestruction>().ClearUseStatus();
        targetTrans.GetComponent<selfDestruction>().isEnemyUsed = true;
        targetTrans.GetComponent<selfDestruction>().enemyObj = this.gameObject;
        
        targetTrans.GetComponent<Rigidbody>().isKinematic = false;
        targetTrans.GetComponent<Rigidbody>().AddForce(transform.forward * 2000f);
        readyShot = false;
    }
}
