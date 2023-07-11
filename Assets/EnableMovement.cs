using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class EnableMovement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
   

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayerControl.Local.canMove = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PlayerControl.Local.canMove = true;
    }
}
