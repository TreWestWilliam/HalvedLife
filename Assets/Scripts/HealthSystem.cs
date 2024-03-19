using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthSystem : MonoBehaviour
{
    private float health = 100.0f;
    public TMP_Text text;
    public float showHealth;
    public float StartHealth = 100f;
    public float GetHealth() { return health; }

    public void Damage(float damage) 
    {
        health -= Mathf.Abs(damage);
        UpdatePreview();
    }

    public void HalfHealth() 
    {
        health /= 2;
        UpdatePreview();
    }

    public void UpdatePreview() 
    {
        showHealth = health;
    }

    protected void UpdateHealth() 
    { 
        ///TODO: Update health text
    }

    // Start is called before the first frame update
    void Start()
    {
        health = StartHealth;
        showHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
