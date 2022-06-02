using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;

namespace Unity.FPS.Gameplay
{
        public class TrampoplanteScript : MonoBehaviour
        {
            [Tooltip("Frequency at which the item will move up and down")]
            public float VerticalBobFrequency = 1f;

            [Tooltip("Distance the item will move up and down")]
            public float BobbingAmount = 1f;


            [Tooltip("Sound played on pickup")] public AudioClip PickupSfx;
            [Tooltip("VFX spawned on pickup")] public GameObject PickupVfxPrefab;

            public Rigidbody PickupRigidbody { get; private set; }

            Collider m_Collider;
            bool m_HasPlayedFeedback;

            protected virtual void Start()
            {
                PickupRigidbody = GetComponent<Rigidbody>();
                DebugUtility.HandleErrorIfNullGetComponent<Rigidbody, TrampoplanteScript>(PickupRigidbody, this, gameObject);
                m_Collider = GetComponent<Collider>();
                DebugUtility.HandleErrorIfNullGetComponent<Collider, TrampoplanteScript>(m_Collider, this, gameObject);

                // ensure the physics setup is a kinematic rigidbody trigger
                PickupRigidbody.isKinematic = true;
                m_Collider.isTrigger = true;

            }


            void OnTriggerEnter(Collider other)
            {
                PlayerCharacterController pickingPlayer = other.GetComponent<PlayerCharacterController>();

                if (pickingPlayer != null)
                {
                    OnTriggered(pickingPlayer);
                }
            }

            protected virtual void OnTriggered(PlayerCharacterController playerController)
            {
                PlayPickupFeedback();
                playerController.TrampoplanteJump();
            }

            public void PlayPickupFeedback()
            {
                if (m_HasPlayedFeedback)
                    return;

                if (PickupSfx)
                {
                    AudioUtility.CreateSFX(PickupSfx, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
                }

                if (PickupVfxPrefab)
                {
                    var pickupVfxInstance = Instantiate(PickupVfxPrefab, transform.position, Quaternion.identity);
                }

                m_HasPlayedFeedback = true;
            }
        }
    }