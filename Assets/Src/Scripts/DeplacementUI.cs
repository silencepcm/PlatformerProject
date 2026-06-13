using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementUI : MonoBehaviour
{
    
    public GameObject moi;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       transform.position = Vector3.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("InventaireUi").transform.position, 2000 * Time.deltaTime);
       // Debug.Log(transform.position);
        float Dist = Vector3.Distance(GameObject.FindGameObjectWithTag("InventaireUi").transform.position, transform.position);
       // Debug.Log(Dist);
        if (  Dist <= 20f )
        {
            Debug.Log("destruction");
            Destroy(moi);
        }
    }
}
