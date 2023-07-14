using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class EnableMovement : MonoBehaviour, IPointerExitHandler,IPointerClickHandler
{
   
    
    public void OnPointerClick(PointerEventData eventData)
    {
        
        PlayerControl.Local.canMove = false;
        PlayerControl.Local.characterAnimator.enabled = false;
  
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        PlayerControl.Local.canMove = true;
        
        PlayerControl.Local.characterAnimator.enabled = true;
        
        gameObject.GetComponent<InputField>().DeactivateInputField();


    }


}
