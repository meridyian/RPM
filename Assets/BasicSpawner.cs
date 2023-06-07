using System.Collections.Generic;
using Fusion;
using System;
using Fusion.Sockets;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;


public class BasicSpawner : SimulationBehaviour,IPlayerJoined
{
    //[SerializeField] private NetworkPrefabRef _playerPrefab;
    [SerializeField] GameObject _playerPrefab;
    public Chair chair;
    
    public void PlayerJoined(PlayerRef player)
    {
        if (player==Runner.LocalPlayer)
        {
            Runner.Spawn(_playerPrefab, new Vector3(0, 0f, 0), quaternion.identity, player);

        }
    }
    
}