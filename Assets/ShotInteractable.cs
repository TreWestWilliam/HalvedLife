using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotInteractable : MonoBehaviour
{

    public virtual void OnHit() 
    {
        Debug.Log($"{gameObject.name} Was hit and you probably didnt update it's extended shot interactable.");
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
