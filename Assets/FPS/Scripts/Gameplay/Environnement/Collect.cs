using Unity.FPS.Game;
using UnityEngine;

namespace Unity.FPS.Gameplay
{
    public class Collect : MonoBehaviour
    {

        public AudioClip CollectSfx;
        public GameObject CollectVfxPrefab;


        Collider m_Collider;
        Vector3 m_StartPosition;
        bool m_HasPlayedFeedback;

        public void Start()
        {
            m_Collider = GetComponent<Collider>();

            m_Collider.isTrigger = true;
            m_StartPosition = transform.position;
        }

        void Update()
        {

        }

        void OnTriggerEnter(Collider other)
        {
            PlayerCharacterController pickingPlayer = other.GetComponentInParent<PlayerCharacterController>();
            if (pickingPlayer)
            {
                pickingPlayer.SetCanCollect(gameObject, true);
            }
        }
        void OnTriggerExit(Collider other)
        {
            PlayerCharacterController pickingPlayer = other.GetComponentInParent<PlayerCharacterController>();

            if (pickingPlayer)
            {
                pickingPlayer.SetCanCollect(gameObject, false);
            }
        }

        public void PlayPickupFeedback()
        {
            if (m_HasPlayedFeedback)
                return;

            if (CollectSfx)
            {
                AudioUtility.CreateSFX(CollectSfx, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
            }

            if (CollectVfxPrefab)
            {
                Instantiate(CollectVfxPrefab, transform.position, Quaternion.identity);
            }

            m_HasPlayedFeedback = true;
        }
        public void CollectEvent()
        {
            switch (name)
            {
                case "Sac":
                    break;
                case "Munitite":
                    GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Munitite++;
                    break;
                case "Directite":
                    GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Directite++;
                    break;
                case "Clochite":
                    GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Clochite++;
                    break;
                case "Fruit":
                    GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Fruit++;
                    break;
                case "Baie":
                    GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Baie++;
                    break;
                case "Poussite":
                    GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Poussite++;
                    break;

            }
            PlayPickupFeedback();
            gameObject.SetActive(false);
            if (name != "Sac")
            {
                GetComponentInParent<Spawner_Ingredient>().StartCoroutine(GetComponentInParent<Spawner_Ingredient>().RespawnWaiter(gameObject));
            }
        }
    }
}