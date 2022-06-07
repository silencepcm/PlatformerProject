using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//Script en construction : OBJECTIF : GEstion des barres d'eau/de Nourriture/de Gourde/de vie + lancement de la mort lier a la barre de vie
public class SurvieScript : MonoBehaviour
{
    public GameObject InventairePanel;
    public Slider SliderNourriture;
    public Slider SliderEau;
    public Slider SliderGourde;
    public Slider Vie;
    public GameObject activeImageConsom;
    public GameObject passiveImageConsom;
    public GameObject activeTextConsom;
    public GameObject passiveTextConsom;
    private float minNourriture = 0f;
    private float maxNourriture = 100f;
    private float minEau = 0f;
    private float maxEau = 100f;
    private float minGourde = 0f;
    private float maxGourde = 100f;
    private float maxVie = 100f;
    private int timerCoolDown = 2;
    private int timer = 0;
    private float perteEauSec = 0.01f;
    private float perteNourritureSec = 0.01f;
    private float perteVieSec = 0.01f;
    private bool baieActive = true;
    private float timerHoldButton = 0f;
    public GameObject Prefab;
    public GameObject Sac;


    // Start is called before the first frame update
    void Start()
    {
        //initialisation des barres
        SliderNourriture.value = maxNourriture;
        SliderEau.value = maxEau;
        SliderGourde.value = maxGourde;
        Vie.value = maxVie;
    }
    public void FixedUpdate()
    {
        //perte d'eau et de nourriture
        SliderNourriture.value -= perteNourritureSec;
        SliderEau.value -= perteEauSec;

        //perte de vie si l'une des 2 barres est a 0
        if (SliderEau.value <= 0f || SliderNourriture.value <= 0f)
        {
            Debug.Log(Vie.value);
            Vie.value -= perteVieSec;
            if (Vie.value <= 0f)
            {
                Dead();
            }
        }
    }

    private void Update()
    {
       
        timer += 1;
        //mise a 0 de la barre de nourriture
        if (SliderNourriture.value <= minNourriture)
        {
            SliderNourriture.value = minNourriture;
        }
        if (SliderNourriture.value >= maxNourriture)
        {
            SliderNourriture.value = maxNourriture;
        }

        if(Vie.value >= maxVie)
        {
           Vie.value = maxVie;
        }

        //mise a 0 de la barre d'eau
        if (SliderEau.value <= minEau)
        {
            SliderEau.value = minEau;
        }
        if(SliderEau.value >= maxEau)
        {
            SliderEau.value = maxEau;
        }

        if(SliderGourde.value <= minGourde)
        {
            SliderGourde.value = minGourde;
        }
        if(SliderGourde.value >= maxGourde)
        {
            SliderGourde.value = maxGourde;
        }

        //Boit dans la gourde si touche alpha3 enfoncer
        if (Input.GetKeyDown(KeyCode.Alpha3) && timer>timerCoolDown && SliderGourde.value >= 20f)
        {
            SliderGourde.value -= 20f;
            SliderEau.value += 20f;
        }
        //Mange un fruit si touche alpha2 enfoncer ATTENTION!!!!  A FAIRE : lier a l'inventaire pour detruire un obj nourriture dedans 
        if (Input.GetKey(KeyCode.Alpha2)&& timer > timerCoolDown)
        {
            timerHoldButton++;
            if (timerHoldButton == 200f)
            {
                Sprite spriteTemp;
                spriteTemp = activeImageConsom.GetComponent<Image>().sprite;
                activeImageConsom.GetComponent<Image>().sprite = passiveImageConsom.GetComponent<Image>().sprite;
                passiveImageConsom.GetComponent<Image>().sprite = spriteTemp;

                baieActive = !baieActive;
                Debug.Log("Changement");
            }
        }
        else if(!Input.GetKeyDown(KeyCode.Alpha2) && timerHoldButton>0) 
        {
            if(timerHoldButton<200)
            {
                Debug.Log("Manger");
                if (baieActive)
                {
                    if (InventairePanel.GetComponent<InventaireScript>().Baie > 0)
                    {
                        SliderNourriture.value += 20f;
                        InventairePanel.GetComponent<InventaireScript>().Baie -= 1;
                    }
                }
                else
                {
                    if (InventairePanel.GetComponent<InventaireScript>().Fruit > 0)
                    {
                        SliderNourriture.value += 20f;
                        InventairePanel.GetComponent<InventaireScript>().Fruit -= 1;
                    }
                }
            }
            timerHoldButton = 0;
        }

        if (Input.GetKeyDown(KeyCode.H) && timer > timerCoolDown && InventairePanel.GetComponent<InventaireScript>().NbPotionSante > 0 && Vie.value < maxVie)
        {
            InventairePanel.GetComponent<InventaireScript>().NbPotionSante -= 1;
            Vie.value = maxVie;
        }

        if (baieActive)
        {
            activeTextConsom.GetComponent<TextMeshProUGUI>().text = InventairePanel.GetComponent<InventaireScript>().Baie.ToString();
            passiveTextConsom.GetComponent<TextMeshProUGUI>().text = InventairePanel.GetComponent<InventaireScript>().Fruit.ToString();
        }
        else
        {
            activeTextConsom.GetComponent<TextMeshProUGUI>().text = InventairePanel.GetComponent<InventaireScript>().Fruit.ToString();
            passiveTextConsom.GetComponent<TextMeshProUGUI>().text = InventairePanel.GetComponent<InventaireScript>().Baie.ToString();
        }

    }

