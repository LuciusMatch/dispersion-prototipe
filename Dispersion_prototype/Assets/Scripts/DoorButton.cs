using UnityEngine;

public class DoorButton : Interactable
{
    [SerializeField]
    Transform door;
    public override void Interact()
    {
        base.Interact();
        ButtonPushed();
    }

    void ButtonPushed()
    {
        door.gameObject.SetActive(false);
        Debug.Log("Button is pushed");
    }
}
