using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    CharacterMovement cm;
 
    PlayerInput inputs;

    WeaponHandler weaponHandler;

    Transform target;

    GameObject enemy;

    int enemyHealth;

    private float timer;

    public float timeBetweenShots;
    // Start is called before the first frame update
    void Start()
    {
        
        cm = GetComponent<CharacterMovement>();
        inputs = GetComponent<PlayerInput>();
        weaponHandler = GetComponent<WeaponHandler>();

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKey(inputs.fire))
        {
           if(timer >= timeBetweenShots) Shoot();
        }

        if (Input.GetKeyDown(inputs.interact) && weaponHandler.reachableObjects.Count > 0)
        {
            weaponHandler.PickClosestWeapon();
            
        }

        cm.anim.SetBool("Dancing", Input.GetKey(inputs.taunt));

        if (cm.anim.GetBool("Dead"))
        {
            weaponHandler.DropWeapon();
            return;
        }
        AutoAim();

    }

    private void OnTriggerEnter(Collider other)
    {
        enemy = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        enemy = null;
    }

    public void Shoot()
    {
        timer = 0;
        cm.anim.SetTrigger("Shoot");
        if (weaponHandler.currentWeapon != null)
        {
            weaponHandler.currentWeapon.GetComponent<IShootable>().Shoot();
        }
    }



    private void AutoAim()
    {
        if (enemy != null)
        {
            target = enemy.transform;
            transform.LookAt(target);
        }
        else
        {
            return;
        }
    }

    

  
}
