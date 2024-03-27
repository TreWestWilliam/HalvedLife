using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class HandScan : MonoBehaviour
{
    [Header("Hand Management")]
    public Transform HandTarget;

    private GameObject HandObject;
    private RightHandManager HandManager;
    private HealthSystem HS;
    [Space]
    [Header("Scan Variables")]
    public float ScanTime;
    public float ScanCost;
    public bool OnlyScanOnce;
    [SerializeReference]
    private float ScanTimer;
    [SerializeReference]
    private bool Scanning = false;
    [SerializeReference]
    private bool CanScan = true;
    [Space]
    [Header("Aesthetic")]
    public TMP_Text DisplayText;
    public string Description;
    public AudioClip CompletedChime;
    public AudioSource _AudioSource;
    private string UnusedText;
    [Space]

    public UnityEngine.Events.UnityEvent CompletedScan;

    public void OnTriggerEnter(Collider other)
    {
        HS = other.GetComponentInParent<HealthSystem>();
        RightHandManager HM = other.GetComponentInChildren<RightHandManager>();
        if (HM != null && CanScan) 
        {
            HandManager = HM;
            HandObject = other.gameObject;
            HM.HandTargetPosition(HandTarget);

            Scanning = true;
            ScanTimer = ScanTime;
            DisplayText.text = "Scanning...";
            Debug.Log("Found 1");
        }
        Debug.Log($"{other.name} entered", gameObject);
    }

    public void OnTriggerExit(Collider other)
    {
        RightHandManager HM = other.GetComponent<RightHandManager>();
        if (HM != null) 
        {
            HM.HandReturnPosition();
            Scanning = false;
        }
        
    }

    private void CompleteScan() 
    {
        CompletedScan.Invoke();

        if (OnlyScanOnce) { CanScan = false; }

        Scanning = false;

        DisplayText.text = $"Scan Complete";

        //Todo grab health system
        HS.Damage(ScanCost);

        if (CompletedChime != null) 
        {
            if (_AudioSource != null) 
            {
                _AudioSource.PlayOneShot(CompletedChime);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UnusedText = $"{Description} for {ScanCost} health";
        DisplayText.text = UnusedText;
    }

    // Update is called once per frame
    void Update()
    {
        if (Scanning) 
        {
            if (ScanTimer > 0) 
            {
                ScanTimer -= Time.deltaTime;
                DisplayText.text = $"Scanning...\n { ((ScanTimer / ScanTime) - 1) *-100}% Complete";
            }
            else 
            {
                CompleteScan();
            }
        }
    }
}
