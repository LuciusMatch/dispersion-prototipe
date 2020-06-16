using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AutoSelectFirst : MonoBehaviour
{
    // select first item after pressing up or down if nothing is selected yet
    private void Update()
    {
        if (Input.GetButtonDown("Vertical") && !EventSystem.current.currentSelectedGameObject)
        {
            EventSystem.current.SetSelectedGameObject(GetComponentInChildren<Button>().gameObject);
        }
    }
}
