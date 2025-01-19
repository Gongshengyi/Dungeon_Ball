using System;
using UnityEngine;

namespace Unity.FPS.Game
{
    public class Damageable : MonoBehaviour
    {
        public float DamageMultiplier = 1f;

        public Health Health { get; private set; }

        void Awake()
        {
            Health = GetComponent<Health>();
            if (!Health)
            {
                Health = GetComponentInParent<Health>();
            }
        }

        public void InflictDamage(float damage, bool isExplosionDamage, GameObject damageSource)
        {
            if (Health)
            {
                var totalDamage = damage;

                if (!isExplosionDamage)
                {
                    totalDamage *= DamageMultiplier;
                    
                }

                Health.TakeDamage(totalDamage, damageSource);

                // Optionally, log the remaining health after damage is taken
                Debug.Log($"After damage, {gameObject.name} has {Health.CurrentHealth}/{Health.MaxHealth} health left.");
            }
        }
    }
}