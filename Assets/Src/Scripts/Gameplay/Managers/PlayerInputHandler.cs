using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.UI;
namespace Unity.FPS.Gameplay {
    public class PlayerInputHandler : MonoBehaviour
    {
        [Tooltip("Sensitivity multiplier for moving the camera around")]
        public float LookSensitivity = 1f;

        [Tooltip("Limit to consider an input when using a trigger on a controller")]
        public float TriggerAxisThreshold = 0.4f;

        [Tooltip("Used to flip the vertical input axis")]
        public bool InvertYAxis = false;

        [Tooltip("Used to flip the horizontal input axis")]
        public bool InvertXAxis = false;

        GameFlowManager m_GameFlowManager;
        PlayerCharacterController playerPhysx;
        bool m_TirInputWasHeld;
        bool m_TirObliqueInputWasHeld;
        private bool canMove;
        private bool canJump;
        private bool canEat;
        private bool canCollect;
        private bool canOpenInventaire;
        void Start()
        {
            playerPhysx = GetComponent<PlayerCharacterController>();
            DebugUtility.HandleErrorIfNullGetComponent<PlayerCharacterController, PlayerInputHandler>(
                playerPhysx, this, gameObject);
            m_GameFlowManager = FindObjectOfType<GameFlowManager>();
            DebugUtility.HandleErrorIfNullFindObject<GameFlowManager, PlayerInputHandler>(m_GameFlowManager, this);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            canMove = true;
            canJump = true;
            canOpenInventaire = true;
            canCollect = true;
            canEat = true;

        }

        void LateUpdate()
        {
            m_TirInputWasHeld = GetTirInputHeld();
            m_TirObliqueInputWasHeld = GetTirObliqueInputHeld();
        }

        public bool CanProcessInput()
        {
            return Cursor.lockState == CursorLockMode.Locked && !m_GameFlowManager.GameIsEnding;
        }

        public Vector3 GetMoveInput()
        {
            if (CanProcessInput() && canMove)
            {
                Vector3 move = new Vector3(Input.GetAxisRaw(GameConstants.k_AxisNameHorizontal), 0f,
                    Input.GetAxisRaw(GameConstants.k_AxisNameVertical));

                // constrain move input to a maximum magnitude of 1, otherwise diagonal movement might exceed the max move speed defined
                move = Vector3.ClampMagnitude(move, 1);

                return move;
            }

            return Vector3.zero;
        }
       
        public float GetLookInputsHorizontal()
        {
            return GetMouseOrStickLookAxis(GameConstants.k_MouseAxisNameHorizontal,
                GameConstants.k_AxisNameJoystickLookHorizontal);
        }

        public float GetLookInputsVertical()
        {
            return GetMouseOrStickLookAxis(GameConstants.k_MouseAxisNameVertical,
                GameConstants.k_AxisNameJoystickLookVertical);
        }

        public bool GetJumpInputDown()
        {
            if (CanProcessInput() && canJump)
            {
                return Input.GetButtonDown(GameConstants.k_ButtonNameJump);
            }

            return false;
        }

        public bool GetCollectInputDown()
        {
            if (CanProcessInput())
            {
                return Input.GetButtonDown(GameConstants.k_ButtonNameCollect);
            }

            return false;
        }

        public bool GetEatInputDown()
        {
            if (CanProcessInput() && canEat)
            {
                return Input.GetButtonDown(GameConstants.k_ButtonNameEat);
            }

            return false;
        }


        public bool GetInventaireInputDown()
        {
            if (CanProcessInput() && canOpenInventaire)
            {
                return Input.GetButtonDown(GameConstants.k_ButtonNameInventaire);
            }

            return false;
        }

        public bool GetJumpInputHeld()
        {
            if (CanProcessInput() && canJump)
            {
                return Input.GetButton(GameConstants.k_ButtonNameJump);
            }

            return false;
        }

