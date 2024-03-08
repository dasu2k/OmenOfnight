using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    [SerializeField] private GameObject gunBarrel;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float delayBetweenBullets;
    [SerializeField] Animator gunAnimator;

    //still testing 

    //still testing
    private SpriteRenderer gunSpriteRenderer = null;
    public Sprite[] muzzleflash;
    private bool canShoot;
    

    void Start()
    {
        bulletSpeed = 30f;
        gunSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        canShoot = true;
    }

    void Update()
    {
        if(Input.GetMouseButton(0) && canShoot){
            //shoot a bullet
            shoot();
            //muzzle flash
            changeSprite(gunSpriteRenderer, muzzleflash[Random.Range(0,5)]);
            canShoot = false;
        }
        
    }

    void canShootNow(){
        canShoot = true;
        gunAnimator.SetBool("isShooting",false);
    }

    void shoot(){
        gunAnimator.SetBool("isShooting",true);
        GameObject bulletClone = Instantiate(bullet , gunBarrel.transform.position , gunBarrel.transform.rotation );
        Destroy(bulletClone,5f);
        bulletClone.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed , ForceMode.Impulse);
        Invoke("canShootNow" , delayBetweenBullets);
        GetComponent<AudioSource>().Play();
    }


    void removeMuzzleFlash(){
        gunSpriteRenderer.sprite = null;
    }
    void changeSprite(SpriteRenderer spriteRenderer , Sprite sprite){
        spriteRenderer.sprite = sprite;
        Invoke("removeMuzzleFlash", 0.1f);
    }
}