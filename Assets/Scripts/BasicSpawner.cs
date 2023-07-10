using System.Collections.Generic;
using Fusion;
using System;
using Fusion.Sockets;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;



public class BasicSpawner : SimulationBehaviour,IPlayerJoined
{
    //[SerializeField] private NetworkPrefabRef _playerPrefab;
    [SerializeField] GameObject _playerPrefab;
    
    public Chair chair;
    /*
    public Canvas usernameCanvas;
    public Text usernameInput;
    public string username = "Default";
    public bool isNameSet = false;
    */

    
    
    public void PlayerJoined(PlayerRef player)
    {
        if (player==Runner.LocalPlayer)
        {
            // if playername is given, set the playername and spawn the player
            Runner.Spawn(_playerPrefab, new Vector3(0, 0.5f, 0), quaternion.identity, player);
        }

    }
    /*
    public void SetUsername()
    {
        username = usernameInput.text;
        isNameSet = true;
        usernameCanvas.gameObject.SetActive(false);
        
    }
    */
    
}