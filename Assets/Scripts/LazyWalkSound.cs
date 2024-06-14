using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyWalkSound : MonoBehaviour
{
    public Vector3 lastPos;
    public AudioClip defautlStepSound;
    public AudioSource AS;
    public DescriptiveMaterialManager MaterialManager;
    public float distanceRequired = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
        if (MaterialManager == null) { MaterialManager = GameObject.Find("Descriptive Material Manager").GetComponent<DescriptiveMaterialManager>(); }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, lastPos) > distanceRequired)
        {
            lastPos = transform.position;
            RaycastHit rch;
            if (Physics.Raycast(transform.position, -transform.up, out rch))
            {

                if (rch.collider.GetComponent<ObjectsMaterial>())
                {
                    ObjectsMaterial matty = rch.collider.GetComponent<ObjectsMaterial>();
                    if (matty.MyMaterial != DescriptiveMaterials.none)
                    {
                        AudioClip[] stepSounds = MaterialManager.ResolveMaterial(matty.MyMaterial).FootstepSounds;
                        if (stepSounds.Length > 0)
                        {
                            AS.PlayOneShot(stepSounds[0]);
                        }
                        else 
                        {
                            PlayDefaultSound();
                        }
                    }
                    else
                    {
                        PlayDefaultSound();
                    }
                }
            }
            else
            {
                PlayDefaultSound();
            }

        }
    }

    void PlayDefaultSound() { AS.PlayOneShot(defautlStepSound); }
}