    public void Dead()
    {
        Debug.Log("Mort"); // tuer le perso et lancer la scene de defaite
        Sac = Instantiate(Prefab, transform.position, Quaternion.identity);
        Sac.GetComponent<InventaireScript>().NbMunitionDirect = InventairePanel.GetComponent<InventaireScript>().NbMunitionDirect / 2;
        Sac.GetComponent<InventaireScript>().NbMunitionOblique = InventairePanel.GetComponent<InventaireScript>().NbMunitionOblique / 2;
        Sac.GetComponent<InventaireScript>().Munitite = InventairePanel.GetComponent<InventaireScript>().Munitite;
        Sac.GetComponent<InventaireScript>().Directite = InventairePanel.GetComponent<InventaireScript>().Directite;
        Sac.GetComponent<InventaireScript>().Clochite = InventairePanel.GetComponent<InventaireScript>().Clochite;
        Sac.GetComponent<InventaireScript>().Sac = 1;
        Sac.GetComponent<InventaireScript>().NbPotionSante = InventairePanel.GetComponent<InventaireScript>().NbPotionSante;
        Sac.GetComponent<InventaireScript>().NbTrampoplante = InventairePanel.GetComponent<InventaireScript>().NbTrampoplante;
        Sac.GetComponent<InventaireScript>().Fruit = InventairePanel.GetComponent<InventaireScript>().Fruit;
        Sac.GetComponent<InventaireScript>().Baie = InventairePanel.GetComponent<InventaireScript>().Baie;
        Sac.GetComponent<InventaireScript>().Poussite = InventairePanel.GetComponent<InventaireScript>().Poussite;
        Sac.GetComponent<InventaireScript>().Plontite1 = InventairePanel.GetComponent<InventaireScript>().Plontite1;
        Sac.GetComponent<InventaireScript>().RecetteMunitionDirect = InventairePanel.GetComponent<InventaireScript>().RecetteMunitionDirect;
        Sac.GetComponent<InventaireScript>().RecetteMunitionOblique = InventairePanel.GetComponent<InventaireScript>().RecetteMunitionOblique;
        Sac.GetComponent<InventaireScript>().RecettePotionSante = InventairePanel.GetComponent<InventaireScript>().RecettePotionSante;
        Sac.GetComponent<InventaireScript>().RecetteTrampoplante = InventairePanel.GetComponent<InventaireScript>().RecetteTrampoplante;



        InventairePanel.GetComponent<InventaireScript>().Sac = 0;
        InventairePanel.GetComponent<InventaireScript>().Clochite = 0;
        InventairePanel.GetComponent<InventaireScript>().Directite = 0;
        InventairePanel.GetComponent<InventaireScript>().Munitite = 0;
        InventairePanel.GetComponent<InventaireScript>().NbMunitionDirect = InventairePanel.GetComponent<InventaireScript>().NbMunitionDirect / 2;
        InventairePanel.GetComponent<InventaireScript>().NbMunitionOblique = InventairePanel.GetComponent<InventaireScript>().NbMunitionOblique / 2;
        InventairePanel.GetComponent<InventaireScript>().NbPotionSante = 0;
        InventairePanel.GetComponent<InventaireScript>().NbTrampoplante = 0;
        InventairePanel.GetComponent<InventaireScript>().Fruit = 0;
        InventairePanel.GetComponent<InventaireScript>().Baie = 0;
        InventairePanel.GetComponent<InventaireScript>().Poussite = 0;
        InventairePanel.GetComponent<InventaireScript>().Plontite1 = 0;
        InventairePanel.GetComponent<InventaireScript>().RecetteMunitionDirect = 0;
        InventairePanel.GetComponent<InventaireScript>().RecetteMunitionOblique = 0;
        InventairePanel.GetComponent<InventaireScript>().RecettePotionSante = 0;
        InventairePanel.GetComponent<InventaireScript>().RecetteTrampoplante = 0;

    }

    // Update is called once per frame

}
