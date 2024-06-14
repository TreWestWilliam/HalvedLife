using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnOnUse : MonoBehaviour
{
    public GameObject SpawnablePrefab;
    public GameObject CurrentInstance;

    public void Use() 
    {
        Destroy(CurrentInstance);
        CurrentInstance = GameObject.Instantiate(SpawnablePrefab, gameObject.transform);
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
