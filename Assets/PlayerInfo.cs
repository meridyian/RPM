using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{

    [SerializeField] string username;
    
    public void UsernameOnValueChange(string valueIn)
    {
        // is attached to the username input field to keep track of the usernames passed in
        Debug.Log("user is typing username");
        username = valueIn;
    }    
}
