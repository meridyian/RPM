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
    
    
    public void YesPressed()
    {
        yesPressed = true;
        PlayerControl.playerInstance.IsSitting = true;
        PlayerControl.playerInstance.chairTransform = FindObjectOfType<Chair>().gameObject.transform;
        gameObject.SetActive(false);

    }
    


    
}
