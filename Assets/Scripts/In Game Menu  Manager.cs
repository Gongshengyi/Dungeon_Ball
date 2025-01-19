using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Unity.FPS.UI
{
    public class InGameMenuManager : MonoBehaviour
    {
        [Tooltip("Root GameObject of the menu used to toggle its activation")]
        public GameObject MenuRoot;

        [Tooltip("Master volume when menu is open")]
        [Range(0.001f, 1f)]
        public float VolumeWhenMenuOpen = 0.5f;

        [Tooltip("Slider component for look sensitivity")]
        public Slider LookSensitivitySlider;

        PlayerInputHandler m_PlayerInputsHandler;

        public GameObject LoseCanvas;

        public GameObject WinCanvas;

        void Start()
        {
            m_PlayerInputsHandler = FindObjectOfType<PlayerInputHandler>();


            MenuRoot.SetActive(false);


            LookSensitivitySlider.value = m_PlayerInputsHandler.LookSensitivity;
            LookSensitivitySlider.onValueChanged.AddListener(OnMouseSensitivityChanged);

        }

        void Update()
        {
            // Lock cursor when clicking outside of menu
            if (!MenuRoot.activeSelf && Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            if (LoseCanvas != null && LoseCanvas.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                SetLoseCanvasActivation(LoseCanvas.activeSelf);
            }

            if (WinCanvas.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if (Input.GetButtonDown(GameConstants.k_ButtonNamePauseMenu))
            {

                SetPauseMenuActivation(!MenuRoot.activeSelf);

            }

            if (Input.GetAxisRaw(GameConstants.k_AxisNameVertical) != 0)
            {
                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                    LookSensitivitySlider.Select();
                }
            }
        }
        void SetLoseCanvasActivation(bool active)
        {
            LoseCanvas.SetActive(active);
            if (active)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f;
            }
        }

        void SetPauseMenuActivation(bool active)
        {
            MenuRoot.SetActive(active);

            if (active)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1f;
            }
        }

        void OnMouseSensitivityChanged(float newValue)
        {
            m_PlayerInputsHandler.LookSensitivity = newValue;
        }
    }
}