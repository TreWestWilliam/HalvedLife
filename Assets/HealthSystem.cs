using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthSystem : MonoBehaviour
{
    private float health;
    public TMP_Text text;

    public float GetHealth() { return health; }

    public void Damage(float damage) 
    {
        health -= Mathf.Abs(damage);
    }

    public void HalfHealth() 
    {
        health /= 2;
    }

    protected void UpdateHealth() 
    { 
        ///TODO: Update health text
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
