using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BasicLadder : MonoBehaviour
{
    [Header("Control Variables")]
    public float ClimbingTime = 5.0f;
    public Transform ClimbedPosition;
    public bool IsClimbing = false;
    private float FinishTime;

    [Header("Necessary")]
    public GameObject Player;
    public TMP_Text PercentText;
    public GameObject UIStuff;


    // Start is called before the first frame update
    void Start()
    {
        if (Player == null) 
        {
            Player = GameObject.Find("XR Origin (XR Rig)");
        }
        UIStuff.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float climbPer = FinishTime - Time.time;
        if (IsClimbing) 
        {
            float percentage = ((1-(climbPer/ClimbingTime)) *100);
            PercentText.text = $"{percentage}%";
            if (climbPer < 0) 
            {
                Player.transform.position = ClimbedPosition.position;
            }

        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            ActivateUI();
        }
        Debug.Log($"{other.name}");
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DeactivateUI();
        }
    }

    public void ActivateUI() 
    {
        UIStuff.SetActive(true);
        FinishTime = Time.time + ClimbingTime;
        PercentText.text = "0%";
        IsClimbing = true;
    }
    public void DeactivateUI() 
    {
        UIStuff.SetActive(false);
        IsClimbing = false;
    }
}
