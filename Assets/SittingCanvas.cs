using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using WebSocketSharp;

public class SittingCanvas : NetworkBehaviour
{
    public Chair chair;

    
    public void Close()
    {
        gameObject.SetActive(false);
    }
    
    
    public void YesPressed()
    {
        gameObject.SetActive(false);
        //chair.isEmpty = false;
    }


    
}
