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
        //GetComponentInParent<PlayerControl>().charController.enabled = false;
        //GetComponentInParent<PlayerControl>().characterAnimator.SetBool("Sit", true);
        //GetComponentInParent<PlayerControl>().IsSitting = true;
        gameObject.SetActive(false);
     

        //chair.isEmpty = false;
    }
    


    
}
