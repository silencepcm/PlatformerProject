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

    public GameObject _player;
    float etatGourde;
    
    void Start()
    {
        etatGourde = _player.GetComponent<PlayerStatsScript>().Gourde;
    }

    void Update()
    {
        switch (etatGourde)
        {
            case 0:
                gourdeEtat1.SetActive(true);
                gourdeEtat2.SetActive(false);
                gourdeEtat3.SetActive(false);
                gourdeEtat4.SetActive(false);
                break;
            case 1:
                gourdeEtat1.SetActive(false);
                gourdeEtat2.SetActive(true);
                gourdeEtat3.SetActive(false);
                gourdeEtat4.SetActive(false);
                break;
            case 2:
                gourdeEtat1.SetActive(false);
                gourdeEtat2.SetActive(false);
                gourdeEtat3.SetActive(true);
                gourdeEtat4.SetActive(false);
                break;
            case 3:
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
