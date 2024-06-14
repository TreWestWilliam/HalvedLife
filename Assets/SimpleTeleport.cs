using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTeleport : MonoBehaviour
{
    public GameObject Player;
    public Transform Target;

    public void Start()
    {
        if (Player == null) 
        {
            Player = GameObject.Find("XR Origin (XR Rig)");
        }
    }

    public void Warp() 
    {
        Player.transform.parent.position = Target.position;
        Player.transform.position = new Vector3(0, 0, 0);
    }
}
