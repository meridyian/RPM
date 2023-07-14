using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if (UNITY_2018_3_OR_NEWER && UNITY_ANDROID)
using UnityEngine.Android;
#endif
using Agora.Rtc;


public class NewBehaviourScript : MonoBehaviour
{

    private string _appID = "08732cff0aa64ae2b6f73cb0b3cae713";
    private string _channelName = "x_channel";
    //private string _token = "007eJxTYCjaWbrs8oVEyeCVBhHxF29Pq55vurM+7tndRVsNl2ieerlPgSHNItHcwsLYzMg4McXE0tLAwtA82Swl2dg4ydwkLc3IspFhY0pDICPDmzfHGBihEMTnZKiIT85IzMtLzWFgAAC2+iRF";
    
    //wrapper to AgoraSDK 
    internal IRtcEngine RtcEngine;
    public GameObject VoiceOnButton;
    public GameObject VoiceOffButton;
    
    
    
#if (UNITY_2018_3_OR_NEWER && UNITY_ANDROID)
    private ArrayList permissionList = new ArrayList() { Permission.Microphone };
#endif
    // Start is called before the first frame update
    void Start()
    {
        SetupVoiceSDKEngine();
        InitEventHandler();
        SetupUI();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPermissions();
    }
    
    private void CheckPermissions() {
#if (UNITY_2018_3_OR_NEWER && UNITY_ANDROID)
    foreach (string permission in permissionList)
    {
        if (!Permission.HasUserAuthorizedPermission(permission))
        {
            Permission.RequestUserPermission(permission);
        }
    }
#endif

    void OnApplicationQuit()
    {
        if (RtcEngine != null)
        {
            Leave();
            RtcEngine.Dispose();
            RtcEngine = null;
        }
    }

    
    }
    private void SetupUI()
    {
        GameObject voiceonBut = VoiceOnButton;
        voiceonBut.GetComponent<Button>().onClick.AddListener(Join);
        GameObject voiceoffBut = VoiceOffButton;
        voiceoffBut.GetComponent<Button>().onClick.AddListener(Leave);
        
    }


    private void SetupVoiceSDKEngine()
    {
        // Create an RtcEngine instance.
        RtcEngine = Agora.Rtc.RtcEngine.CreateAgoraRtcEngine();
        RtcEngineContext context = new RtcEngineContext(_appID, 0, 
            CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_LIVE_BROADCASTING,
            AUDIO_SCENARIO_TYPE.AUDIO_SCENARIO_DEFAULT);
        // Initialize RtcEngine.
        RtcEngine.Initialize(context);
    }
    private void InitEventHandler()
    {
        // Creates a UserEventHandler instance.
        UserEventHandler handler = new UserEventHandler(this);
        RtcEngine.InitEventHandler(handler);
    }
    public void Join()
    {
        // Enables the audio module.
        RtcEngine.EnableAudio();
        // Sets the user role ad broadcaster.
        RtcEngine.SetClientRole(CLIENT_ROLE_TYPE.CLIENT_ROLE_BROADCASTER);
        // Joins a channel.
        RtcEngine.JoinChannel(null, _channelName);
        Debug.Log("player joined to chat");
    }

    public void Leave()
    {
        // Leaves the channel.
        RtcEngine.LeaveChannel();
        // Disable the audio modules.
        RtcEngine.DisableAudio();
        Debug.Log("player left from the chat");
    }

    internal class UserEventHandler : IRtcEngineEventHandler
    {
        private readonly NewBehaviourScript _audioSample;

        internal UserEventHandler(NewBehaviourScript audioSample)
        {
            _audioSample = audioSample;
        }

        // This callback is triggered when the local user joins the channel.
        public override void OnJoinChannelSuccess(RtcConnection connection, int elapsed)
        {
            Debug.Log("successful conection");
        }
    }




}
