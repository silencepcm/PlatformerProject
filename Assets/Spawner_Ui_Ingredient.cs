using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner_Ui_Ingredient : MonoBehaviour
{
   
    public Image Ui_Munite;
    public Image Ui_Directite;
    public Image Ui_Clochite;
    public Image UI_Baie;
    public Image Ui_Fruit;
    public Image Ui_Plontite1;
    public Image Ui_Poussite;
    public Image Ui_RecetteP;
    public Image Ui_RecetteS;
    public Image Ui_RecetteT;
    public Image Ui_PotionDirect;
    public Image Ui_PotionOblique;
    public Image Ui_PotionSante;
    public Image Ui_PotionTrampoplante;
    public Image Ui_potionplonte;
    public Image Ui_RecetteMO;
    public Image Ui_RecetteMD;

    public Vector3 defaultPosition;

    GameObject canvas;
    // Start is called before the first frame update
    private void Start()
    {
        defaultPosition = transform.position;
    }

    // Update is called once per frame
    public void Spawn_Ui_Ingredient(string Nom)
    {
        canvas = GameObject.FindGameObjectWithTag("canva");
        switch (Nom)
        {
            case "Munite":
                
                //Ui_Munite.transform.Translate((new Vector3(-90.9f,-2.6f,0f)- transform.position).normalized*Time.deltaTime*0.01f);
                Image Munite = Instantiate(Ui_Munite) as Image;
                Munite.transform.SetParent(canvas.transform);
                break;
            case "Directite":
                Image Directite = Instantiate(Ui_Directite) as Image;
                Directite.transform.SetParent(canvas.transform);
                break;
            case "Clochite":
                Image Clochite = Instantiate(Ui_Clochite) as Image;
                Clochite.transform.SetParent(canvas.transform);
                break;
            case "Baie":
                Image Baie = Instantiate(UI_Baie) as Image;
                Baie.transform.SetParent(canvas.transform);
                break;
            case "Fruit":
                Image Fruit = Instantiate(Ui_Fruit) as Image;
                Fruit.transform.SetParent(canvas.transform);
                break;
            case "Plontite1":
                Image Plontite1 = Instantiate(Ui_Plontite1) as Image;
                Plontite1.transform.SetParent(canvas.transform);
                break;
            case "Poussite":
                Image Poussite = Instantiate(Ui_Poussite) as Image;
                Poussite.transform.SetParent(canvas.transform);
                break;
            case "RecetteP":
                Image RecetteP = Instantiate(Ui_RecetteP) as Image;
                RecetteP.transform.SetParent(canvas.transform);
                break;
            case "RecetteS":
                Image RecetteS = Instantiate(Ui_RecetteS) as Image;
                RecetteS.transform.SetParent(canvas.transform);
                break;
            case "RecetteT":
                Image RecetteT = Instantiate(Ui_RecetteT) as Image;
                RecetteT.transform.SetParent(canvas.transform);
                break;
            case "PotionDirect":
                Image PotionDirect = Instantiate(Ui_PotionDirect) as Image;
                PotionDirect.transform.SetParent(canvas.transform);
                break;
            case "PotionOblique":
                Image PotionOblique = Instantiate(Ui_PotionOblique) as Image;
                PotionOblique.transform.SetParent(canvas.transform);
                break;
            case "PotionSante":
                Image PotionSante = Instantiate(Ui_PotionSante) as Image;
                PotionSante.transform.SetParent(canvas.transform);
                break;
            case "PotionTrampoplante":
                Image PotionTrampoplante = Instantiate(Ui_PotionTrampoplante) as Image;
                PotionTrampoplante.transform.SetParent(canvas.transform);
                break;
            case "PotionPlonte":
                Image PotionPlonte = Instantiate(Ui_potionplonte) as Image;
                PotionPlonte.transform.SetParent(canvas.transform);
                break;
            case "RecetteMD":
                Image RecetteMD = Instantiate(Ui_RecetteMO) as Image;
                RecetteMD.transform.SetParent(canvas.transform);
                break;
            case "RecetteMO":
                Image RecetteMO = Instantiate(Ui_RecetteMD) as Image;
                RecetteMO.transform.SetParent(canvas.transform);
                break;
        }
    }

    public void DeSpawn_Ui_Ingredient(string Nom)
    {
        switch (Nom)
        {
            case "Munite":
                Ui_Munite.transform.position = defaultPosition;
               

               
                
                Debug.Log("bob");
                break;
            case "Directite":
                Instantiate(Ui_Directite, transform.position, Quaternion.identity);
                break;
            case "Clochite":
                Instantiate(Ui_Clochite, transform.position, Quaternion.identity);
                break;
            case "Baie":
                Instantiate(UI_Baie, transform.position, Quaternion.identity);
                break;
            case "Fruit":
                Instantiate(Ui_Fruit, transform.position, Quaternion.identity);
                break;
            case "Plontite1":
                Instantiate(Ui_Plontite1, transform.position, Quaternion.identity);
                break;
            case "Poussite":
                Instantiate(Ui_Poussite, transform.position, Quaternion.identity);
                break;
            case "RecetteP":
                Instantiate(Ui_RecetteP, transform.position, Quaternion.identity);
                break;
            case "RecetteS":
                Instantiate(Ui_RecetteS, transform.position, Quaternion.identity);
                break;
            case "RecetteT":
                Instantiate(Ui_RecetteT, transform.position, Quaternion.identity);
                break;
            case "PotionDirect":
                Instantiate(Ui_PotionDirect, transform.position, Quaternion.identity);
                break;
            case "PotionOblique":
                Instantiate(Ui_PotionOblique, transform.position, Quaternion.identity);
                break;
            case "PotionSante":
                Instantiate(Ui_PotionSante, transform.position, Quaternion.identity);
                break;
            case "PotionTrampoplante":
                Instantiate(Ui_PotionTrampoplante, transform.position, Quaternion.identity);
                break;
            case "PotionPlonte":
                Instantiate(Ui_potionplonte, transform.position, Quaternion.identity);
                break;
            case "RecetteMD":
                Instantiate(Ui_RecetteMD, transform.position, Quaternion.identity);
                break;
            case "RecetteMO":
                Instantiate(Ui_RecetteMO, transform.position, Quaternion.identity);
                break;
        }
    }
    }
