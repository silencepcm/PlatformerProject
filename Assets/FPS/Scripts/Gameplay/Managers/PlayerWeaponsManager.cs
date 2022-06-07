using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.Events;

namespace Unity.FPS.Gameplay
{
    public class PlayerWeaponsManager : MonoBehaviour
    {
        public enum WeaponSwitchState
        {
            Up,
            Down,
            PutDownPrevious,
            PutUpNew,
        }

        [Header("References")] [Tooltip("Secondary camera used to avoid seeing weapon go throw geometries")]
        public Camera WeaponCamera;

        [Tooltip("Parent transform where all weapon will be added in the hierarchy")]
        public Transform WeaponParentSocket;

        [Tooltip("Position for weapons when active but not actively aiming")]
        public Transform DefaultWeaponPosition;

        [Tooltip("Position for weapons when aiming")]
        public Transform AimingWeaponPosition;

        [Tooltip("Position for innactive weapons")]
        public Transform DownWeaponPosition;

        [Header("Weapon Bob")]
        [Tooltip("Frequency at which the weapon will move around in the screen when the player is in movement")]
        public float BobFrequency = 10f;

        [Tooltip("How fast the weapon bob is applied, the bigger value the fastest")]
        public float BobSharpness = 10f;

        [Tooltip("Distance the weapon bobs when not aiming")]
        public float DefaultBobAmount = 0.05f;

        [Tooltip("Distance the weapon bobs when aiming")]
        public float AimingBobAmount = 0.02f;

        [Header("Weapon Recoil")]
        [Tooltip("This will affect how fast the recoil moves the weapon, the bigger the value, the fastest")]
        public float RecoilSharpness = 50f;

        [Tooltip("Maximum distance the recoil can affect the weapon")]
        public float MaxRecoilDistance = 0.5f;

        [Tooltip("How fast the weapon goes back to it's original position after the recoil is finished")]
        public float RecoilRestitutionSharpness = 10f;

        [Header("Misc")] [Tooltip("Speed at which the aiming animatoin is played")]
        public float AimingAnimationSpeed = 10f;

        [Tooltip("Field of view when not aiming")]
        public float DefaultFov = 60f;

        [Tooltip("Portion of the regular FOV to apply to the weapon camera")]
        public float WeaponFovMultiplier = 1f;

        [Tooltip("Delay before switching weapon a second time, to avoid recieving multiple inputs from mouse wheel")]
        public float WeaponSwitchDelay = 1f;


        public bool IsAiming { get; private set; }
        public bool IsPointingAtEnemy { get; private set; }
        public int ActiveWeaponIndex { get; private set; }

        public WeaponController Weapon;
        public WeaponController WeaponDirect;
        PlayerInputHandler m_InputHandler;
        PlayerCharacterController m_PlayerCharacterController;
        float m_WeaponBobFactor;
        Vector3 m_LastCharacterPosition;
        Vector3 m_WeaponMainLocalPosition;
        Vector3 m_WeaponBobLocalPosition;
        Vector3 m_WeaponRecoilLocalPosition;
        Vector3 m_AccumulatedRecoil;
        WeaponSwitchState m_WeaponSwitchState;

        void Start()
        {
            ActiveWeaponIndex = -1;
            m_WeaponSwitchState = WeaponSwitchState.Up;

            m_InputHandler = GetComponent<PlayerInputHandler>();

            m_PlayerCharacterController = GetComponent<PlayerCharacterController>();

            SetFov(DefaultFov);
            Weapon.Owner = gameObject;
        }

        void Update()
        {
            // shoot handling

            if (Weapon != null && Weapon.IsReloading)
                return;
            if (Weapon != null && m_WeaponSwitchState == WeaponSwitchState.Up)
            {
                if (m_InputHandler.GetReloadButtonDown() && Weapon.GetCurrentAmmoDirect()/Weapon.MaxAmmo < 1.0f)
                {
                    IsAiming = false;
                    Weapon.StartReloadAnimation();
                    return;
                }
                // handle shooting
                bool hasFired = Weapon.HandleShootInputs(
                    m_InputHandler.GetTirInputDown());
                   

                // Handle accumulating recoil
                if (hasFired)
                {
                    m_AccumulatedRecoil += Vector3.back * Weapon.RecoilForce;
                    m_AccumulatedRecoil = Vector3.ClampMagnitude(m_AccumulatedRecoil, MaxRecoilDistance);
                }
            }


            // Pointing at enemy handling
            IsPointingAtEnemy = false;
            if (Weapon)
            {
                if (Physics.Raycast(WeaponCamera.transform.position, WeaponCamera.transform.forward, out RaycastHit hit,
                    1000, -1, QueryTriggerInteraction.Ignore))
                {
                    if (hit.collider.tag == "Enemy")
                    {
                        IsPointingAtEnemy = true;
                    }
                }
            }
        }


