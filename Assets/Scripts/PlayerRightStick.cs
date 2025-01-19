using Unity.FPS.Gameplay;
using UnityEngine;

namespace Unity.FPS.Game
{
    public class PlayerRightStick : MonoBehaviour
    {
        private static int IsRightThrowingHash = Animator.StringToHash("isRightThrowing");

        [SerializeField] private Animator animator;

        public bool IsRightThrowing => isRightThrowing;

        public PlayerInputHandler inputHandler;
        private bool RightthrowInputHeld;
        private bool isRightThrowing;

        void Awake()    
        {
            // Get the input handler component from the player
            inputHandler = GameObject.FindObjectOfType<PlayerInputHandler>();
        }

        private void Update()
        {
            // Recycle aim input for throw
            RightthrowInputHeld = inputHandler.GetAimInputHeld();

            // Update block state and trigger animations
            HandleInput(RightthrowInputHeld);
        }

        private void HandleInput(bool RightthrowInputHeld)
        {
            // Update the animator parameter
            animator.SetBool(IsRightThrowingHash, RightthrowInputHeld);

            // Update blocking state
            if (RightthrowInputHeld)
            {
                StartRightThrowEvent();
            }
            else
            {
                EndRightThrowEvent();
            }
        }

        private void StartRightThrowEvent()
        {
            // Enable blocking
            isRightThrowing = false;
        }

        private void EndRightThrowEvent()
        {
            // Disable blocking
            isRightThrowing = true;
        }
    }
}
