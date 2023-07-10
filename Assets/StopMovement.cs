using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopMovement : MonoBehaviour
{
    public GameObject[] inputFields;

    public void Update()
    {
        foreach (GameObject inputField in inputFields)
        {
            if (inputField.GetComponent<InputField>().isFocused)
            {
                PlayerControl.Local.IsTyping = true;
            }
        }
    }
    
    
}
