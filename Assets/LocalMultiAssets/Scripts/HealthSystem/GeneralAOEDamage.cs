using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralAOEDamage : MonoBehaviour
{
    public int damageAmount;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
        var healthScript = other.GetComponent<Health>();
        if (!healthScript)
        {
            Debug.Log("No health script attached");
            return;
        }

        healthScript.TakeDamage(damageAmount, 0);

    }
}
