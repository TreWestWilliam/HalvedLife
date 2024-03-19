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
    public int AmmoCount;
    public int MaxAmmo;
    
    

    [Header("Interaction Objects")]
    public GameObject PistolObject;
    public GameObject RayInteractorObject;
    public GameObject HandObject;
    public GameObject WeaponUI;

    //TODO : PROPER STATE MACHINE FOR GUN/TOOLS/HAND

    private void ShootPistol() 
    {
        RaycastHit ray;

        //Firing the Gun
        //Checking Ammo count first
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
            }
            AmmoCount--;
            UpdateAmmoCount();
        }
        else
        {
            // TODO FAILED FIRE SOUND???
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
    }

    public void ToggleState(RightHandStates rightState) 
    {
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
        HandObject.SetActive(false);
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
        Reload();
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(PistolTip.position, -PistolTip.forward);
        
      
        
    }

    public void MenuButton() 
    {
        Debug.Log("Menu Button Pressed");
    }

}

public enum RightHandStates 
{ 
    hand,
    pistol,
    tool,
    shotgun,
    uiselection,
    smg
}
