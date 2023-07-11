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

    
#if (UNITY_2018_3_OR_NEWER && UNITY_ANDROID)
    private ArrayList permissionList = new ArrayList() { Permission.Microphone };
#endif
    
    private string _appID = "f8a7883623ad4990817c6dc33b74ff29";
    private string _channelName = "x_channel";
    private string _token = "007eJxTYFjFbs4amX2AO8Yx6lRm2aHYR09+F81fF52Q08b3agvvfV0FhjSLRHMLC2MzI+PEFBNLSwMLQ/Nks5RkY+Mkc5O0NCNLC+a1KQ2BjAw/H29nYmSAQBCfk6EiPjkjMS8vNYeBAQCweyEp";
    
    //wrapper to AgoraSDK 
    internal IRtcEngine RtcEngine;


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
    void OnApplicationQuit()
    {
        if (RtcEngine != null)
        {
            Leave();
            RtcEngine.Dispose();
            RtcEngine = null;
        }
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
    }
    private void SetupUI()
    {
        GameObject go = GameObject.Find("Leave");
        go.GetComponent<Button>().onClick.AddListener(Leave);
        go = GameObject.Find("Join");
        go.GetComponent<Button>().onClick.AddListener(Join);
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
        RtcEngine.JoinChannel(_token, _channelName);
    }

    public void Leave()
    {
        // Leaves the channel.
        RtcEngine.LeaveChannel();
        // Disable the audio modules.
        RtcEngine.DisableAudio();
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
        }
    }




}
