using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GourdeUI : MonoBehaviour
{
    public GameObject gourdeEtat1;
    public GameObject gourdeEtat2;
    public GameObject gourdeEtat3;
    public GameObject gourdeEtat4;

    public GameObject GameManager;
    float etatGourde;
    
    void Start()
    {
        etatGourde = GameManager.GetComponent<SurvieScript>().SliderGourde.value;
    }

    void Update()
    {
        etatGourde = GameManager.GetComponent<SurvieScript>().SliderGourde.value;
        switch (etatGourde)
        {
            case 0f:
                gourdeEtat1.SetActive(true);
                gourdeEtat2.SetActive(false);
                gourdeEtat3.SetActive(false);
                gourdeEtat4.SetActive(false);
                break;
            case 20f:
                gourdeEtat1.SetActive(false);
                gourdeEtat2.SetActive(true);
                gourdeEtat3.SetActive(false);
                gourdeEtat4.SetActive(false);
                break;
            case 40f:
                gourdeEtat1.SetActive(false);
                gourdeEtat2.SetActive(false);
                gourdeEtat3.SetActive(true);
                gourdeEtat4.SetActive(false);
                break;
            case 60f:
                gourdeEtat1.SetActive(false);
                gourdeEtat2.SetActive(false);
                gourdeEtat3.SetActive(false);
                gourdeEtat4.SetActive(true);
                break;
            default:
                gourdeEtat1.SetActive(false);
                gourdeEtat2.SetActive(false);
                gourdeEtat3.SetActive(false);
                gourdeEtat4.SetActive(false);
                break;

        }
    }
}
