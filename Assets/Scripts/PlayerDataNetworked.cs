using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataNetworked : NetworkBehaviour
{

    [Networked(OnChanged = nameof(UsernameChanged))]
    public string UserName { get;  set; }
    
    public Text _playernameEntryText;
    private GameManager _gameManager;
    
    public override void Spawned()
    {
        _playernameEntryText.text = UserName;
        if (Object.HasStateAuthority)
        {
            var userName = FindObjectOfType<PlayerData>().GetUserName();
            DealNameRpc(userName);
            _playernameEntryText.text = UserName;

        }

        /*
        foreach (PlayerRef player in Runner.ActivePlayers)
        {
 
            Debug.Log(plobj.GetComponent<PlayerDataNetworked>().UserName);
        }
        //_gameManager = FindObjectOfType<GameManager>();
        //_gameManager.AddPlayer(Object.InputAuthority, this);
*/
        
    }

    private static void UsernameChanged(Changed<PlayerDataNetworked> changed)
    {
        changed.Behaviour.UserName = changed.Behaviour._playernameEntryText.text;
    }
    
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void DealNameRpc(string name)
    {
        UserName = name;
    }

}
