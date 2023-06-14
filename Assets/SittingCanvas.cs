using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using WebSocketSharp;


public class SittingCanvas : MonoBehaviour
{
    [Networked]
    public bool yesPressed{ get; set; }

    
    public void Close()
    {
        gameObject.SetActive(false);
    }
    
    // instance is used since it was assigning the playercontrol as the one having state authority
    public void YesPressed()
    {
        yesPressed = true;
        PlayerControl.Local.IsSitting = true;
        PlayerControl.Local.chairTransform = FindObjectOfType<Chair>().gameObject.transform;
        gameObject.SetActive(false);

    }
    


    
}
