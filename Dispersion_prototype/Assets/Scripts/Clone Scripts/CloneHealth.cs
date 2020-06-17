using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneHealth : MonoBehaviour
{
    float maxHealth = 100;
    
    GameObject healthbar;
    [SerializeField]
    Vector3 helthbarlenght;

    public float curHealth = 100;

    [SerializeField]
    float healthdecreaseSpeed;
    public CloningController cloningController;

    public Animator animator;

    bool hasGun = false;


    void Start()
    {
        healthdecreaseSpeed = 25;
        healthbar = transform.Find("HealthBar").gameObject;
        helthbarlenght = healthbar.transform.localScale;
        animator = transform.GetChild(0).Find("Model").GetComponent<Animator>();

    }

   void UpdateHP()
    {
        Vector3 healthbarlengthnew = new Vector3(helthbarlenght.x, helthbarlenght.y, helthbarlenght.z * curHealth / maxHealth);
        healthbar.transform.localScale = healthbarlengthnew;

        if (curHealth <= 0)
            CloneDeath();
    }

    public void DecreaseHP()
    {
        curHealth -= Time.deltaTime * healthdecreaseSpeed;
        UpdateHP();
    }

     public void CloneDeath()
    {
        cloningController.platformactivated = false;
        GameManager.Instance.clones.Remove(this.gameObject);

        KeycardInventory inventory = GetComponent<KeycardInventory>();
        if (inventory != null)
        {
            KeycardInventory playerInventory = GameManager.Instance.player.GetComponent<KeycardInventory>();
            foreach (Keycard card in inventory.keycards)
            {
                if (!playerInventory.HasKeycard(card.keycardId))
                {
                    card.Reset();
                }
            }
        }

        Destroy(this.gameObject);
    }

}
