using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
public class RightHandManager : MonoBehaviour
{
    public RightHandStates HandState = RightHandStates.hand;

    [Header("Input Variables")]
    public InputAction OpenWeaponMenu;
    public InputAction SecondaryAction;
    public InputAction TriggerPressed;

    [Header("Pistol Variables")]
    public Transform PistolTip;
    public Animator PistolAnimator;
    public TMP_Text AmmoCountDisplay;
    public AudioClip FailedShot;
    public AudioSource ASource;
    public int AmmoCount;
    public int MaxAmmo;
    public float ShotDelay;
    [SerializeReference]
    private float TimeTillNextShot;

    [Header("Testing Variables")]
    public float WeaponUISpawnDistance = 10.0f;

    [Header("Hand Animation")]
    public Animator HandAnimator;
    public XRRayInteractor XRray;
    [SerializeReference]
    private bool HandTargeting;
    public Transform HandTarget;
    public Transform DefaultHandTransform;
    public Transform HandTransformBackup;
    [Space]
    

    [Header("Interaction Objects")]
    public GameObject PistolObject;
    public GameObject RayInteractorObject;
    public GameObject HandObject;
    public GameObject WeaponUI;
    public HandSnappable CurrentlySnapped;
    public HealthSystem HSystem;


    //TODO : PROPER STATE MACHINE FOR GUN/TOOLS/HAND

    private void ShootPistol() 
    {
        RaycastHit ray;

        //Firing the Gun
        //Check if we're not delayed ( seperate from the next check so we dont spam the non firing sound)
        if (Time.time > TimeTillNextShot) 
        {
            //Checking Ammo count
            if (AmmoCount > 0)
            {
                //Tell the Animator we're firing
                PistolAnimator.SetBool("Firing", true);
                PistolAnimator.SetTrigger("Fire");
                //Check if it'll hit anything
                if (Physics.Raycast(PistolTip.position, -PistolTip.forward, out ray))
                {
                    Debug.Log($"Shot: {ray.collider.gameObject.name}");
                    ShotInteractable LetsInteract = ray.collider.gameObject.GetComponent<ShotInteractable>();
                    if (LetsInteract != null)
                    {
                        LetsInteract.OnHit();
                    }

                    if (ray.collider.CompareTag("Enemy")) 
                    {
                        ZombieAI parentai = ray.collider.GetComponentInParent<ZombieAI>();
                        if (parentai != null) 
                        {
                            parentai.Damage(gameObject);
                        }
                    
                    }

                    GunMovable GM = ray.transform.gameObject.GetComponent<GunMovable>();
                    if (GM != null) 
                    {
                        GM.OnHit(gameObject);
                    }


                }
                AmmoCount--;
                UpdateAmmoCount();
                TimeTillNextShot = Time.time + ShotDelay;
            }
            else
            {
                if (ASource != null)
                {
                    ASource.PlayOneShot(FailedShot);
                }
            }
        }
        
    }

    private void UpdateAmmoCount() 
    { 
        AmmoCountDisplay.text =  $"AMMO : {AmmoCount}";
    }
    //Persona 3 Reference (real) 
    private void Reload() 
    {
        AmmoCount = MaxAmmo;
        UpdateAmmoCount();
        HSystem.Damage(10.0f);
    }

    public void ToggleState(RightHandStates rightState) 
    {
        if (HandTargeting == true) { HandReturnPosition(); }

        HandState = rightState;
        RefreshState();
    
    }
    public void ToggleState(int IteratorOfState) { ToggleState((RightHandStates)IteratorOfState); }

    public void RefreshState()
    {
        DisableObjects();
        switch (HandState)
        {
            case (RightHandStates)0: break;
            case RightHandStates.pistol: PistolObject.SetActive(true); break;
            case RightHandStates.uiselection: WeaponUI.SetActive(true); RayInteractorObject.SetActive(true); break;
 
        }

    }

    
    private void DisableObjects() 
    {
        PistolObject.SetActive(false);
        RayInteractorObject.SetActive(false);
        //HandObject.SetActive(false);
        WeaponUI.SetActive(false);

    }

