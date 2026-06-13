using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroncScript : MonoBehaviour
{
public void Activate()
    {
        GetComponent<Animator>().SetTrigger("Start");
    }
}
