using UnityEngine;
using UnityEngine.EventSystems;

// Made by      : Abia Herlianto
// Description  : sets game object as selected game object in
//                the event system.
public class MainMenuFocus : MonoBehaviour, IPointerEnterHandler
{
    public EventSystem eventSystem;

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventSystem.SetSelectedGameObject(this.gameObject);
    }
}