        // Update various animated features in LateUpdate because it needs to override the animated arm position
        void LateUpdate()
        {
            //UpdateWeaponAiming();
            UpdateWeaponBob();
            UpdateWeaponRecoil();
            
            // Set final weapon socket position based on all the combined animation influences
            WeaponParentSocket.localPosition =
                m_WeaponMainLocalPosition + m_WeaponBobLocalPosition + m_WeaponRecoilLocalPosition;
        }

        // Sets the FOV of the main camera and the weapon camera simultaneously
        public void SetFov(float fov)
        {
            m_PlayerCharacterController.PlayerCamera.fieldOfView = fov;
            WeaponCamera.fieldOfView = fov * WeaponFovMultiplier;
        }

        

        // Updates weapon position and camera FoV for the aiming transition
        void UpdateWeaponAiming()
        {
            if (m_WeaponSwitchState == WeaponSwitchState.Up)
            {
                if (IsAiming && Weapon)
                {
                    Weapon.transform.position = Vector3.Lerp(Weapon.transform.position,
                        AimingWeaponPosition.position + Weapon.AimOffset,
                        AimingAnimationSpeed * Time.deltaTime);
                    SetFov(Mathf.Lerp(m_PlayerCharacterController.PlayerCamera.fieldOfView,
                        Weapon.AimZoomRatio * DefaultFov, AimingAnimationSpeed * Time.deltaTime));
                }
                else
                {
                    Weapon.transform.position = Vector3.Lerp(Weapon.transform.position,
                        DefaultWeaponPosition.position, AimingAnimationSpeed * Time.deltaTime);
                    SetFov(Mathf.Lerp(m_PlayerCharacterController.PlayerCamera.fieldOfView, DefaultFov,
                        AimingAnimationSpeed * Time.deltaTime));
                }
            }
        }

        // Updates the weapon bob animation based on character speed
        void UpdateWeaponBob()
        {
            if (Time.deltaTime > 0f)
            {
                Vector3 playerCharacterVelocity =
                    (m_PlayerCharacterController.transform.position - m_LastCharacterPosition) / Time.deltaTime;

                // calculate a smoothed weapon bob amount based on how close to our max grounded movement velocity we are
                float characterMovementFactor = 0f;
                if (m_PlayerCharacterController.IsGrounded)
                {
                    characterMovementFactor =
                        Mathf.Clamp01(playerCharacterVelocity.magnitude /
                                      (m_PlayerCharacterController.MaxSpeedOnGround *
                                       m_PlayerCharacterController.SprintSpeedModifier));
                }

                m_WeaponBobFactor =
                    Mathf.Lerp(m_WeaponBobFactor, characterMovementFactor, BobSharpness * Time.deltaTime);

                // Calculate vertical and horizontal weapon bob values based on a sine function
                float bobAmount = IsAiming ? AimingBobAmount : DefaultBobAmount;
                float frequency = BobFrequency;
                float hBobValue = Mathf.Sin(Time.time * frequency) * bobAmount * m_WeaponBobFactor;
                float vBobValue = ((Mathf.Sin(Time.time * frequency * 2f) * 0.5f) + 0.5f) * bobAmount *
                                  m_WeaponBobFactor;

                // Apply weapon bob
                m_WeaponBobLocalPosition.x = hBobValue;
                m_WeaponBobLocalPosition.y = Mathf.Abs(vBobValue);

                m_LastCharacterPosition = m_PlayerCharacterController.transform.position;
            }
        }

        // Updates the weapon recoil animation
        void UpdateWeaponRecoil()
        {
            // if the accumulated recoil is further away from the current position, make the current position move towards the recoil target
            if (m_WeaponRecoilLocalPosition.z >= m_AccumulatedRecoil.z * 0.99f)
            {
                m_WeaponRecoilLocalPosition = Vector3.Lerp(m_WeaponRecoilLocalPosition, m_AccumulatedRecoil,
                    RecoilSharpness * Time.deltaTime);
            }
            // otherwise, move recoil position to make it recover towards its resting pose
            else
            {
                m_WeaponRecoilLocalPosition = Vector3.Lerp(m_WeaponRecoilLocalPosition, Vector3.zero,
                    RecoilRestitutionSharpness * Time.deltaTime);
                m_AccumulatedRecoil = m_WeaponRecoilLocalPosition;
            }
        }

        


    }
}