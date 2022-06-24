using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawn : MonoBehaviour
{
    public GameObject weapon;

    public float timeToSpawn;
    private float currentTimeToSpawn;




    // To do weapon pick up, spawnedWeapon update, On death weapon drop
    void Update()
    {
       if (currentTimeToSpawn > 0)
        {
            currentTimeToSpawn -= Time.deltaTime;
        }
        else
        {
            SpawnWeapon();
            currentTimeToSpawn = timeToSpawn;
        }
       
    }

    void SpawnWeapon()
    {
        Instantiate(weapon, transform.position, transform.rotation);
    }

   
}
