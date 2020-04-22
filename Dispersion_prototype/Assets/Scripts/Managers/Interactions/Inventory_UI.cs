using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{

    public GameObject inventoryUIlayer;

    [SerializeField]
    GameObject singleCharacterInventoryUI; //Prefab to be used

    public List<Inventory> inventories;
    public int offsetY = 100; // offset of the inventory above the player's head

    void Start()
    {
        inventories.Add(GameObject.FindWithTag("Player").GetComponent<Inventory>());
        
        foreach (Inventory inventory in inventories)
        {
            inventory.onItemChangedCallback += UpdateUI;
            inventory.inventoryUI = Instantiate(singleCharacterInventoryUI, inventoryUIlayer.gameObject.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            SwitchInventoryUI();
            StickToCharacter();
        }
    }

    void UpdateUI(Inventory inventory)
    {
        Inventory_Slot[] slots = inventoryUIlayer.GetComponentsInChildren<Inventory_Slot>();

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
        if (inventoryUIlayer.activeSelf)
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
            Vector3 pos = Camera.main.WorldToScreenPoint(GameManager.Instance.player.transform.position);
            inventory.inventoryUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(2 * pos.x, 2 * pos.y + offsetY);

        }
    }

}
