using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public override void TakeDamage(int amount, int damager = 0)
    {
        base.TakeDamage(amount, damager);
    }
    
    public override void TriggerDeath(int damager)
    {
        base.TriggerDeath(damager);
        RoundManager.instance.UpdateScore(damager, GetComponent<PlayerInput>().playerNum);
        Animator anim = GetComponentInChildren<Animator>();
        anim.SetBool("Dead", true);
        
    }

    public override void Die()
    {
        RoundManager.instance.SpawnPlayer(GetComponent<PlayerInput>().playerNum);
        Destroy(gameObject);
    }
    public void ResetDamage()
    {
        canBeDamaged = true;
    }

}
