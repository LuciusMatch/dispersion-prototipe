using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    PlayerController playerController;

    float maxHealth = 100;
    public float curHealth = 100;
    [SerializeField]
    float healthdecreaseSpeed = 20;

    public Slider healthBar;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playerController = transform.GetComponent<PlayerController>();

        healthBar = GameObject.Find("PlayerHealthBar").GetComponent<Slider>();
        healthBar.value = maxHealth;

        animator = transform.GetChild(0).Find("Model").GetComponent<Animator>();
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
        StartCoroutine(DeathCoroutine());
    }

    IEnumerator DeathCoroutine()
    {
        AsyncOperation asyncOper = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        asyncOper.allowSceneActivation = false;
        animator.SetBool("Death", true); //DEATH ANIMATION
        playerController.DeathFreeze();
        GameManager.audioPlayer.ElectricShock();

        yield return new WaitForSeconds(1);

        while (asyncOper.progress < .9f)
        {
            yield return null;
        }

        asyncOper.allowSceneActivation = true;
    }
}
