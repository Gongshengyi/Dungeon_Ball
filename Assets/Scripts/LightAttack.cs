using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;

public class LightAttack : MonoBehaviour
{
    [SerializeField] private LayerMask hitLayerMask;
    [SerializeField] private Vector3 halfExtents;
    [SerializeField] float damage = 10f;
    private HashSet<Damageable> processedHits = new HashSet<Damageable>();
    private static Collider[] buffer = new Collider[3];
    private LightAttack actor;
    private EnemyAI enemyAI;
    //Health
    public Health Health { get; private set; }

    void Start()
    {
        actor = GetComponent<LightAttack>();
        enemyAI = GetComponentInParent<EnemyAI>();
    }

    // Logic for handling attacks on Player by enemies within attack range
    public void HandleSwing()
    {
        int hits = Physics.OverlapBoxNonAlloc(transform.position, halfExtents, buffer, transform.rotation, hitLayerMask, QueryTriggerInteraction.Ignore);
        if (hits == 0)
            return;

        for (int i = 0; i < hits; i++)
            DamageCollider(buffer[i]);

        processedHits.Clear();
    }


    private void DamageCollider(Collider collider)
    {
        
        if (collider.gameObject == actor.gameObject || (enemyAI != null && collider.gameObject == enemyAI.gameObject))
        {
            return;
        }
        Damageable damageable = collider.GetComponent<Damageable>();
        if (damageable == null)
        {
            return;
        }
        if (processedHits.Contains(damageable))
        {
            return;
        }
        processedHits.Add(damageable);

        Debug.Log("Inflicting damage on " + damageable.gameObject.name + " with " + damage + " damage.");

        damageable.InflictDamage(damage, false, enemyAI.gameObject);
    }
}