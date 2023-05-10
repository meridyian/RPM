using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class Chair : NetworkBehaviour
{
    [Networked]
    public bool IsChairFull{ get; set; }

    public string X;

    public void Update()
    {
        if (Object != null)
        {
            X = Object.StateAuthority.ToString();
        }
    }
}