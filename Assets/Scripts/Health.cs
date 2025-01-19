using Unity.FPS.UI;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Unity.FPS.Game
{
    public class Health : MonoBehaviour
    {
        //用于计时
        private float gameTimer;
        public Text gameTimerText;
        private bool gameTimerStatus;

        public float MaxHealth = 10f;

        public UnityAction<float, GameObject> OnDamaged;

        public UnityAction<float> OnHealed;
        public UnityAction OnDie;

        public GameObject LoseCanvas;
        public GameObject WinCanvas;
        //封装Health
        public float CurrentHealth { get; set; }
        // Encapsulate whether props can be picked up to increase health and return a Boolean value
        public bool CanPickup() => CurrentHealth < MaxHealth;

        //Encapsulates the health value of the picked up item
        public float GetRatio() => CurrentHealth / MaxHealth;
        public int enemyCount;

        public AudioClip damageSound;
        private AudioSource audioSource;

        void Start()
        {
            CurrentHealth = MaxHealth;
            gameTimerStatus = true;
        }


        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (gameTimerStatus)
            {
                gameTimer += Time.deltaTime;
            }

            enemyCount = Resources.FindObjectsOfTypeAll<Damageable>().Length;

            if (enemyCount == 3)
            {
                WinCanvas.gameObject.SetActive(true);
            }
        }



        public void Heal(float healAmount)
        {
            
            CurrentHealth += healAmount;
           
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, MaxHealth);
        }

        
        public void TakeDamage(float damage, GameObject damageSource)
        {
            Debug.Log($"Object '{gameObject.name}' is taking {damage} damage from '{damageSource.name}'.");
            
            CurrentHealth -= damage;
            
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, MaxHealth);

            PlayDamageSound();

            HandleDeath();
        }

        private void PlayDamageSound()
        {
            if (damageSound != null && audioSource != null)
            {
                audioSource.clip = damageSound;
                audioSource.Play();
            }
        }


        void HandleDeath()
        {

            if (CurrentHealth <= 0f)
            {
                gameTimerStatus = false;

                LoseCanvas.SetActive(true);

                int min = (int)(gameTimer / 60);
                int sec = (int)(gameTimer % 60);

                if (min < 10)
                {
                    if (sec < 10)
                    {
                        gameTimerText.text = "0" + min + " : 0" + sec;
                    }
                    else
                    {
                        gameTimerText.text = "0" + min + " : " + sec;
                    }
                }
                else
                {
                    if (sec < 10)
                    {
                        gameTimerText.text = "" + min + " : 0" + sec;
                    }
                    else
                    {
                        gameTimerText.text = "" + min + " : " + sec;
                    }
                }
                return;
            }
        }
    }
}