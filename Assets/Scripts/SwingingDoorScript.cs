using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingDoorScript : MonoBehaviour
{
    [Header("Door Variables")]
    public Rigidbody rb;
    public bool IsLocked;
    public float ForceDoorForce;

    [Header("Sound stuff")]
    public AudioSource aSource;
    public AudioClip UnlockSound;
    public AudioClip LockSound;
    // Start is called before the first frame update
    void Start()
    {
        //Ensuring we have a rigid body to target
        if (rb == null) { rb = GetComponent<Rigidbody>(); }
        if ( rb == null) 
        {
            Debug.Log("No Rigidbody attached, none referenced.", gameObject);
            return;
        }
        // This makes sure that our bool is in the correct state
        if (IsLocked) { LockDoorSilent(); }
        else { UnlockDoorSilent(); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnlockDoor() 
    {
        rb.isKinematic = false;
        IsLocked = false;
        if (aSource != null && UnlockSound != null) 
        {
            aSource.PlayOneShot(UnlockSound);
        }
        
    }

    public void SwingDoor() 
    {
        rb.AddForce(transform.right * ForceDoorForce);
    }
    public void LockDoor()
    {
        IsLocked = true;
        rb.isKinematic = true;
        if (aSource != null && LockSound != null)
        {
            aSource.PlayOneShot(LockSound);
        }
    }
    public void LockDoorSilent()
    {
        IsLocked = true;
        rb.isKinematic = true;
    }
    public void UnlockDoorSilent()
    {
        rb.isKinematic = false;
        IsLocked = false;
    }
}
