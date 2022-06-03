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

        public GameObject objet;

        public enum TypeRessource
        {
           Ouverte,
           Fermer
        }

        public TypeRessource Type;

        public bool collision = false;

        public GameObject Prefab;

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
                collision = true;
                 PlayerCharacterController pickingPlayer = other.GetComponent<PlayerCharacterController>();
                
                if (pickingPlayer != null && Type == TypeRessource.Ouverte)
                {
                    OnTriggered(pickingPlayer);
                    
                }
                else
                {
                Debug.Log("press E");
                //afficher Ui appuyer sue e pour utiliser une potion de trampoplante
                
                }
                

            }

        private void OnTriggerExit(Collider other)
        {
            collision = false;
        }

        protected virtual void OnTriggered(PlayerCharacterController playerController)
            {
                PlayPickupFeedback();
                playerController.TrampoplanteJump();
            }

        public void Update()
        {
            if (Type == TypeRessource.Fermer && Input.GetKeyDown(KeyCode.E))
            {
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().NbTrampoplante > 0)
                {
                    Debug.Log("la trampoplante s'ouvre");
                    Instantiate(Prefab, transform.position, Quaternion.identity);
                    GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().NbTrampoplante -= 1;
                    Destroy(objet);
                }
                else
                {
                    Debug.Log("pas de potion");
                    //afficher Ui pas de potion de trampoplnate dans l'inventaire
                }
            }
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