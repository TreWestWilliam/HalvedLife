using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShotRelayable : MonoBehaviour
{
    public UnityEvent ShotEvent = new UnityEvent();

    public void Start()
    {
        
    }

    public virtual void OnRelayed() 
    {
        ShotEvent.Invoke();
    
    }

}
