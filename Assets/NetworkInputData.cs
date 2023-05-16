using Fusion;
using UnityEngine;



public struct NetworkInputData : INetworkInput
{

   public Vector3 direction;

   public NetworkBool HiphopAnim;
   public NetworkBool SillyDanceAnim;
   public NetworkBool TalkingAnim;

   public NetworkBool isLeftClickPressed;
   [Networked]
   public bool stand { get; set; }
   
}
