using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggCook : MonoBehaviour
{
    private bool cookedEgg = false;
    public GameObject FriedEggObject;
    public AudioSource MySource;
    public AudioClip FrySound;


    // Start is called before the first frame update
    void Start()
    {
        FriedEggObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!cookedEgg) 
        {
            if (collision.gameObject.name.Equals("egg"))
            {
                //We're cooking!
                cookedEgg = true;
                // Remove the egg game object from existence
                Destroy(collision.gameObject);
                // Trade it for the fried egg
                FriedEggObject.SetActive(cookedEgg);
                //Play the cooking sound since there's no animation!
                MySource.PlayOneShot(FrySound);
            }
        }
        
    }

}
