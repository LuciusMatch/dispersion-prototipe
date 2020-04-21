using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class Item_PickUp : Interactable
{
    public Item item;
    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    void PickUp()
    {
        //GameObject.Find("Canvas_Levels").GetComponent<Inventory_UI>().SwitchInventoryUI();

        bool wasPickedUp = Interactor.GetComponent<Inventory>().Add(item);

        Debug.Log("Picking up by" + Interactor.transform.name);
        Debug.Log("Picking up by" + Interactor.transform.name);
        Debug.Log("Picking up by" + Interactor.transform.name);
        Debug.Log("Picking up by" + Interactor.transform.name);
        if (wasPickedUp)
        {
            GameObject.Find("Help_text").GetComponent<Text>().text = ""; //FOR PLAYTESTING
            Destroy(gameObject);
        }
    }
}