    private void Awake()
    {
        OpenWeaponMenu.performed += OpenWeaponMenu_performed;
        SecondaryAction.performed += SecondaryAction_performed;
        TriggerPressed.performed += TriggerPressed_performed;
    }

    private void TriggerPressed_performed(InputAction.CallbackContext obj)
    {
        //Debug.Log("Triggered!");
        switch (HandState) 
        {
            case RightHandStates.hand: break;
            case RightHandStates.pistol: ShootPistol();break;
            case RightHandStates.tool: break; // todo make tool thing i think maybe??

        }
    }

    private void SecondaryAction_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Secondary Button Used");
        // temporary reload please redo later :D 
        if (HandState == RightHandStates.pistol) 
        {
            Reload();
        }
        
    }

    private void OpenWeaponMenu_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Open Weapon Menu performed");
        MenuButton();
        ToggleState(RightHandStates.uiselection);
    }

    private void OnEnable()
    {
        OpenWeaponMenu.Enable();
        SecondaryAction.Enable();
        TriggerPressed.Enable();
    }
    private void OnDisable()
    {
        OpenWeaponMenu.Disable();
        SecondaryAction.Disable();
        TriggerPressed.Disable();
    }

    public void HandTargetPosition(Transform target) 
    { 
        if (HandState == RightHandStates.hand || HandState == RightHandStates.snapped) 
        {
            HandTargeting = true;
            HandObject.transform.SetPositionAndRotation(target.position, target.rotation);
        }
    }

    public void HandReturnPosition()
    {
        if (HandState == RightHandStates.hand || HandState == RightHandStates.snapped)
        {
            HandTargeting = false;
            HandObject.transform.SetPositionAndRotation(DefaultHandTransform.position, DefaultHandTransform.rotation);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //DefaultHandTransform = HandObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(PistolTip.position, -PistolTip.forward);
        if (HandState == RightHandStates.hand) 
        {
            RaycastHit handHit;
            if (Physics.Raycast(transform.position, transform.forward, out handHit)) 
            {
                HandSnappable HS = handHit.collider.GetComponent<HandSnappable>();
                if (HS != null)
                {
                    Debug.Log($"Hit a snappable.  Distance:{handHit.distance} Required:{HS.distanceSnapped}", gameObject);
                    if (HS.TrySnap(gameObject))
                    {
                        CurrentlySnapped = HS;
                        
                        ToggleState(RightHandStates.snapped);
                    }
                }
            }
        }
        else if (HandState == RightHandStates.snapped) 
        {
            RaycastHit handHit;
            if (Physics.Raycast(transform.position, transform.forward, out handHit))
            {
                HandSnappable HS = handHit.collider.GetComponent<HandSnappable>();
                if (HS != null)
                {
                    if (CurrentlySnapped == HS)
                    {
                        //Debug.Log($"Attached to a snappable.  Distance:{handHit.distance} Required:{HS.distanceSnapped}", gameObject);
                        HS.TrySnap(gameObject);
                        HandTargetPosition(HS.HandTarget);
                    }
                    else
                    {
                        ToggleState(RightHandStates.hand);
                        HandReturnPosition();
                        CurrentlySnapped.OnHandUnsnapped(gameObject);
                        HandTargeting = false;
                    }
                }
                else
                {
                    ToggleState(RightHandStates.hand);
                    HandReturnPosition();
                    CurrentlySnapped.OnHandUnsnapped(gameObject);
                    HandTargeting = false;
                }
            }
        }
      
        
    }

    public void MenuButton() 
    {
        Debug.Log("Menu Button Pressed");
        //Vector3 UIPOS = Camera.main.transform.position + (Camera.main.transform.forward * WeaponUISpawnDistance);
        Vector3 UIPOS = transform.position + (transform.forward * WeaponUISpawnDistance);
        UIPOS.y = transform.position.y;
        WeaponUI.transform.position = UIPOS;
        WeaponUI.transform.rotation = Quaternion.LookRotation(UIPOS-transform.position);
    }



}

public enum RightHandStates 
{ 
    hand,
    pistol,
    tool,
    shotgun,
    uiselection,
    smg,
    snapped
}
