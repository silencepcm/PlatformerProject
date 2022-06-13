using Unity.FPS.Game;
using UnityEngine;

namespace Unity.FPS.Gameplay
{
    public class Collect : MonoBehaviour
    {

        public AudioClip CollectSfx;
        public GameObject CollectVfxPrefab;
        public GameObject Inventaire;
        public GameObject HuD;
        

        public enum TypeRessource
        {
            Munitite,
            Directite,
            Clochite,
            Baie,
            Fruit,
            Poussite,
            Plontite1,
            Sac,
            eau,
            RecetteT,
            RecetteS,
            RecetteP
        }

        public TypeRessource Type;

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
            /*if(Type == TypeRessource.eau)
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<SurvieScript>().SliderGourde.value += 100;
                GameObject.FindGameObjectWithTag("GameFlowManager").GetComponent<GameFlowManager>().PtSauvegarde = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
            }*/
            if (Type == TypeRessource.Munitite)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Munitite += 1;
                HuD.GetComponent<Spawner_Ui_Ingredient>().Spawn_Ui_Ingredient("Munite");
            }
            else if (Type == TypeRessource.Directite)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Directite += 1;
                HuD.GetComponent<Spawner_Ui_Ingredient>().Spawn_Ui_Ingredient("Directite");
            }
            else if (Type == TypeRessource.Clochite)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Clochite += 1;
                HuD.GetComponent<Spawner_Ui_Ingredient>().Spawn_Ui_Ingredient("Clochite");
            }
            else if (Type == TypeRessource.Baie)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Baie += 1;
                HuD.GetComponent<Spawner_Ui_Ingredient>().Spawn_Ui_Ingredient("Baie");
            }
            else if (Type == TypeRessource.Fruit)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Fruit += 1;
                HuD.GetComponent<Spawner_Ui_Ingredient>().Spawn_Ui_Ingredient("Fruit");
            }
            else if (Type == TypeRessource.Plontite1)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Plontite += 1;
                HuD.GetComponent<Spawner_Ui_Ingredient>().Spawn_Ui_Ingredient("Plontite1");
            }
            else if (Type == TypeRessource.Poussite)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Poussite += 1;
                HuD.GetComponent<Spawner_Ui_Ingredient>().Spawn_Ui_Ingredient("Poussite");
            }
            else if (Type == TypeRessource.RecetteP)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().RecettePlonte += 1;
                HuD.GetComponent<Spawner_Ui_Ingredient>().Spawn_Ui_Ingredient("RecetteP");
            }
            else if (Type == TypeRessource.RecetteS)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().RecettePotionSante += 1;
                HuD.GetComponent<Spawner_Ui_Ingredient>().Spawn_Ui_Ingredient("RecetteS");
            }
            else if (Type == TypeRessource.RecetteT)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().RecetteTrampoplante += 1;
                HuD.GetComponent<Spawner_Ui_Ingredient>().Spawn_Ui_Ingredient("RecetteT");
            }
            else if (Type == TypeRessource.Sac)
            {
                Debug.Log("biblou");

                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Munitite += GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Munitite;
                HuD.GetComponent<Spawner_Ui_Ingredient>().Spawn_Ui_Ingredient("Munite");
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Clochite += GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Clochite;
                HuD.GetComponent<Spawner_Ui_Ingredient>().Spawn_Ui_Ingredient("Directite");
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Directite += GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Directite;
                HuD.GetComponent<Spawner_Ui_Ingredient>().Spawn_Ui_Ingredient("Clochite");
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().NbPotionOblique += GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().NbPotionOblique;
                HuD.GetComponent<Spawner_Ui_Ingredient>().Spawn_Ui_Ingredient("PotionOblique");
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().NbPotionDirect += GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().NbPotionOblique;
                HuD.GetComponent<Spawner_Ui_Ingredient>().Spawn_Ui_Ingredient("PotionDirect");
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Baie += GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Baie; 
                HuD.GetComponent<Spawner_Ui_Ingredient>().Spawn_Ui_Ingredient("Baie");
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Fruit += GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Fruit; 
                HuD.GetComponent<Spawner_Ui_Ingredient>().Spawn_Ui_Ingredient("Fruit");
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Plontite += GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Plontite; 
                HuD.GetComponent<Spawner_Ui_Ingredient>().Spawn_Ui_Ingredient("Plontite1");
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Poussite += GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Poussite; 
                HuD.GetComponent<Spawner_Ui_Ingredient>().Spawn_Ui_Ingredient("Poussite");
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().RecetteMunitionDirect += GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().RecetteMunitionDirect;
                HuD.GetComponent<Spawner_Ui_Ingredient>().Spawn_Ui_Ingredient("RecetteMD");
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().RecetteMunitionOblique += GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().RecetteMunitionOblique; 
                HuD.GetComponent<Spawner_Ui_Ingredient>().Spawn_Ui_Ingredient("RecetteMO");
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().RecettePotionSante += GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().RecettePotionSante;
                HuD.GetComponent<Spawner_Ui_Ingredient>().Spawn_Ui_Ingredient("RecetteS");
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().RecetteTrampoplante += GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().RecetteTrampoplante;
                HuD.GetComponent<Spawner_Ui_Ingredient>().Spawn_Ui_Ingredient("RecetteT");
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().RecettePlonte += GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().RecettePlonte;
                HuD.GetComponent<Spawner_Ui_Ingredient>().Spawn_Ui_Ingredient("RecetteP");

                GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Munitite = 0;
                GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Clochite = 0;
                GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Directite = 0;
                GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().NbPotionOblique = 0;
                GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().NbPotionDirect = 0;
                GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Baie = 0;
                GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Fruit = 0;
                GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Plontite = 0;
                GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Poussite = 0;
                GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().RecetteMunitionDirect = 0;
                GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().RecetteMunitionOblique = 0;
                GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().RecettePotionSante = 0;
                GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().RecetteTrampoplante = 0;
                GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().RecettePlonte = 0;
                Inventaire.GetComponent<UI.InGameMenuManager>().SetInventaireMenuActivation();


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