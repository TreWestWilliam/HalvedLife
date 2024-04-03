using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRelayable : ShotRelayable
{
    public GameObject DoorObject;
    public Transform TargetPosition;
    private bool Working = false;
    public override void OnRelayed() 
    {
        Working = true;
        Debug.Log($"Door {gameObject.name} opening", gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (Working) 
        {
            //DoorObject.transform.Translate(TargetPosition.position * .1f * Time.deltaTime);

            DoorObject.transform.position = Vector3.MoveTowards(DoorObject.transform.position, TargetPosition.position, .01f);

            if (Vector3.Distance(DoorObject.transform.position, TargetPosition.position) < 0.001f)
            {
                Working = false;
            }
        }
    }
}
