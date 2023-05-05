using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class Chair : NetworkBehaviour
{

    public NetworkBool isSitting;
    public Canvas SittingCanvas;
    
    public override void FixedUpdateNetwork()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            isSitting = true;
            SittingCanvas.gameObject.SetActive(true);
            Debug.Log("sitting");

        }
    }
}
