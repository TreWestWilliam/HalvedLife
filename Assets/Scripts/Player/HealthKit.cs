using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : MonoBehaviour
{

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        if (rb==null) { rb = gameObject.GetComponent<Rigidbody>(); }
    }

    public void OnCollisionEnter(Collision collision)
    {
        float totalVelocity = Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y) + Mathf.Abs(rb.velocity.z);
        if (totalVelocity > 1) 
        {
            Smash();
        }
    }

    public void Smash() 
    {
        Debug.Log("Add Smash Mechanic to Healthkits!!!");
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
