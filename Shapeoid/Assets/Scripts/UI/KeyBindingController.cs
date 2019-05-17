using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// A component which controls key binding changes.
/// </summary>
public class KeyBindingController : MonoBehaviour
{
    private GameObject selectedKeyBindingButton;
    public EventSystem eventSystem;
    public Text fireKeyText;
    public Text airKeyText;
    public Text waterKeyText;
    public Text earthKeyText;

    private void OnEnable()
    {
        fireKeyText.text = GameData.currentSavedData.keyBindings[fireKeyText.transform.parent.name].ToString();
        airKeyText.text = GameData.currentSavedData.keyBindings[airKeyText.transform.parent.name].ToString();
        waterKeyText.text = GameData.currentSavedData.keyBindings[waterKeyText.transform.parent.name].ToString();
        earthKeyText.text = GameData.currentSavedData.keyBindings[earthKeyText.transform.parent.name].ToString();
    }

    private void OnGUI()
    {
        if (selectedKeyBindingButton != null)
        {
            Event _event = Event.current;

            if (_event.isKey)
            {
                GameData.currentSavedData.keyBindings[selectedKeyBindingButton.name] = _event.keyCode;
                //Debug.Log(GameData.currentSavedData.keyBindings[selectedKeyBindingButton.name]);
                selectedKeyBindingButton.GetComponentInChildren<Text>().text = _event.keyCode.ToString();
                selectedKeyBindingButton = null;
            }
        }
    }

    public void SetAsSelectedKeyBindingButton(GameObject keyBindingButton)
    {
        selectedKeyBindingButton = keyBindingButton;
        eventSystem.SetSelectedGameObject(keyBindingButton);
    }
}
