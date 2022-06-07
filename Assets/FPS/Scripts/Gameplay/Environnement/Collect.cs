﻿using Unity.FPS.Game;
using UnityEngine;

namespace Unity.FPS.Gameplay
{
    public class Collect : MonoBehaviour
    {

        public AudioClip CollectSfx;
        public GameObject CollectVfxPrefab;

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
            eau
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
            }
            else if (Type == TypeRessource.Directite)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Directite += 1;
            }
            else if (Type == TypeRessource.Clochite)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Clochite += 1;
            }
            else if (Type == TypeRessource.Baie)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Baie += 1;
            }
            else if (Type == TypeRessource.Fruit)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Fruit += 1;
            }
            else if (Type == TypeRessource.Plontite1)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Plontite1 += 1;
            }
            else if(Type == TypeRessource.Poussite)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Poussite += 1;
            }
            else if(Type == TypeRessource.Sac)
            {
                Debug.Log("biblou");

                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Sac += GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Sac;
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Munitite += GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Munitite;
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Clochite += GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Clochite;
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Directite += GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Directite;
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().NbMunitionOblique += GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().NbMunitionOblique;
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().NbMunitionDirect += GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().NbMunitionOblique;
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Baie += GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Baie;
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Fruit += GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Fruit;
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Plontite1 += GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Plontite1;
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().Poussite += GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Poussite;
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().RecetteMunitionDirect += GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().RecetteMunitionDirect;
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().RecetteMunitionOblique += GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().RecetteMunitionOblique;
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().RecettePotionSante += GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().RecettePotionSante;
                GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().RecetteTrampoplante += GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().RecetteTrampoplante;

                GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Sac += 0;
                GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Munitite += 0;
                GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Clochite += 0;
                GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Directite += 0;
                GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().NbMunitionOblique += 0;
                GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().NbMunitionDirect += 0;
                GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Baie += 0;
                GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Fruit += 0;
                GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Plontite1 += 0;
                GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().Poussite += 0;
                GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().RecetteMunitionDirect += 0;
                GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().RecetteMunitionOblique += 0;
                GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().RecettePotionSante += 0;
                GameObject.FindGameObjectWithTag("Sac").GetComponent<InventaireScript>().RecetteTrampoplante += 0;
            }
            PlayPickupFeedback();
            gameObject.SetActive(false);
            if (name != "Sac")
            {
                
                GetComponentInParent<Spawner_Ingredient>().RespawnWaiter(gameObject);
            }

           
        }
    }
}