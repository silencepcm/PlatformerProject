using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.FPS.Game;


public class PlayerStatsScript : Personnage
{
    public static PlayerStatsScript _instance;
    public static PlayerStatsScript Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatsScript>();
            }

            return _instance;
        }
    }

    public float DelaySoif;
    public float DelayNourriture;
    public float Thirst;
    public float Gourde;
    public float Nourriture;

    public float timer = 0f;

    float maxSoif;
    float minSoif;

    float maxGourde;
    float minGourde;

    float maxNourriture = 100f;
    float minNourriture = 0f;

    float DegatsNourriture;
    float DegatsSoif;

    int NbIngredientA;
    int NbIngredientB;
    int NbIngredientC;
    int NbIngredientD;
    int NbIngredientE;
    int NbIngredientF;
    int NbIngredientG;

    int NbRecette1;
    int NbRecette2;
    int NbRecette3;
    int NbRecette4;
    int NbRecette5;

    public float CriticalVie;


    //public Dictionary<string, Ingridient>;


    [Tooltip("Health ratio at which the critical health vignette starts appearing")]
    public float CriticalHealthRatio = 0.3f;

    public UnityAction<float, GameObject> OnDamaged;
    public UnityAction<float> OnHealed;
    public UnityAction OnDie;

    public bool Invincible { get; set; }
    public float GetRatio() => Vie / MaxVie;
    public bool IsCritical() => GetRatio() <= CriticalHealthRatio;

    bool m_IsDead;

    void Start()
    {
        DelaySoif = GameManager.Instance.DelaySoif;
        DelayNourriture = GameManager.Instance.DelayNourriture;
        DegatsNourriture = GameManager.Instance.DegatsNourriture;
        DegatsSoif = GameManager.Instance.DegatsSoif;
        Vie = GameManager.Instance.Vie;
        MaxVie = 100;
        maxSoif = GameManager.Instance.MaxSoif;
        minSoif = GameManager.Instance.MinSoif;

        maxGourde = GameManager.Instance.MaxGourde;
        minGourde = GameManager.Instance.MinGourde;

        maxNourriture = GameManager.Instance.MaxNourriture;
        minNourriture = GameManager.Instance.MinNourriture;

        NbIngredientA = GameManager.Instance.NbIngredientA;
        NbIngredientB = GameManager.Instance.NbIngredientB;
        NbIngredientC = GameManager.Instance.NbIngredientC;
        NbIngredientD = GameManager.Instance.NbIngredientD;
        NbIngredientE = GameManager.Instance.NbIngredientE;
        NbIngredientF = GameManager.Instance.NbIngredientF;
        NbIngredientG = GameManager.Instance.NbIngredientG;

        NbRecette1 = GameManager.Instance.NbRecette1;
        NbRecette2 = GameManager.Instance.NbRecette2;
        NbRecette3 = GameManager.Instance.NbRecette3;
        NbRecette4 = GameManager.Instance.NbRecette4;
        NbRecette5 = GameManager.Instance.NbRecette5;

    }



    void Update()
    {

        Thirst -= 1f * Time.deltaTime;
        Nourriture -= 0.3f * Time.deltaTime;
        /*
        if (Input.GetKey(KeyCode.Alpha2) && verif == true)
        {
            Thirst += 20f;
            Gourde -= 20f;
            timer = 0f;
            verif = false;
        }*/


        if (Input.GetKey(KeyCode.Alpha2) /*&& verif == true*/)
        {
            Thirst += 20f;
            Gourde -= 20f;
            timer = 0f;
            //verif = false;
        }

        //a faire apres l'inventaire si nourriture dans l'inventaire le joueur mange*/
    }


    void FixedUpdate()
    {

        timer += Time.deltaTime;
        if (timer >= DelaySoif)
        {
            //verif = true;
        }
        if (Thirst > maxSoif)
        {
            Thirst = maxSoif;
        }

        if (Thirst < minSoif)
        {
            Thirst = minSoif;

            //player.GetComponent<Unity.FPS.Game.Health>().TakeDamage(0.1f, player);
            //perte de vie
        }

        if (Gourde > maxGourde)
        {
            Gourde = maxGourde;
        }

        if (Gourde < minGourde)
        {
            Gourde = minGourde;
        }

        if (Nourriture > maxNourriture)
        {
            Nourriture = maxNourriture;
        }

        if (Nourriture < minNourriture)
        {
            Nourriture = minNourriture;
            //hpDegats(1);
            //perte de vie
        }

    }

    public void hpDegats(int degats)
    {
        Vie -= degats;
        if (Vie <= 0)
        {

        }
    }
    public void Die()
    {
        alive = false;
    }
}
