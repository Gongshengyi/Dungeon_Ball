using Unity.FPS.Gameplay;
using UnityEngine;

namespace Unity.FPS.Game
{
    public class PlayerLeftStick : MonoBehaviour
    {
        private static int IsLeftThrowingHash = Animator.StringToHash("isLeftThrowing");

        [SerializeField] private Animator animator;

        public bool IsLeftThrowing => isLeftThrowing;

        public PlayerInputHandler inputHandler;
        private bool LeftthrowInputHeld;
        private bool isLeftThrowing;

        void Awake()
        {
            // Get the input handler component from the player
            inputHandler = GameObject.FindObjectOfType<PlayerInputHandler>();
        }

        private void Update()
        {
            // Recycle aim input for throw
            LeftthrowInputHeld = inputHandler.GetFireInputHeld();

            // Update block state and trigger animations
            HandleInput(LeftthrowInputHeld);
        }

        private void HandleInput(bool LeftthrowInputHeld)
        {
            // Update the animator parameter
            animator.SetBool(IsLeftThrowingHash, LeftthrowInputHeld);

            // Update blocking state
            if (LeftthrowInputHeld)
            {
                StartLeftThrowEvent();
            }
            else
            {
                EndLeftThrowEvent();
            }
        }

        private void StartLeftThrowEvent()
        {
            // Enable blocking
            isLeftThrowing = true;
        }

        private void EndLeftThrowEvent()
        {
            // Disable blocking
            isLeftThrowing = false;
        }
    }
}
