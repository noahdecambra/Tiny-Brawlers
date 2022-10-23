using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharHealth : MonoBehaviour
{
    public Animator anim;
    public float deathTime;
    public GameObject gameOverUI;

    //private EnemyAI enemyAI;

    public int currentHealth;
    public int maxHealth;
    public HealthBar healthBar;
    public int Health
    {
        get
        {
            return currentHealth;
        }
        set
        {
            if (value >= 0)
            {
                currentHealth = value;
            }
            else
            {
                currentHealth = 0;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = Mathf.Clamp(currentHealth - 1, 0, 100);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int amount)
    {
        anim.SetTrigger("Take Damage");

        currentHealth -= amount;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0) 
        {
            anim.SetTrigger("Die");
            Invoke(nameof(DestroyEnemy), 0.5f);                    

            if (tag == "Player")
            {                
                GetComponent<PlayerController>().enabled = false;
                gameOverUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                GetComponent<EnemyAI>().enabled = false;
            }
            GetComponent<BoxCollider>().enabled = false;                        
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject, deathTime);
    }
}
