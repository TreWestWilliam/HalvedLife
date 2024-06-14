using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthSystem : MonoBehaviour
{
    private float health = 100.0f;
    public TMP_Text text;
    public float showHealth;
    public float maxHealth = 100f;
    public float StartHealth = 100f;
    public float GetHealth() { return health; }

    [Header("Sound stuff")]
    public AudioSource AS;
    public AudioClip DamageClip;
    public AudioClip HealClip;
    public AudioClip ReloadClip;


    public void Damage(float damage) 
    {
        health -= Mathf.Abs(damage);
        UpdateHealth();
    }

    public void DamageEnemy(float damage) 
    {
        AS.PlayOneShot(DamageClip);
        Damage(damage);
    }
    public void DamageReload(float damage) 
    {
        AS.PlayOneShot(ReloadClip);
        Damage(damage);
    }

    public void HalfHealth() 
    {
        health /= 2;
        UpdateHealth();
    }

    public void Heal(float heal) 
    {
        health += Mathf.Abs(heal);
        if (health > maxHealth) { health = maxHealth; }
        UpdateHealth();
    }

    public void UpdatePreview() 
    {
        showHealth = health;
    }

    protected void UpdateHealth() 
    {
        ///TODO: Update health text
        ///
        text.text = $"HEALTH:{health}";
        showHealth = health;
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
