using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectUIScript : MonoBehaviour
{
    public GameObject CollectObjectUI;
    public Vector3 ActivatedPosition;
    Vector3 StartPos;
    bool activated;
    bool moving;
    Vector3 TargetPos;
    public float speed;
    void Start()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Unity.FPS.Gameplay.PlayerCharacterController>().OnCollectActivateUI += Activate;
        StartPos = CollectObjectUI.GetComponent<RectTransform>().localPosition;
        TargetPos = CollectObjectUI.GetComponent<RectTransform>().localPosition;
        activated = false;
        moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            CollectObjectUI.GetComponent<RectTransform>().localPosition = Vector3.Lerp(CollectObjectUI.GetComponent<RectTransform>().localPosition, TargetPos, speed * Time.fixedDeltaTime);
            if (Vector3.Distance(CollectObjectUI.GetComponent<RectTransform>().localPosition, TargetPos) < 0.1f)
            {
                moving = false;
            }
        }   
    }
    public void Activate(bool active)
    {
        if(active != activated)
        {
            moving = true;
            activated = active;
            if (active)
            {
                TargetPos = new Vector3(CollectObjectUI.GetComponent<RectTransform>().localPosition.x, -200f, CollectObjectUI.GetComponent<RectTransform>().localPosition.z);
            }
            else
            {
                TargetPos = StartPos;
            }
        }
    }
}
