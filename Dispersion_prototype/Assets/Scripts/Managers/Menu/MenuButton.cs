using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(Selectable))]
public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea] public string text;

    private void Update()
    {
        foreach (Text textElem in GetComponentsInChildren<Text>())
        {
            textElem.text = text;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}