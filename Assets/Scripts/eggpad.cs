using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class eggpad : MonoBehaviour
{
    private string code = "";
    public string display = "";
    private string solution = "EGG";

    public TMP_Text Output;

    public UnityEvent OnCodeSolved;

    public void TypeE() 
    {
        code += "E";
        CheckCode();
    }
    public void TypeG() 
    {
        code += "G";
        CheckCode();
    }

    private void CheckCode() 
    {
        Output.text = code;
        if (code.Equals(solution)) 
        {
            OnCodeSolved.Invoke();
        }
        if (code.Length > 4) 
        {
            code = "";
            Output.text = "RETRY";
        }
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
