using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{

    public GameObject inventoryUIlayer;

    [SerializeField]
    GameObject singleCharacterInventoryUI; //Prefab to be used

    public List<Inventory> inventories;

    

    void Start()
    {
        inventories.Add(GameObject.FindWithTag("Player").GetComponent<Inventory>());
        
        foreach (Inventory inventory in inventories)
        {
            //inventory.onItemChangedCallback += UpdateUI(inventory);  //NOT WORKING, DON'T KNOW WHY
            inventory.inventoryUI = Instantiate(singleCharacterInventoryUI, inventory.gameObject.transform);
        }

    }

    // Update is called once per frame
    void Update()
    {
        StickToCharacter();

        if (Input.GetButtonDown("Inventory"))
        {
            SwitchInventoryUI();
        }
    }

    void UpdateUI(Inventory inventory)
    {
        Inventory_Slot[] slots = inventory.inventoryUI.GetComponentsInChildren<Inventory_Slot>();

        for (int i = 0; i < inventory.space; i++)
        {
            if (i < inventory.inventoryItems.Count)
            {
                slots[i].AddItem(inventory.inventoryItems[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public void SwitchInventoryUI ()
    {
        inventoryUIlayer.SetActive(!inventoryUIlayer.activeSelf);
        if (inventoryUIlayer.active == true)
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

    public void StickToCharacter()
    {
        foreach (Inventory inventory in inventories)
        {
            //inventory.inventoryUI.transform.position = inventory.gameObject.transform.position;
        }
    }


}
