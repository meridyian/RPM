using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Chair : NetworkBehaviour
{
    [Networked(OnChanged = nameof(NetworkedChairChanged))]
    public bool IsChairFull{ get; set; }

    public string X;

    public void Update()
    {
        if (Object != null)
        {
            X = Object.StateAuthority.ToString();
        }
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void DealSittingRpc(bool sitting)
    {
        Debug.Log("Received DealSittingRpc on StateAuthority, modifying Networked variable");
        IsChairFull = sitting;
    }

    private static void NetworkedChairChanged(Changed<Chair> changed)
    {
        Debug.Log($"$chair changed to:{changed.Behaviour.IsChairFull}");
    }
    
    


}