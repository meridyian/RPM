using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public  Dictionary<PlayerRef, string> _userList = new Dictionary<PlayerRef, string>();

    public static GameManager gameManagerInstance;

    public void Awake()
    {
        if(gameManagerInstance != null) return;
        gameManagerInstance = this;
    }

    public void AddPlayer(PlayerRef player, PlayerDataNetworked playerDataNetworked)
    {
        if (playerDataNetworked == null) return;
        
        string user = playerDataNetworked.UserName;
        
        _userList.Add(player, user);
    }

}
