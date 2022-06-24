using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Tooltip ("HealthAmounts")]
    public int maxhealth, startingHealth, currentHealth;
    public bool canBeDamaged;

    //components
    public Slider healthUI;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        UpdateHealthUI();

    }

    public virtual void TakeDamage(int amount, int damager = 0)
    {
        if (!canBeDamaged) 
        {
            Debug.Log("Attempted to Damage" + gameObject.name + "but can not be damaged right now");
            return;
        }
        currentHealth -= amount;
        if (currentHealth <= 0) 
        { 
            currentHealth = 0;
            TriggerDeath(damager);
        }
        UpdateHealthUI();
    }

    public virtual void TriggerDeath(int damager)
    {
        //animate death, sounds etc
        Debug.Log(gameObject + "has been killed");
        canBeDamaged = false;
        Invoke("Die", 2f);

    }

    public virtual void Die()
    {
        Debug.Log(gameObject + "removed from scene");
        Destroy(gameObject);
    }

    private void UpdateHealthUI()
    {
        if (!healthUI)return;
        healthUI.maxValue = maxhealth;
        healthUI.value = currentHealth;
    }
}
