using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirActivateAnimation : MonoBehaviour
{
    public TourelleControllerScript script;
    // Start is called before the first frame update
    public void shoot()
    {
        script.Shoot();
    }
}
