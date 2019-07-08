using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// A component which controls key binding changes.
/// </summary>
public class KeyBindingController : MonoBehaviour
{
    private GameObject selectedKeyBindingButton;
    public EventSystem eventSystem;
    //public Text fireKeyText;
    //public Text airKeyText;
    //public Text waterKeyText;
    //public Text earthKeyText;
    public TMP_Text fireKeyText;
    public TMP_Text airKeyText;
    public TMP_Text waterKeyText;
    public TMP_Text earthKeyText;

    //private bool buttonSelected = false;

    private void OnEnable()
    {
        fireKeyText.text = GameData.currentSavedData.keyBindings[fireKeyText.rectTransform.parent.name].ToString();
        airKeyText.text = GameData.currentSavedData.keyBindings[airKeyText.rectTransform.parent.name].ToString();
        waterKeyText.text = GameData.currentSavedData.keyBindings[waterKeyText.rectTransform.parent.name].ToString();
        earthKeyText.text = GameData.currentSavedData.keyBindings[earthKeyText.rectTransform.parent.name].ToString();
    }

    private void OnGUI()
    {
        //Debug.Log(eventSystem.currentSelectedGameObject);
        //Debug.Log("In OnGUI: " + selectedKeyBindingButton);
        //Debug.Log(selectedKeyBindingButton);
        if (selectedKeyBindingButton != null)
        {
            Debug.Log("IF entered.");
            Event _event = Event.current;

            if (_event.isKey)
            {
                GameData.currentSavedData.keyBindings[selectedKeyBindingButton.name] = _event.keyCode;
                //Debug.Log(_event.keyCode);
                //Debug.Log(GameData.currentSavedData.keyBindings[selectedKeyBindingButton.name]);
                selectedKeyBindingButton.GetComponentInChildren<TMP_Text>().text = _event.keyCode.ToString();
                selectedKeyBindingButton = null;
                //buttonSelected = false;
            }
        }
    }

    public void SetAsSelectedKeyBindingButton(GameObject keyBindingButton)
    {
        //Debug.Log("SetAsSelectedKeyBindingButton called.");
        selectedKeyBindingButton = keyBindingButton;
        //Debug.Log("In SetAsSelectedKeyBindingButton: " + selectedKeyBindingButton);
        eventSystem.SetSelectedGameObject(keyBindingButton);
        //buttonSelected = true;
    }
}
