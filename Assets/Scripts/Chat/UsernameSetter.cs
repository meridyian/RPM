using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class UsernameSetter : MonoBehaviour
{
    [SerializeField] private NetworkRunner _networkRunnerPrefab = null;
    [SerializeField] private PlayerData _playerDataPrefab = null;

    [SerializeField] private InputField _userName = null;
    [SerializeField] private Text _nickNamePlaceHolder;

    //[SerializeField] private InputField _roomName = null;
    [SerializeField] private string _gameSceneName = null;
    
    private NetworkRunner _runnerInstance;
    //public GameObject joinButton;

    private PlayerControls playerControls;

    public void OnEnable()
    {
        playerControls = new PlayerControls();
        playerControls.UI.Enable();
        playerControls.UI.Submit.performed += StartSharedSession;
    }
    
    

    public void OnDisable()
    {
        playerControls.UI.Disable();
        playerControls.UI.Submit.performed -= StartSharedSession;
    }
    

    public void StartSharedSession(InputAction.CallbackContext context)
    {
        SetPlayerData();
        StartGame(GameMode.Shared, _gameSceneName);
    }
    
    
    private void SetPlayerData()
    {
        var playerData = FindObjectOfType<PlayerData>();
        if (playerData == null)
        {
            playerData = Instantiate(_playerDataPrefab);
        }

        if (string.IsNullOrWhiteSpace(_userName.text))
        {
            playerData.SetUserName(_nickNamePlaceHolder.text);
        }
        else
        {
            playerData.SetUserName(_userName.text);

        }
    }

    private async void StartGame(GameMode mode, string sceneName)
    {
        _runnerInstance = FindObjectOfType<NetworkRunner>();
        if (_runnerInstance == null)
        {
            _runnerInstance = Instantiate(_networkRunnerPrefab);
        }

        _runnerInstance.ProvideInput = true;

        var startGameArgs = new StartGameArgs()
        {
            GameMode = mode,
            //sObjectPool = _runnerInstance.GetComponent<NetworkObjectPoolDefault>(),
        };
        await _runnerInstance.StartGame(startGameArgs);
        _runnerInstance.SetActiveScene(sceneName);
    }
    

}
