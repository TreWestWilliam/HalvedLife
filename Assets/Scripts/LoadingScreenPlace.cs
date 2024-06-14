using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenPlace : MonoBehaviour
{
    void Start()
    {
        GameObject.Find("Loading Manager").GetComponent<SimpleLevelLoading>().LoadingScenePlacement = transform;
    }
}
