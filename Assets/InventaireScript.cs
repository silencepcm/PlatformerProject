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
    public int Sac;

    public int NbMunitionDirect;
    public int NbMunitionOblique;
    public int NbPotionSanté;
    public int NbTrampoplante;

    public int RecetteMunitionDirect;
    public int RecetteMunitionOblique;
    public int RecettePotionSanté;
    public int RecetteTrampoplante;

    // Start is called before the first frame update
    void Start()
    {
        Sac = 0;
        Munitite = 0;
        Directite = 0;
        Clochite = 0;
        Baie = 0;
        Fruit = 0;
        Poussite = 0;
        Plontite1 = 0;
        NbMunitionDirect = NbMunitionDirect;
        NbMunitionOblique = NbMunitionOblique;
        RecetteMunitionDirect = 1;
        RecetteMunitionOblique = 1;
        RecettePotionSanté = 0;
        RecetteTrampoplante = 0;
    }

}
