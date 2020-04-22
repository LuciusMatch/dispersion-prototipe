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

    bool hasGun = false;


    void Start()
    {
        healthdecreaseSpeed = 15;
        healthbar = transform.Find("HealthBar").gameObject;
        helthbarlenght = healthbar.transform.localScale;
    }

   void UpdateHP()
    {
        Vector3 healthbarlengthnew = new Vector3(helthbarlenght.x, helthbarlenght.y, helthbarlenght.z * curHealth / maxHealth);
        healthbar.transform.localScale = healthbarlengthnew;

        if (curHealth <= 0)
            CloneDeath();

        Debug.Log(curHealth);
    }

    public void DecreaseHP()
    {
        curHealth -= Time.deltaTime * healthdecreaseSpeed;
        UpdateHP();
    }

     public void CloneDeath()
    {
        cloningController.platformactivated = false;
        Destroy(this.gameObject);
    }
}
