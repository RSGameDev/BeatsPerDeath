using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIhandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // OnHover()
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255, 243, 97, 255);

        if (gameObject.name == "Back Button" || gameObject.name == "Quit Button")
        {
            GetComponentInChildren<TextMeshProUGUI>().color = new Color32(91, 185, 245, 255);
        }

        if (gameObject.name == "Options Button" || gameObject.name == "Gameplay Button" || gameObject.name == "Sounds Button" || gameObject.name == "Credits Button" || gameObject.name == "Default Button")
        {
            GetComponentInChildren<TextMeshProUGUI>().color = new Color32(0, 100, 245, 255);
        }

        //////////// because of old wwise taken out
        /////////////AkSoundEngine.PostEvent("UI_On_Hover", gameObject);
    }

    // OnHoverExit() 
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
        AkSoundEngine.PostEvent("Hover", gameObject);
    }

    // OnMouseClick()
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        switch(gameObject.name)
        {
            
            case "Start Button":
            AkSoundEngine.PostEvent("Select", gameObject);
            break;

            case "Options Button":
            AkSoundEngine.PostEvent("Select", gameObject);
            break;
            
            case "Back Button":
            AkSoundEngine.PostEvent("Select", gameObject);
            break;
            
            case "Default Button":  
            break;
            
            case "Quit Button":
            Debug.Log("quit button pressed");
            AkSoundEngine.PostEvent("Quit", gameObject);
            break;

            ////// WE HAVENT HAD THE NEED TO USE THIS CODE SO FAR:
            //////case "Gameplay Button":
            //////    AkSoundEngine.PostEvent("UI_Forward", gameObject);
            //////    break;
            //////
            //////case "Sounds Button":
            //////    AkSoundEngine.PostEvent("UI_Forward", gameObject);
            //////    break;
            //////
            //////case "Credits Button":
            //////    AkSoundEngine.PostEvent("UI_Forward", gameObject);
            //////    break;
        }
    }
}


