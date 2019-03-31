using UnityEngine;
using UnityEngine.EventSystems;

// Made by: Abia P.H.
// Description: selects selectedObject on any given input from the Vertical
//              axis allowing the player to select buttons using the arrow keys
public class SelectOnInput : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject selectedObject;

    private bool buttonIsSelected;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetAxisRaw("Vertical") != 0 && !buttonIsSelected)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonIsSelected = true;
        }
    }

    private void OnDisable()
    {
        buttonIsSelected = false;
    }
}
