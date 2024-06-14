using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HandSnappable : MonoBehaviour
{

    [Header("Hand Management")]
    public Transform HandTarget;
    protected GameObject HandObject;
    protected RightHandManager HandManager;
    protected HealthSystem HS;
    public float distanceSnapped = 5f;
    protected bool IsSnapped;
    public GameObject ShowHand;

    public bool TrySnap(GameObject other) 
    {
        if (Vector3.Distance(other.transform.position, transform.position) < distanceSnapped) 
        {
            HS = other.GetComponentInParent<HealthSystem>();
            RightHandManager HM = other.GetComponentInChildren<RightHandManager>();
            HandManager = HM;
            HM.HandTargetPosition(HandTarget);
            if (!IsSnapped) 
            {
                OnHandSnapped(other);
            }
            

            IsSnapped = true;
            return true;
        }
        return false;
    }

    public virtual void OnHandSnapped(GameObject other) { ShowHand.SetActive(true); }
    public virtual void OnHandUnsnapped(GameObject other) { ShowHand.SetActive(false); IsSnapped = false; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsSnapped) 
        {
            if (Vector3.Distance(HandObject.transform.position, transform.position) > distanceSnapped) 
            {
                Debug.Log("Hand Unsnapped", gameObject);
                IsSnapped = false;
                OnHandUnsnapped(HandObject);
                HandManager.HandReturnPosition();
            }
        }
    }
}