        public bool GetTirInputDown()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                return GetTirInputHeld() && !m_TirInputWasHeld;
            }
            else if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                return GetTirObliqueInputHeld() && !m_TirObliqueInputWasHeld;
            }
            return false;
        }
        public bool GetTirInputHeld()
        {
            if (CanProcessInput())
            {
                bool isGamepad = Input.GetAxis(GameConstants.k_ButtonNameGamepadFire) != 0f;
                if (isGamepad)
                {
                    return Input.GetAxis(GameConstants.k_ButtonNameGamepadFire) >= TriggerAxisThreshold;
                }
                else
                {
                    return Input.GetButton(GameConstants.k_ButtonNameTir);
                }
            }

            return false;
        }
        public bool GetTirObliqueInputHeld()
        {
            if (CanProcessInput())
            {
                bool isGamepad = Input.GetAxis(GameConstants.k_ButtonNameGamepadAim) != 0f;
                if (isGamepad)
                {
                    return Input.GetAxis(GameConstants.k_ButtonNameGamepadAim) >= TriggerAxisThreshold;
                }

                else
                {
                    return Input.GetButton(GameConstants.k_ButtonNameAim);
                }
            }

            return false;
        }

        public bool GetAimInputHeld()
        {
            

            return false;
        }

        public bool GetSprintInputHeld()
        {
            if (CanProcessInput())
            {
                return Input.GetButton(GameConstants.k_ButtonNameSprint);
            }

            return false;
        }

        public bool GetReloadButtonDown()
        {
            if (CanProcessInput())
            {
                return Input.GetButtonDown(GameConstants.k_ButtonReload);
            }

            return false;
        }

        public int GetSwitchWeaponInput()
        {
            if (CanProcessInput())
            {

                bool isGamepad = Input.GetAxis(GameConstants.k_ButtonNameGamepadSwitchWeapon) != 0f;
                string axisName = isGamepad
                    ? GameConstants.k_ButtonNameGamepadSwitchWeapon
                    : GameConstants.k_ButtonNameSwitchWeapon;

                if (Input.GetAxis(axisName) > 0f)
                    return -1;
                else if (Input.GetAxis(axisName) < 0f)
                    return 1;
                else if (Input.GetAxis(GameConstants.k_ButtonNameNextWeapon) > 0f)
                    return -1;
                else if (Input.GetAxis(GameConstants.k_ButtonNameNextWeapon) < 0f)
                    return 1;
            }

            return 0;
        }

        public int GetSelectWeaponInput()
        {
            if (CanProcessInput())
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                    return 1;
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                    return 2;
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                    return 3;
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                    return 4;
                else if (Input.GetKeyDown(KeyCode.Alpha5))
                    return 5;
                else if (Input.GetKeyDown(KeyCode.Alpha6))
                    return 6;
                else if (Input.GetKeyDown(KeyCode.Alpha7))
                    return 7;
                else if (Input.GetKeyDown(KeyCode.Alpha8))
                    return 8;
                else if (Input.GetKeyDown(KeyCode.Alpha9))
                    return 9;
                else
                    return 0;
            }

            return 0;
        }
        public bool GetCanOpenInventaire()
        {
            return canOpenInventaire;
        }
        float GetMouseOrStickLookAxis(string mouseInputName, string stickInputName)
        {
            if (CanProcessInput())
            {
                // Check if this look input is coming from the mouse
                bool isGamepad = Input.GetAxis(stickInputName) != 0f;
                float i = isGamepad ? Input.GetAxis(stickInputName) : Input.GetAxisRaw(mouseInputName);

                // handle inverting vertical input
                if (InvertYAxis)
                    i *= -1f;

                // apply sensitivity multiplier
                i *= LookSensitivity;

                if (isGamepad)
                {
                    // since mouse input is already deltaTime-dependant, only scale input with frame time if it's coming from sticks
                    i *= Time.deltaTime;
                }
                else
                {
                    // reduce mouse input amount to be equivalent to stick movement
                    i *= 0.01f;
#if UNITY_WEBGL
                    // Mouse tends to be even more sensitive in WebGL due to mouse acceleration, so reduce it even more
                    i *= WebglLookSensitivityMultiplier;
#endif
                }

                return i;
            }

            return 0f;
        }
    }
}