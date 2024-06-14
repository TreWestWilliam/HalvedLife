using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScenePlace : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Loading Manager").GetComponent<SimpleLevelLoading>().FinalScenePlacement = transform;  
    }
}
