using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using TMPro;
public class GunControl : MonoBehaviour
{
    //animations and shit
    [SerializeField] private GameObject gunBarrel;
    [SerializeField] private GameObject bulletImpact;
    [SerializeField] private float delayBetweenBullets;
    [SerializeField] Animator gunAnimator;
    [SerializeField] private GameObject playerView;

    private SpriteRenderer gunSpriteRenderer = null;
    public Sprite[] muzzleflash;
    private bool canShoot;
    //end

    

    //weapon active info
    [SerializeField] private float damage;
    [SerializeField] private int TotalAmmo;
    private int ammoInMag;
    [SerializeField] private int magSize;
    //end

    

    //User interface shit
    [SerializeField] private TMP_Text ammo;
    //end
    

    //mechanics
    [SerializeField] private GameObject shootRayFrom; 
    
    //end 

    void Start()
    {
        damage = 1f;
        gunSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        canShoot = true;
        ammoInMag = magSize;

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            reload();
        }
        if(Input.GetMouseButton(0) && canShoot && (TotalAmmo+ammoInMag > 0)){
            //shoot a bullet
            if(ammoInMag <= 0)
            {
                reload();
                ammo.text = ammoInMag + "/" + TotalAmmo; 
            }
            shoot();
            //muzzle flash
            changeSprite(gunSpriteRenderer, muzzleflash[Random.Range(0,5)]);
            canShoot = false;
        }
        else{
            Debug.Log("no ammo left");
        }

        Debug.Log("Ammo in mag" + ammoInMag);
    }

    void reload(){
        gunAnimator.SetBool("isReloading",true);
        if(TotalAmmo >= magSize){
            TotalAmmo-= magSize-ammoInMag;
            ammoInMag = magSize;
        }
        else{
            ammoInMag = TotalAmmo;
            TotalAmmo = 0;
        }
        ammo.text = ammoInMag + "/" + TotalAmmo;
        Invoke("reloadComplete" , 0.7f);
    }
    void reloadComplete(){
        gunAnimator.SetBool("isReloading",false);
    }
    void canShootNow(){
        canShoot = true;
        gunAnimator.SetBool("isShooting",false);
        playerView.GetComponent<Animator>().SetBool("isShooting",false);
    }

    void shoot(){
        gunAnimator.SetBool("isShooting",true);
        playerView.GetComponent<Animator>().SetBool("isShooting",true);
        RaycastHit hit;
        if(Physics.Raycast(shootRayFrom.transform.position , Camera.main.transform.forward , out hit ,Mathf.Infinity)){
            Debug.Log(hit.collider.tag);
            if(hit.collider.tag == "Enemy"){
                hit.collider.gameObject.GetComponent<EnemyTest>().takeDamage(damage);
            }
            Instantiate(bulletImpact,hit.point, Quaternion.identity);
        };
        ammoInMag--;
        ammo.text = ammoInMag + "/" + TotalAmmo;
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