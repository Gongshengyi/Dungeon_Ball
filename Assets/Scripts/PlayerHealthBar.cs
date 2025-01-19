using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.UI
{
    public class PlayerHealthBar : MonoBehaviour
    {
        public Image HealthFillImage;

        Health PlayerHealth;

        void Start()
        {
            PlayerCharacterController playerCharacterController = GameObject.FindObjectOfType<PlayerCharacterController>(); 

            PlayerHealth = playerCharacterController.GetComponent<Health>();

        }

        void Update()
        {
            HealthFillImage.fillAmount = PlayerHealth.CurrentHealth / PlayerHealth.MaxHealth;
        }
    }
}