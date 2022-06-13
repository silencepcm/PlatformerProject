using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventaireScript : MonoBehaviour
{
    public int Munitite;
    public int Directite;
    public int Clochite;
    public int Baie;
    public int Fruit;
    public int Poussite;
    public int Plontite;

    public int NbPotionDirect;
    public int NbPotionOblique;
    public int NbPotionSante;
    public int NbPotionTrampoplante;
    public int NbPotionPlonte;

    public int RecetteMunitionDirect;
    public int RecetteMunitionOblique;
    public int RecettePotionSante;
    public int RecetteTrampoplante;
    public int RecettePlonte;

    // Start is called before the first frame update
    void Start()
    {
        Munitite = 0;
        Directite = 0;
        Clochite = 0;
        Baie = 0;
        Fruit = 0;
        Poussite = 0;
        Plontite = 0;
        NbPotionDirect = 5;
        NbPotionOblique = 5;
        NbPotionSante = 0;
        NbPotionTrampoplante = 1;
        NbPotionPlonte = 0;
        RecetteMunitionDirect = 1;
        RecetteMunitionOblique = 1;
        RecettePotionSante = 0;
        RecetteTrampoplante = 0;
        RecettePlonte = 0;
    }

}
