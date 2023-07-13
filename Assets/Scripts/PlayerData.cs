using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerData : MonoBehaviour
{
    public string _userName;

    public static PlayerData Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetUserName(string userName)
    {
        _userName = userName;
    }

    public string GetUserName()
    {
        if (string.IsNullOrWhiteSpace(_userName))
        {
            _userName = GetRandomUserName();
        }

        return _userName;
    }

    public static string GetRandomUserName()
    {
        var rngPlayerNumber = Random.Range(0, 7777);
        return $"Player {rngPlayerNumber.ToString("0000")}";
    }


}
