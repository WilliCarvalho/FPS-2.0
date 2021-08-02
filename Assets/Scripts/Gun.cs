using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [Header("GunConfig")]
    public int Damage;
    public int Range;
    public float reloadTime;
    public float speed;
    public float bulletMass;
    public float fireRate;
    public enum type
    {
        Rifle,
        Pistol,
        Sniper,
        Shotgun,
        Rocket
    }
    public type _type;

    [Header("Ammo")]
    public int ammo;
    public int maxAmmoInMag;
    public int ammoInMag;

    [Header("Imports")]
    public GameObject bullet;
    public PlayerController player;
    public GameObject pointGun;
    public ParticleSystem effectFire;
    public ParticleSystem effectEject;
    public Text ammoTxt;

    //private
    private bool fireRateBool = true;

    // Update is called once per frame
    void Update()
    {
        ammoTxt.text = ammoInMag + "/" + ammo;
        if(_type.ToString() == "Rifle")
        {
            if (Input.GetMouseButton(0))
            {
                if(ammoInMag > 0 && fireRateBool == true)
                {
                    Shoot();
                }
                StopCoroutine(ReloadCoolDown());
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (ammoInMag > 0 && fireRateBool == true)
                {
                    Shoot();
                }
                StopCoroutine(ReloadCoolDown());
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            if(ammoInMag != maxAmmoInMag && ammo != 0)
            {
                StartCoroutine(ReloadCoolDown());
            }
        }
    }

    void Shoot()
    {
        GameObject go = Instantiate(bullet, pointGun.transform.position, pointGun.transform.rotation);
        Bullet _bullet = go.GetComponent<Bullet>();
        _bullet.mass = bulletMass;
        _bullet.damage = Damage;
        _bullet.range = Range;
        _bullet.speed = speed;
        effectEject.Play();
        effectFire.Play();
        ammoInMag--;
        StartCoroutine(FireRateLoop());
    }

    void Reload()
    {
        for(int i = 0; i < maxAmmoInMag; i++)
        {
            if(ammo > 0 && ammoInMag < maxAmmoInMag)
            {
                ammoInMag++;
                ammo--;
            }
            else
            {
                break;
            }
        }
    }

    IEnumerator ReloadCoolDown()
    {
        yield return new WaitForSeconds(reloadTime);
        Reload();
    }

    IEnumerator FireRateLoop()
    {
        fireRateBool = false;
        yield return new WaitForSeconds(fireRate);
        fireRateBool = true;
    }
}
