using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    int maxHealth = 100;

    GameObject healthbar;
    [SerializeField]
    Vector3 helthbarlenght;

    public float curHealth = 100;

    [SerializeField]
    float regenerationSpeed = 30;

    [SerializeField]
    float damagingSpeed = 50;

    // Start is called before the first frame update
    void Start()
    {
        healthbar = transform.Find("HealthBar").gameObject;
        helthbarlenght = healthbar.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (curHealth < maxHealth)
        {
            curHealth += Time.deltaTime * regenerationSpeed;
            if (curHealth >= maxHealth) curHealth = maxHealth;
        }

        Vector3 healthbarlengthnew = new Vector3(helthbarlenght.x, helthbarlenght.y, helthbarlenght.z * curHealth / maxHealth);
        healthbar.transform.localScale = healthbarlengthnew;

        if (curHealth <= 0)
           EnemyDeath();
    }


    public void DecreaseHP(float damage)
    {
        curHealth -= Time.deltaTime * damage;
    }

    public void EnemyDeath()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        curHealth -= Time.deltaTime * damagingSpeed;
    }
}
