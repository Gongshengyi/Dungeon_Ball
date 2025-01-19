using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.FPS.Game;

public class EnemyAI : MonoBehaviour
{
    // target to catch
    public Animator anim;
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 10;
    [SerializeField] float sight = 15;
    [SerializeField] float leapRange = 6;
    [SerializeField] float turnSpeed = 5;
    bool isScanning = false;
    public bool isChasing = true;

    public NavMeshAgent navMashAgent;

    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    Health health;
    [SerializeField] LightAttack lightAttack;

    public float speed;

    public float startWaitTime;


    void Start()
    {
        navMashAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (isScanning)
        {
            lightAttack.HandleSwing();
            isScanning = false;
        }
        ChaseTarget();

        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (distanceToTarget > sight)
        {
            isProvoked = false;
            anim.SetBool("attack", false);
        }
        if (isProvoked)
        {
            anim.SetBool("idle", false);
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
            anim.SetBool("idle", false);
        }
        else if (!isProvoked)
        {
            Patrol();
        }

    }
    private void Patrol()
    {
        anim.SetBool("idle", false);
    }
    private void EngageTarget()
    {
        FaceTarget();
        if (distanceToTarget < leapRange && distanceToTarget > navMashAgent.stoppingDistance + 2 && !anim.GetBool("attack"))
        {
            Leap();
        }
        else if (distanceToTarget >= navMashAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        else if (distanceToTarget < navMashAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void AttackTarget()
    {
        anim.SetBool("leap", false);
        anim.SetBool("attack", true);
    }
    private void Leap()
    {
        anim.SetBool("leap", true);
        anim.SetBool("attack", true);
    }
    private void ChaseTarget()
    {
        anim.SetBool("attack", false);
        anim.SetBool("leap", false);
        navMashAgent.SetDestination(target.position);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    void OnDrawGizmosSelected()
    {
        // ÊÓÒ°·¶Î§
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sight);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    public void AttackStarted()
    {
        isScanning = true;
    }

    public void AttackEnded()
    {
        isScanning = false;
    }
}
