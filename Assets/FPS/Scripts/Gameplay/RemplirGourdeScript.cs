using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemplirGourdeScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MainCamera")
        {
            GameManager.Instance.GetComponent<SurvieScript>().RemplirGourde();
        }
    }
}
