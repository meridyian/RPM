using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;


public class EnableMovement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayerControl.Local.canMove = false;
        PlayerControl.Local.characterAnimator.enabled = false;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        PlayerControl.Local.canMove = true;
        PlayerControl.Local.characterAnimator.enabled = true;
    }
}
