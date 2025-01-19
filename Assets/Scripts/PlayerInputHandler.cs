using Unity.FPS.Game;
using UnityEngine;

namespace Unity.FPS.Gameplay
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [Tooltip("Sensitivity multiplier for moving the camera around")]
        public float LookSensitivity = 1f;

        [Tooltip("Additional sensitivity multiplier for WebGL")]
        public float WebglLookSensitivityMultiplier = 0.25f;


        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public bool CanProcessInput()
        {
            return Cursor.lockState == CursorLockMode.Locked;
        }
        //Reduce MaxMovement Speed
        public Vector3 GetMoveInput()
        {
            if (CanProcessInput())
            {
                Vector3 move = new Vector3(Input.GetAxisRaw(GameConstants.k_AxisNameHorizontal), 0f,
                    Input.GetAxisRaw(GameConstants.k_AxisNameVertical));

                move = Vector3.ClampMagnitude(move, 1);

                return move;
            }

            return Vector3.zero;
        }

        public float GetLookInputsHorizontal()
        {
            return GetMouseLookAxis(GameConstants.k_MouseAxisNameHorizontal);
        }

        public float GetLookInputsVertical()
        {
            return GetMouseLookAxis(GameConstants.k_MouseAxisNameVertical);
        }

        public bool GetFireInputHeld()
        {
            if (CanProcessInput())
            {
                return Input.GetButton(GameConstants.k_ButtonNameFire);
            }

            return false;
        }

        public bool GetAimInputHeld()
        {
            if (CanProcessInput())
            {
                return Input.GetButton(GameConstants.k_ButtonNameAim);
            }

            return false;
        }

        //WEBGL_Setting,Reducing Sensitivity
        float GetMouseLookAxis(string mouseInputName)
        {
            if (CanProcessInput())
            {
                float i = Input.GetAxisRaw(mouseInputName);

                // apply sensitivity multiplier
                i *= LookSensitivity;

                i *= 0.01f;

#if UNITY_WEBGL
                    // Mouse tends to be even more sensitive in WebGL due to mouse acceleration, so reduce it even more
                    i *= WebglLookSensitivityMultiplier;
#endif
                return i;
            }
            return 0f;
        }
    }
}