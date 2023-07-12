using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public string _userName;

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
