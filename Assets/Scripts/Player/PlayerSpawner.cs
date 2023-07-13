using System.Collections;
using System.Collections.Generic;
using Fusion;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : SimulationBehaviour, ISpawned
{
    // References to the NetworkObject prefab to be used for the player's avatars
    [SerializeField] GameObject _playerPrefab;

    
    
    public void Spawned()
    {
        //SpawnPlayer(Runner.LocalPlayer);
        SpawnPlayer(Runner.LocalPlayer);
    }
    
    
    public void SpawnPlayer(PlayerRef player)
    {
        if (player==Runner.LocalPlayer)
        {
            // if playername is given, set the playername and spawn the player
            Runner.Spawn(_playerPrefab, new Vector3(0, 0.5f, 0), Quaternion.identity, player);
            
        }
    }
    


}


