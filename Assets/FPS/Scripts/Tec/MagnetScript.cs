using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetScript : MonoBehaviour
{
    public float magnetForce = 100;
    readonly List<Rigidbody> caughtRigidbodies = new List<Rigidbody>();

    void FixedUpdate()
    {
        for (int i = 0; i < caughtRigidbodies.Count; i++)
        {
            caughtRigidbodies[i].velocity = (transform.position - (caughtRigidbodies[i].transform.position + caughtRigidbodies[i].centerOfMass)) * magnetForce * Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            Rigidbody r = other.GetComponent<Rigidbody>();

            if ((!caughtRigidbodies.Contains(r))&&(r.gameObject.tag == "Magnet"))
            {
                //Add Rigidbody
                caughtRigidbodies.Add(r);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            Rigidbody r = other.GetComponent<Rigidbody>();

            if (caughtRigidbodies.Contains(r))
            {
                //Remove Rigidbody
                caughtRigidbodies.Remove(r);
            }
        }
    }
}
