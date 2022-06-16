using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.Instance.LastSpawn = transform.position;
            GameManager.Instance.LastRotation = transform.rotation;
        }
    }
}
