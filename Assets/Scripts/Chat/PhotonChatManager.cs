using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Fusion;
using UnityEngine;
using Photon.Chat;
using ReadyPlayerMe.WebView;
using Unity.VisualScripting;
using UnityEngine.Networking.PlayerConnection;
using UnityEngine.UI;

public class PhotonChatManager : NetworkBehaviour, IChatClientListener
{
    // chatclient should be initalized
    ChatClient chatClient;
    //
    bool isConnected;
    [SerializeField] string username;
    
    // hold the list of online players and usernames 
    
    // chat setup
    [SerializeField] GameObject chatPanel;
    string currentChat;
    [SerializeField] InputField chatField;
    [SerializeField] Text chatDisplay;

    public static PhotonChatManager photonChatManager;
    
    // to keep track of private chat field
    string privateReceiver = "";
    
    // IChatClientListener is the interface applying the callbacks from chat
    // add mask to scroll view so that the chat is only displayed in scroll view
    
    public void Awake()
    {
        if (photonChatManager != null) return;
        photonChatManager = this;
        
    }
    
     // Start is called before the first frame update
    public void ChatConnectOnClick()
    {
        /*
        if (Object.HasStateAuthority)
        {
             username = PlayerData.Instance._userName;
          
        }
        */
        username = PlayerData.Instance._userName;
        // is attached to JoinChat button OnClick event
        isConnected = true;
        chatClient = new ChatClient(this);
        // Fusion.Photon.Realtime.PhotonAppSettings.AppSettings instance şeklinde çekemezsin
        // if you call the Connect on Start you can not dynamically set your username 
        Fusion.Photon.Realtime.AppSettings settings = Fusion.Photon.Realtime.PhotonAppSettings.Instance.AppSettings;
        chatClient.Connect(settings.AppIdChat, settings.AppVersion, new AuthenticationValues(username));
        Debug.Log("Connecting");
        
    }
    
    // set the username
    


    // Update is called once per frame
    public  override void FixedUpdateNetwork()
    {
        // if you call the service before connecting, it throws an error. 
        // check connection state
        if (isConnected)
        {
            //maintains connection, needs to be called in Update
            chatClient.Service();
        }

        if (chatField.text != "" && Input.GetKey(KeyCode.Return))
        {
            SubmitPublicChatOnClick();
            SubmitPrivateChatOnClick();
        }

        
    }
    
    #region PublicChat
    public void SubmitPublicChatOnClick()
    {
        // check if you are writing in private mode
        if (privateReceiver == "")
        {
            chatClient.PublishMessage("RegionChannel", currentChat);
            // clear the field
            chatField.text = "";
            currentChat = "";
 
        }
    }
    
    // attached to chatInput input field
    public void TypeChatOnValueChange(string valueIn)
    {
        
        currentChat = valueIn;
    }
    #endregion PublicChat
    
    
    #region PrivateChat
    public void ReceiverOnValueChange(string valueIn)
    {
        privateReceiver = valueIn;
    }
    
    public void SubmitPrivateChatOnClick()
    {
        if (privateReceiver != "")
        {
            chatClient.SendPrivateMessage(privateReceiver, currentChat);
            chatField.text = "";
            currentChat = "";
        }
    }
   

    #endregion PrivateChat

    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log("x");
    }

    public void OnDisconnected()
    {
        throw new System.NotImplementedException();
    }

    //comes from interface
    public void OnConnected()
    {
        // only able to connect to one region limitation
        Debug.Log("Connected");
        isConnected = true;
    
        // subscribing to a specific chatroom
        chatClient.Subscribe(new string[] { "RegionChannel" });
    }

    public void OnChatStateChange(ChatState state)
    {
        //throw new System.NotImplementedException();
        //Debug.Log("Connected");
        //isConnected = true;
        //joinChatButton.SetActive(false);
    }

    // to be able to display the public chat in the scroll view
    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        string msgs = "";
        for (int i = 0; i < senders.Length; i++)
        {
            msgs = string.Format("{0}: {1}", senders[i], messages[i]);
            chatDisplay.text += "\n" + msgs;
            Debug.Log(msgs);
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        string msgs = "";
        msgs = string.Format("(Private) {0}: {1}", sender, message);
        chatDisplay.text += "\n" + msgs;
        Debug.Log(msgs);
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        chatPanel.SetActive(true);
    }

    public void OnUnsubscribed(string[] channels)
    {
        throw new System.NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }
}
