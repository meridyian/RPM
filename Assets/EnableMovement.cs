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
    private bool inPanel;

    public void Update()
    {
        if (!inPanel)
        {
            this.gameObject.GetComponent<InputField>().DeactivateInputField();
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        inPanel = true;
        PlayerControl.Local.canMove = false;
        PlayerControl.Local.characterAnimator.enabled = false;
  
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        inPanel = false;
        PlayerControl.Local.canMove = true;
        PlayerControl.Local.characterAnimator.enabled = true;
        


    }


}
