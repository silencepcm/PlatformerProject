using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{

    #region SINGLETON PATTERN
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    Resources.Load<GameObject>("GameManager");
                    _instance.Awake();
                }
            }

            return _instance;
        }
    }
    #endregion
    [HideInInspector] public bool FallDamage;
    [HideInInspector] public float MaxSpeedOnGround;
    [HideInInspector] public float MaxSpeedInAir;
    [HideInInspector] public float JumpForce;
    [HideInInspector] public float GravityForce;
    [HideInInspector] public float MaxChargeDuration;
    [HideInInspector] public int MaxAmmo;
    [HideInInspector] public float BulletSpreadAngle;
    [HideInInspector] public float TrampoplanteForce;
    [HideInInspector] public float MinSpeedFallDamage;
    [HideInInspector] public float FallDamageValeurAtMinSpeed;
    [HideInInspector] public float MaxSpeedFallDamage;
    [HideInInspector] public float FallDamageValeurAtMaxSpeed;


    [HideInInspector] public int MaxVie;
    [HideInInspector] public int Vie;


    [HideInInspector] public float MaxSoif;
    [HideInInspector] public float MinSoif;

    [HideInInspector] public float MaxGourde;
    [HideInInspector] public float MinGourde;

    [HideInInspector] public float MaxNourriture;
    [HideInInspector] public float MinNourriture;


    [HideInInspector] public bool Collect;
    [HideInInspector] public bool Eat;


    [HideInInspector] public float DelaySoif;
    [HideInInspector] public float DelayNourriture;
    [HideInInspector] public float DegatsNourriture;
    [HideInInspector] public float DegatsSoif;

    [HideInInspector] public int NbIngredientA;
    [HideInInspector] public int NbIngredientB;
    [HideInInspector] public int NbIngredientC;
    [HideInInspector] public int NbIngredientD;
    [HideInInspector] public int NbIngredientE;
    [HideInInspector] public int NbIngredientF;
    [HideInInspector] public int NbIngredientG;

    [HideInInspector] public int NbRecette1;
    [HideInInspector] public int NbRecette2;
    [HideInInspector] public int NbRecette3;
    [HideInInspector] public int NbRecette4;
    [HideInInspector] public int NbRecette5;
    

    [HideInInspector] public float BulletGravity;
    [HideInInspector] public float BulletSpeed;


    [HideInInspector] public float BruteWalkSpeed;
    [HideInInspector] public float BruteRunSpeed;
    [HideInInspector] public float BruteAcceleration;
    [HideInInspector] public float BruteAngleSpeed;
    [HideInInspector] public float BruteAttackDelay;
    [HideInInspector] public float BruteAttackDistance;
    [HideInInspector] public float BruteStopDistance;
    [HideInInspector] public float BruteDetectDistance;
    
    

    [HideInInspector] public float TourelleAngleSpeed;
    [HideInInspector] public float TourelleAttackDelay;
    [HideInInspector] public float TourelleAttackDistance;
    [HideInInspector] public float TourelleAttackStopDistance;
    [HideInInspector] public float TourelleDetectDistance;

    [HideInInspector] public float FrondeWalkSpeed;
    [HideInInspector] public float FrondeRunSpeed;
    [HideInInspector] public float FrondeAcceleration;
    [HideInInspector] public float FrondeAngleSpeed;
    [HideInInspector] public float FrondeAttackDelay;
    [HideInInspector] public float FrondeAttackDistance;
    [HideInInspector] public float FrondeStopDistance;
    [HideInInspector] public float FrondeDetectDistance;
    



    public GameObject Player;
    public GameObject InventairePanel;

    public TextMeshProUGUI NbIngredient1;
    public TextMeshProUGUI NbIngredient2;
    public TextMeshProUGUI NbIngredient3;
    public TextMeshProUGUI NbIngredient4;
    public TextMeshProUGUI NbIngredient5;
    public TextMeshProUGUI NbIngredient6;
    public TextMeshProUGUI NbIngredient7;
    public TextMeshProUGUI NbIngredient8;

    public GameObject Recette1;
    public GameObject Recette2;
    public GameObject Recette3;
    public GameObject Recette4;
    public GameObject Recette5;

    public TextMeshProUGUI NbPotionDirect;
    public TextMeshProUGUI NbPotionOblique;
    public TextMeshProUGUI NbPotionSante;
    public TextMeshProUGUI NbPotionTrampoplante;
    public TextMeshProUGUI NbPotionPlonte;

    public GameObject AthMunitionDirect;
    public GameObject AthMunitionOblique;
    public GameObject NbPotionSanteATH;

    private bool HasBeenUsed;


    public void Awake()
    {
        SaveData data = SaveToyboxScript.LoadData();
        if (data != null)
        {
            FallDamage = data.FallDamage;
            MaxSpeedOnGround = data.MovementSpeedOnGround;
            MaxSpeedInAir = data.MovementSpeedInAir;
            JumpForce = data.JumpForce;
            GravityForce = data.GravityForce;
            MaxChargeDuration = data.MaxChargeDuration;
            MaxAmmo = data.MaxAmmo;
            BulletSpreadAngle = data.BulletSpreadAngle;
            TrampoplanteForce = data.TrampoplanteForce;
            MinSpeedFallDamage = data.MinSpeedFallDamage;
            FallDamageValeurAtMinSpeed = data.FallDamageValeurAtMinSpeed;
            MaxSpeedFallDamage = data.MaxSpeedFallDamage;
            FallDamageValeurAtMaxSpeed = data.FallDamageValeurAtMaxSpeed;

            Vie = data.Vie;
            MaxVie = data.MaxVie;

            MaxSoif = data.maxThirst;
            MinSoif = data.minThirst;

            MaxGourde = data.maxGourde;
            MinGourde = data.minGourde;

            MaxNourriture = data.maxNourriture;
            MinNourriture = data.minNourriture;

            Collect = data.Collect;
            Eat = data.Eat;


            DelaySoif = data.DelaySoif;
            DelayNourriture = data.DelayNourriture;
            DegatsSoif = data.DegatsSoif;
            DegatsNourriture = data.DegatsNourriture;

            NbIngredientA = data.NbIngredientA;
            NbIngredientB = data.NbIngredientB;
            NbIngredientC = data.NbIngredientC;
            NbIngredientD = data.NbIngredientD;
            NbIngredientE = data.NbIngredientE;
            NbIngredientF = data.NbIngredientF;
            NbIngredientG = data.NbIngredientG;

            NbRecette1 = data.NbRecette1;
            NbRecette2 = data.NbRecette2;
            NbRecette3 = data.NbRecette3;
            NbRecette4 = data.NbRecette4;
            NbRecette5 = data.NbRecette5;

            BulletGravity = data.BulletGravity;
            BulletSpeed = data.BulletSpeed;



            BruteWalkSpeed = data.BruteWalkSpeed;
            BruteRunSpeed = data.BruteRunSpeed;
            BruteAngleSpeed = data.BruteAngleSpeed;
            BruteAttackDelay = data.BruteAttackDelay;
            BruteAttackDistance = data.BruteAttackDistance;
            BruteStopDistance = data.BruteStopDistance;
            BruteDetectDistance = data.BruteDetectDistance;
            BruteAcceleration = data.BruteAcceleration;

            TourelleAngleSpeed = data.TourelleAngleSpeed;
            TourelleAttackDelay = data.TourelleAttackDelay;
            TourelleAttackDistance = data.TourelleAttackDistance;
            TourelleAttackStopDistance = data.TourelleAttackStopDistance;
            TourelleDetectDistance = data.TourelleDetectDistance;

            FrondeWalkSpeed = data.FrondeWalkSpeed;
            FrondeRunSpeed = data.FrondeRunSpeed;
            FrondeAttackDelay = data.FrondeAttackDelay;
            FrondeAngleSpeed = data.FrondeAngleSpeed;
            FrondeAttackDistance = data.FrondeAttackDistance;
            FrondeStopDistance = data.FrondeStopDistance;
            FrondeDetectDistance = data.FrondeDetectDistance;
            FrondeAcceleration = data.FrondeAcceleration;

        }
        else
        {
            FallDamage = true;
            MaxSpeedOnGround = 7;
            MaxSpeedInAir = 7;
            JumpForce = 11;
            GravityForce = 33;
            MaxChargeDuration = 5;
            MaxAmmo = 5;
            BulletSpreadAngle = 5;
            TrampoplanteForce = 35;
            MinSpeedFallDamage = 26;
            FallDamageValeurAtMinSpeed = 5;
            MaxSpeedFallDamage = 52;
            FallDamageValeurAtMaxSpeed = 20;


            Vie = 100;
            MaxVie = 100;
            MaxSoif = 100f;
            MinSoif = 0f;
            MaxGourde = 100f;
            MinGourde = 0f;
            MaxNourriture = 100f;
            MinNourriture = 0f;


            DelaySoif = 3f;
            DelayNourriture = 3f;
            DegatsSoif = 10f;
            DegatsNourriture = 10f;

            NbIngredientA = 0;
            NbIngredientB = 0;
            NbIngredientC = 0;
            NbIngredientD = 0;
            NbIngredientE = 0;
            NbIngredientF = 0;
            NbIngredientG = 0;

            NbRecette1 = 0;
            NbRecette2 = 0;
            NbRecette3 = 0;
            NbRecette4 = 0;
            NbRecette5 = 0;

            BulletGravity = 10;
            BulletSpeed = 3;


            BruteWalkSpeed = 3f;
            BruteRunSpeed = 4.5f;
            BruteAngleSpeed = 120f;
            BruteAttackDistance = 2f;
            BruteStopDistance = 2.3f;
            BruteDetectDistance = 8f;
            BruteAcceleration = 8f;

            TourelleAngleSpeed = 120f;
            TourelleAttackDistance = 6f;
            TourelleAttackStopDistance = 10f;
            TourelleDetectDistance = 10f;

            FrondeWalkSpeed = 3f;
            FrondeRunSpeed = 4.5f;
            FrondeAngleSpeed = 120f;
            FrondeAttackDistance = 6f;
            FrondeStopDistance = 10f;
            FrondeDetectDistance = 10f;
        }
    }
    
    private void Update()
    {
        AthMunitionDirect.GetComponent<TextMeshProUGUI>().text = Player.GetComponent<InventaireScript>().NbPotionDirect.ToString();
        AthMunitionOblique.GetComponent<TextMeshProUGUI>().text = Player.GetComponent<InventaireScript>().NbPotionOblique.ToString();
        NbPotionSanteATH.GetComponent<TextMeshProUGUI>().text = Player.GetComponent<InventaireScript>().NbPotionSante.ToString();

        if (InventairePanel.activeInHierarchy)
        {

            NbIngredient1.text = Player.GetComponent<InventaireScript>().Munitite.ToString();
            NbIngredient2.text = Player.GetComponent<InventaireScript>().Directite.ToString();
            NbIngredient3.text = Player.GetComponent<InventaireScript>().Clochite.ToString();
            NbIngredient4.text = Player.GetComponent<InventaireScript>().Baie.ToString();
            NbIngredient5.text = Player.GetComponent<InventaireScript>().Fruit.ToString();
            NbIngredient6.text = Player.GetComponent<InventaireScript>().Poussite.ToString();
            NbIngredient7.text = Player.GetComponent<InventaireScript>().Plontite.ToString();
            NbPotionDirect.text = Player.GetComponent<InventaireScript>().NbPotionDirect.ToString();
            NbPotionOblique.text = Player.GetComponent<InventaireScript>().NbPotionOblique.ToString();
            NbPotionSante.text = Player.GetComponent<InventaireScript>().NbPotionSante.ToString();
            NbPotionTrampoplante.text = Player.GetComponent<InventaireScript>().NbPotionTrampoplante.ToString();
            NbPotionPlonte.text = Player.GetComponent<InventaireScript>().NbPotionPlonte.ToString();

            if (Player.GetComponent<InventaireScript>().RecetteMunitionDirect > 0 && Recette1.activeInHierarchy == false)
            {
                Recette1.SetActive(true);
            }


            if (Player.GetComponent<InventaireScript>().RecetteMunitionOblique > 0 && Recette2.activeInHierarchy == false)
            {
                Recette2.SetActive(true);
            }


            if (Player.GetComponent<InventaireScript>().RecettePotionSante > 0 && Recette3.activeInHierarchy == false)
            {
                Recette3.SetActive(true);
            }


            if (Player.GetComponent<InventaireScript>().RecetteTrampoplante > 0 && Recette4.activeInHierarchy == false)
            {
                Recette4.SetActive(true);
            }

            if (Player.GetComponent<InventaireScript>().RecettePlonte > 0 && Recette5.activeInHierarchy == false)
            {
                Recette5.SetActive(true);
            }

        }
    }
    
    public void OnClickRecetteMunitionDirect()
    {
        if (Player.GetComponent<InventaireScript>().Munitite >= 3 && Player.GetComponent<InventaireScript>().Directite >= 2)
        {
            Player.GetComponent<InventaireScript>().Directite -= 1;
            Player.GetComponent<InventaireScript>().Munitite -= 3;
            Player.GetComponent<InventaireScript>().NbPotionDirect += 5;
        }
    }
    public void OnClickRecetteMunitionOblique()
    {
        if (Player.GetComponent<InventaireScript>().Munitite >= 3 && Player.GetComponent<InventaireScript>().Clochite >= 2)
        {
            Player.GetComponent<InventaireScript>().Clochite -= 2;
            Player.GetComponent<InventaireScript>().Munitite -= 6;
            Player.GetComponent<InventaireScript>().NbPotionOblique += 5;
        }
    }
    public void OnClickRecettePotionDeSanté()
    {
        if (Player.GetComponent<InventaireScript>().Baie >= 3 && Player.GetComponent<InventaireScript>().Fruit >= 1)
        {
            Player.GetComponent<InventaireScript>().Baie -= 4;
            Player.GetComponent<InventaireScript>().Fruit -= 3;
            Player.GetComponent<InventaireScript>().NbPotionSante += 1;

        }
    }
    public void OnClickRecetteDeTrampoplante()
    {
        if (Player.GetComponent<InventaireScript>().Directite >= 2 && Player.GetComponent<InventaireScript>().Poussite >= 2)
        {
            Player.GetComponent<InventaireScript>().Directite -= 2;
            Player.GetComponent<InventaireScript>().Poussite -= 2;
            Player.GetComponent<InventaireScript>().NbPotionTrampoplante += 1;
        }
    }
    public void OnClickRecetteDePlonte()
    {
        if (Player.GetComponent<InventaireScript>().Poussite >= 5 && Player.GetComponent<InventaireScript>().Plontite >= 1)
        {
            Player.GetComponent<InventaireScript>().Poussite -= 2;
            Player.GetComponent<InventaireScript>().Plontite -= 2;
            Player.GetComponent<InventaireScript>().NbPotionPlonte += 1;
        }
    }

}
