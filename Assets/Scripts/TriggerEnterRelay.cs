using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEnterRelay : MonoBehaviour
{
    [Header("Filter Properties")]
    public List<GameObject> TargetObjects;
    public string[] TargetObjectNames;
    public string[] TargetObjectTags;

    [Header("Events")]
    public UnityEvent OnCorrectObjectEntered;
    public UnityEvent AnyObjectEntered;
    public UnityEvent OnCorrectObjectExit;
    public UnityEvent OnAnyObjectExit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool IsObjectInFilter(GameObject obj) 
    {
        if (TargetObjects.Contains(obj))
        {

            return true;
        }
        foreach (string s in TargetObjectNames)
        {
            if (s.Equals(obj.name))
            {
                return true;
            }
        }
        foreach (string s in TargetObjectTags)
        {
            if (obj.CompareTag(s))
            {
                return true;
            }
        }
        return false;
    }

    public void OnTriggerEnter(Collider other)
    {
        AnyObjectEntered.Invoke();
        if (IsObjectInFilter(other.gameObject)) 
        {
            OnCorrectObjectEntered.Invoke();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        OnAnyObjectExit.Invoke();
        if (IsObjectInFilter(other.gameObject)) 
        {
            OnCorrectObjectExit.Invoke();
        }
    }

}
