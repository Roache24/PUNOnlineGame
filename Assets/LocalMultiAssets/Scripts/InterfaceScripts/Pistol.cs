using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour, IShootable
{
    [Tooltip("Bullet properties")]
    [SerializeField] float range, reloadTime;
    [SerializeField] int damage, ammo, maxAmmo;
    [Tooltip("Components")]
    [SerializeField] ParticleSystem pS, hitPS;
    [SerializeField] Transform[] raycastPoints;
    [SerializeField] LineRenderer lR;
    [SerializeField] LayerMask shootableLayer;
    public AudioSource shootSound;
    public float AudioVolume;
    public AudioClip noAmmo;

    public void Start()
    {
        shootSound = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        ammo--;

        shootSound.Play();
        RaycastHit hit;
        foreach (Transform raypoint in raycastPoints)
        {
            if (Physics.Raycast(raypoint.position, raypoint.forward, out hit, range, shootableLayer))
            {
                if (hit.collider.CompareTag("Player") && hit.collider.gameObject != gameObject)
                {
                    lR.SetPosition(0, raypoint.transform.position);
                    lR.SetPosition(1, hit.point);
                    lR.enabled = true;
                    var healthScript = hit.collider.GetComponent<Health>();
                    hitPS.transform.position = hit.point;
                    hitPS.Play();
                    if (healthScript == null)
                    {
                        Debug.Log("no script attached to target" + hit.collider.name);
                        return;
                    }
                    healthScript.TakeDamage(damage, GetComponentInParent<PlayerInput>().playerNum);
                    pS.Play();
                    Invoke("TurnOffEffects", 0.25f);
                }
                else
                {
                    lR.SetPosition(0, raycastPoints[0].position);
                    lR.SetPosition(1, hit.point);
                    lR.enabled = true;
                    hitPS.transform.position = hit.point;
                    hitPS.Play();
                    pS.Play();
                    Invoke("TurnOffEffects", 0.25f);
                }
            }
            else
            {
                lR.SetPosition(0, raycastPoints[0].transform.position);
                lR.SetPosition(1, raycastPoints[0].transform.position + raycastPoints[0].forward * range);
                lR.enabled = true;
                pS.Play();
                Invoke("TurnOffEffects", 0.25f);
            }
        }
        
        Debug.Log("pEw Im A pIsTol");
        Invoke("CheckAmmo", 0.25f);
    }
    
    void Destroy()
    {
        Destroy(gameObject);
    }
    void CheckAmmo()
    {
        if (ammo < 1)
        {
            shootSound.volume = AudioVolume;
            shootSound.clip = noAmmo;
            shootSound.Play();
            Invoke("Destroy", 1f);
        }
    }

    void TurnOffEffects()
    {
        lR.enabled = false;
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Transform raypoint in raycastPoints)
        {
            Gizmos.DrawLine(raypoint.position, raypoint.transform.position + raypoint.transform.forward * range);
        }
    }
#endif
}