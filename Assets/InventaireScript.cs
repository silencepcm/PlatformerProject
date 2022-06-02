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
    public int Plontite1;

    public int NbMunitionDirect;
    public int NbMunitionOblique;
    public int NbPotionSante;
    public int NbTrampoplante;

    public int RecetteMunitionDirect;
    public int RecetteMunitionOblique;
    public int RecettePotionSante;
    public int RecetteTrampoplante;

    // Start is called before the first frame update
    void Start()
    {
        Munitite = 0;
        Directite = 0;
        Clochite = 0;
        Baie = 0;
        Fruit = 0;
        Poussite = 0;
        Plontite1 = 0;
        NbMunitionDirect = 5;
        NbMunitionOblique = 5;
        RecetteMunitionDirect = 1;
        RecetteMunitionOblique = 1;
        RecettePotionSante = 0;
        RecetteTrampoplante = 0;
    }

}
