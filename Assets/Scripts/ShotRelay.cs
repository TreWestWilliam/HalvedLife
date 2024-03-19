using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This script is probably an unnecessary layer of abstraction for such a small project but oh well.
/// This is used to take an object being shot to interact with another seperate object
/// Aka A button gets shot, move a door.
/// I just wanted to make this plug into different things as needed
/// Also I only realized how Unity events was the better way to do this later, lol
/// </summary>
public class ShotRelay : ShotInteractable
{
    public UnityEvent OnShotEvent = new UnityEvent();
    public ShotRelayable shot;
    public override void OnHit()
    {
        base.OnHit();
        shot.OnRelayed();
        OnShotEvent.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
