using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MintyRoomScript : MonoBehaviour
{
    [Header("Necessary Items")]
    public AudioSource My_AudioSource;
    public AudioClip MintyFreshToes;
    public Transform BottleCenter;
    public GameObject Bottle;
    public int BottlesToSpawn = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCoolStuff() 
    {
        My_AudioSource.PlayOneShot(MintyFreshToes);
        for (int i = 0; i < BottlesToSpawn; i++) 
        {
            GameObject.Instantiate(Bottle, new Vector3(BottleCenter.position.x + Random.Range(-1, 1), BottleCenter.position.y, BottleCenter.position.z + Random.Range(-1, 1)), BottleCenter.rotation);
        }
    }
}
