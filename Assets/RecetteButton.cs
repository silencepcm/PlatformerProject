using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecetteButton : MonoBehaviour
{
    public GameObject ingridient1;
    public GameObject ingridient2;
    public GameObject NumberText;
    public int nbIng1, nbIng2;
    public void OnButtonPressed()
    {
        int nbing1 = int.Parse(ingridient1.GetComponent<TextMeshProUGUI>().text);
        int nbing2 = int.Parse(ingridient2.GetComponent<TextMeshProUGUI>().text);
        if((nbing1>=nbIng1)&& (nbing2 >= nbIng2))
        {
            nbing1 -= nbIng1;
            nbing2 -= nbIng2;
            ingridient1.GetComponent<TextMeshProUGUI>().text = nbing1.ToString();
            ingridient2.GetComponent<TextMeshProUGUI>().text = nbing2.ToString();
            int res = int.Parse(NumberText.GetComponent<TextMeshProUGUI>().text) + 1;
            NumberText.GetComponent<TextMeshProUGUI>().text = res.ToString();
        }
    }
}
