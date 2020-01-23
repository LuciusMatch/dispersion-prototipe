using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    float maxHealth = 100;
    float curHealth = 100;
    [SerializeField]
    float healthdecreaseSpeed = 20;

    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GameObject.Find("PlayerHealthBar").GetComponent<Slider>();
        healthBar.value = maxHealth;
    }


    public void DecreaseHP()
    {
        curHealth -= Time.deltaTime * healthdecreaseSpeed;
        UpdateHP();
    }


    void UpdateHP()
    {
        healthBar.value = curHealth;

        if (curHealth <= 0)
            Death();
    }

    public void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
