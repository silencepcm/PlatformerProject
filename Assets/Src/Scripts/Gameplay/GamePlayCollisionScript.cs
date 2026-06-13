using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayCollisionScript : MonoBehaviour
{
    public bool WaterCollision;
    public bool CollectableCollision;

    private void Start()
    {
        WaterCollision = false;
        CollectableCollision = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            WaterCollision = true;
        } else if (other.CompareTag("Collectable"))
        {
            CollectableCollision = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            WaterCollision = false;
        }
        else if (other.CompareTag("Collectable"))
        {
            CollectableCollision = false;
        }
    }
}
