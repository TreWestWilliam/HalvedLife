using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMovable : MonoBehaviour
{
    public Rigidbody rb;

    public void Start()
    {
        if (rb == null) 
        {
            rb = gameObject.GetComponent<Rigidbody>();
        }
    }

    public void OnHit(GameObject hand)  
    {
        rb.AddForce( new Vector3(transform.position.x - hand.transform.position.x,
            transform.position.y - hand.transform.position.y,
            transform.position.z - hand.transform.position.z).normalized*100  );
        //Debug.Log(Vector3.Cross(hand.transform.position, transform.position));
    }
}
