using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DigitalRuby.Tween;

//Script en construction : OBJECTIF : GEstion des barres d'eau/de Nourriture/de Gourde/de vie + lancement de la mort lier a la barre de vie
public class SurvieScript : MonoBehaviour
{
    private GameObject Player;
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
    private float maxEau = 60f;
    private float minGourde = 0f;
    private float maxGourde = 60f;
    private float maxVie = 100f;
    private int timerCoolDown = 2;
    private int timer = 0;
    private float perteEauSec = 0.01f;
    private float perteNourritureSec = 0.01f;
    private float perteVieSec = 0.01f;
    private bool baieActive = true;
    private float timerHoldButton = 0f;
    public GameObject Prefab;
    public GameObject FbSante;
    private SpriteRenderer spriteRenderer;




    // Start is called before the first frame update
    void Start()
    {
        //initialisation des barres
        SliderNourriture.value = maxNourriture;
        SliderEau.value = maxEau;
        SliderGourde.value = maxGourde;
        Vie.value = maxVie;
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    public void FixedUpdate()
    {
       
        //perte d'eau et de nourriture
        SliderNourriture.value -= perteNourritureSec;
        SliderEau.value -= perteEauSec;
        if (Vie.value < 25f)
        {
            FbSante.SetActive(true);
            TweenColor();
        }
        
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
        FbSante.SetActive(false);
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
                    if (Player.GetComponent<InventaireScript>().Baie > 0)
                    {
                        SliderNourriture.value += 20f;
                        Player.GetComponent<InventaireScript>().Baie -= 1;
                    }
                }
                else
                {
                    if (Player.GetComponent<InventaireScript>().Fruit > 0)
                    {
                        SliderNourriture.value += 20f;
                        Player.GetComponent<InventaireScript>().Fruit -= 1;
                    }
                }
            }
            timerHoldButton = 0;
        }

        if (Input.GetKeyDown(KeyCode.H) && timer > timerCoolDown && Player.GetComponent<InventaireScript>().NbPotionSante > 0 && Vie.value < maxVie)
        {
            Player.GetComponent<InventaireScript>().NbPotionSante -= 1;
            Vie.value = maxVie;
        }

        if (baieActive)
        {
            activeTextConsom.GetComponent<TextMeshProUGUI>().text = Player.GetComponent<InventaireScript>().Baie.ToString();
            passiveTextConsom.GetComponent<TextMeshProUGUI>().text = Player.GetComponent<InventaireScript>().Fruit.ToString();
        }
        else
        {
            activeTextConsom.GetComponent<TextMeshProUGUI>().text = Player.GetComponent<InventaireScript>().Fruit.ToString();
            passiveTextConsom.GetComponent<TextMeshProUGUI>().text = Player.GetComponent<InventaireScript>().Baie.ToString();
        }

    }
    private void TweenColor()
    {
        System.Action<ITween<Color>> updateColor = (t) =>
        {
            spriteRenderer.color = t.CurrentValue;
        };

        Color endColor = UnityEngine.Random.ColorHSV(0.0f, 1.0f, 0.0f, 1.0f, 0.5f, 1.0f, 1.0f, 1.0f);

        // completion defaults to null if not passed in
        FbSante.gameObject.Tween("ColorCircle", spriteRenderer.color, endColor, 1.0f, TweenScaleFunctions.QuadraticEaseOut, updateColor);
    }
    public void Dead()
    {
        Player.GetComponent<PlayerStatsScript>().Kill();
        GameObject Sac = Instantiate(Prefab, transform.position, Quaternion.identity);
        InventaireScript sacInventaire = Sac.GetComponent<InventaireScript>();
        InventaireScript Inventaire = Player.GetComponent<InventaireScript>();
        sacInventaire.NbPotionDirect = Inventaire.NbPotionDirect / 2;
        sacInventaire.NbPotionOblique = Inventaire.NbPotionOblique / 2;
        sacInventaire.Munitite = Inventaire.Munitite;
        sacInventaire.Directite = Inventaire.Directite;
        sacInventaire.Clochite = Inventaire.Clochite;
        sacInventaire.NbPotionSante = Inventaire.NbPotionSante;
        sacInventaire.NbPotionTrampoplante = Inventaire.NbPotionTrampoplante;
        sacInventaire.Fruit = Inventaire.Fruit;
        sacInventaire.Baie = Inventaire.Baie;
        sacInventaire.Poussite = Inventaire.Poussite;
        sacInventaire.Plontite = Inventaire.Plontite;
        sacInventaire.RecetteMunitionDirect = Inventaire.RecetteMunitionDirect;
        sacInventaire.RecetteMunitionOblique = Inventaire.RecetteMunitionOblique;
        sacInventaire.RecettePotionSante = Inventaire.RecettePotionSante;
        sacInventaire.RecetteTrampoplante = Inventaire.RecetteTrampoplante;



        Inventaire.Clochite = 0;
        Inventaire.Directite = 0;
        Inventaire.Munitite = 0;
        Inventaire.NbPotionDirect /= 2;
        Inventaire.NbPotionOblique /=  2;
        Inventaire.NbPotionSante = 0;
        Inventaire.NbPotionTrampoplante = 0;
        Inventaire.Fruit = 0;
        Inventaire.Baie = 0;
        Inventaire.Poussite = 0;
        Inventaire.Plontite = 0;
        Inventaire.RecetteMunitionDirect = 0;
        Inventaire.RecetteMunitionOblique = 0;
        Inventaire.RecettePotionSante = 0;
        Inventaire.RecetteTrampoplante = 0;
    }

    // Update is called once per frame
    
}
