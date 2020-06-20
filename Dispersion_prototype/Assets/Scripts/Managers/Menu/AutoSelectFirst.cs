using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AutoSelectFirst : MonoBehaviour
{
    // select first item after pressing up or down if nothing is selected yet
    private void Update()
    {
        if ((Input.GetButtonDown("Vertical") || Input.GetAxis("Vertical") != 0)
            && !EventSystem.current.currentSelectedGameObject)
        {
            EventSystem.current.SetSelectedGameObject(GetComponentInChildren<Selectable>().gameObject);
        }
    }
}
