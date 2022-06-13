using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.FPS.Gameplay
{
    public class Plonte_Script : MonoBehaviour
    {
        public enum TypeRessource
        {
            Ouverte,
            Fermer
        }
        public TypeRessource Type;

        public Animator PlonteAnimator;

        public bool collision = false;

        void OnTriggerEnter(Collider other)
        {
            collision = true;
            PlayerCharacterController pickingPlayer = other.GetComponent<PlayerCharacterController>();

            if (pickingPlayer != null && Type == TypeRessource.Fermer)
            {
                Debug.Log("press E");

            }
        }

        private void OnTriggerExit(Collider other)
        {
            collision = false;
        }

        public void OnTriggerStay(Collider other)
        {
            Debug.Log("coll");
            if (Type == TypeRessource.Fermer && Input.GetKeyDown(KeyCode.E) && other.tag == "MainCamera")
            {
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().NbPotionPlonte > 0)
                {
                    Debug.Log("la Plonte s'ouvre");
                    //Pose anim ICI
                    PlonteAnimator.SetBool("Potion", true);

                    GameObject.FindGameObjectWithTag("Player").GetComponent<InventaireScript>().NbPotionPlonte -= 1;
                    Type = TypeRessource.Ouverte;
                }
                else
                {
                    Debug.Log("pas de potion");
                    //afficher Ui pas de potion de trampoplnate dans l'inventaire
                }
            }
        }
    }
}
