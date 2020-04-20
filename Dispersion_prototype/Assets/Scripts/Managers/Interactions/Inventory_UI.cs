using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public Transform itemsOriginalParent;
    public Transform itemsCloneParent;
    public GameObject inventoryUI;
    Inventory inventory;

    Inventory_Slot[] ogiginalSlots;
    Inventory_Slot[] cloneSlots;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        ogiginalSlots = itemsOriginalParent.GetComponentsInChildren<Inventory_Slot>();
        cloneSlots = itemsCloneParent.GetComponentsInChildren<Inventory_Slot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            SwitchInventoryUI();
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < ogiginalSlots.Length; i++)
        {
            if (i < inventory.originalItems.Count)
            {
                ogiginalSlots[i].AddItem(inventory.originalItems[i]);
                cloneSlots[i].AddItem(inventory.cloneItems[i]);
            }
            else
            {
                ogiginalSlots[i].ClearSlot();
                cloneSlots[i].ClearSlot();
            }
        }
    }

    public void SwitchInventoryUI ()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
        if (inventoryUI.active == true)
        {
            Time.timeScale = 0.1f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }
        else
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }
    }
}
