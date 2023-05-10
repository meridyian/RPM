using Fusion;
using UnityEngine;



public struct NetworkInputData : INetworkInput
{

   public Vector3 direction;

   public NetworkBool HiphopAnim;
   public NetworkBool SillyDanceAnim;
   public NetworkBool TalkingAnim;

   public NetworkBool isLeftClickPressed;
   public NetworkBool stand;
   
   public NetworkBool sit;

   

}
