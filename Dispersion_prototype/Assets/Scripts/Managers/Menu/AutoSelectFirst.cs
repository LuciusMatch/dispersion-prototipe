using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// select first item after pressing up or down if nothing is selected yet
public class AutoSelectFirst : MonoBehaviour
{
    private PlayerControls input;

    private void Awake()
    {
        input = new PlayerControls();
    }

    private void Update()
    {
        if (input.UI.Navigate.triggered && !EventSystem.current.currentSelectedGameObject)
        {
            EventSystem.current.SetSelectedGameObject(GetComponentInChildren<Selectable>().gameObject);
        }
    }

    private void OnEnable()
    {
        input.Enable();
        EventSystem.current.SetSelectedGameObject(GetComponentInChildren<Selectable>().gameObject);
    }

    private void OnDisable()
    {
        input.Disable();
    }
}
