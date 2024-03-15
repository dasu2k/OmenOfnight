using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    //animations and shit
    [SerializeField] private GameObject gunBarrel;
    [SerializeField] private GameObject bulletImpact;
    [SerializeField] private float delayBetweenBullets;
    [SerializeField] Animator gunAnimator;
    

    //weapon active info
    [SerializeField] private float damage;
    [SerializeField] private int TotalAmmo;


    private SpriteRenderer gunSpriteRenderer = null;
    public Sprite[] muzzleflash;
    private bool canShoot;
    
    [SerializeField] private GameObject shootRayFrom; 
    void Start()
    {
        damage = 1f;
        gunSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        canShoot = true;
        TotalAmmo = 100;
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
        shootRayFrom.GetComponent<Animator>().SetBool("isShooting",false);
    }

    void shoot(){
        gunAnimator.SetBool("isShooting",true);
        shootRayFrom.GetComponent<Animator>().SetBool("isShooting",true);
        RaycastHit hit;
        if(Physics.Raycast(shootRayFrom.transform.position , Camera.main.transform.forward , out hit ,Mathf.Infinity)){
            
            Debug.Log(hit.collider.tag);
            if(hit.collider.tag == "Enemy"){
                hit.collider.gameObject.GetComponent<EnemyTest>().takeDamage(damage);
            }
            Instantiate(bulletImpact,hit.point, Quaternion.identity);

        };
        
        Invoke("canShootNow" , delayBetweenBullets);
        //GetComponent<AudioSource>().Play();
    }


    void removeMuzzleFlash(){
        gunSpriteRenderer.sprite = null;
    }
    void changeSprite(SpriteRenderer spriteRenderer , Sprite sprite){
        spriteRenderer.sprite = sprite;
        Invoke("removeMuzzleFlash", 0.1f);
    }
}