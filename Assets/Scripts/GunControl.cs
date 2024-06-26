using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using TMPro;public class GunControl : MonoBehaviour
{
    //animations and shit
    [SerializeField] private GameObject gunBarrel;
    [SerializeField] private GameObject bulletImpact;
    [SerializeField] private float delayBetweenBullets;
    [SerializeField] Animator gunAnimator;
    [SerializeField] private GameObject playerView;
    private bool canShoot;

    //end

    

    //weapon active info
    
    [SerializeField] private int TotalAmmo;
    private int ammoInMag;
    
    //end

    

    //User interface shit
    [SerializeField] private TMP_Text ammoUiElement;
    [SerializeField] private TMP_Text messageUiElement;
    //end
    
    

    //mechanics
    [Header("mechanism of the gun")]
    [SerializeField] private GameObject shootRayFrom; 
    [SerializeField] private bool canHoldDownShoot;
    [SerializeField] private int magSize;
    [SerializeField] private float damage;
    [SerializeField] private float reloadingTime;
    [SerializeField] private GameObject muzzleFlashObj;
    //end 


    //still testing
    [Header("testing shit")]
    public GameObject HUD;
    //end

    void Start()
    {
        canShoot = true;
        ammoInMag = magSize;
       
    }
    void Update()
    {
        //taking in the fire input
        bool fireInput;
        if(canHoldDownShoot)
        {
            fireInput = Input.GetMouseButton(0);
        }
        else{
            fireInput = Input.GetMouseButtonDown(0);
        }

        
        if(Input.GetKeyDown(KeyCode.R) && canShoot)
        {
           
                reload();
        }


        

        // if the shoot input is there , gun is free to shoot another bullet and there is atleast 1 ammo either in the mag or in storage go ahead 
        if(fireInput && canShoot && (TotalAmmo+ammoInMag > 0)){
            //if there is no ammo in the magazine do a reload
            if(ammoInMag <= 0)
            {
                reload();
            }
            //else just shoot a bullet
            else{
                shoot();
            }
           
            //muzzle flash
            Instantiate(muzzleFlashObj , gunBarrel.transform.position, gunBarrel.transform.rotation);
            
        }
        else{
            //Debug.Log("no ammo left");
        }

        //Debug.Log("Ammo in mag" + ammoInMag);
    }

    void reload(){
        canShoot = false;
        gunAnimator.SetBool("isReloading",true);
        playerView.GetComponent<Animator>().SetBool("isShooting",false);
        messageUiElement.text = "Reloading";
        Invoke("reloadComplete" , reloadingTime);
    }

    void reloadComplete(){
        if(TotalAmmo >= magSize){
            TotalAmmo-= magSize-ammoInMag;
            ammoInMag = magSize;
        }
        else{
            ammoInMag += TotalAmmo;
            TotalAmmo = 0;
        }
        messageUiElement.text = "";
        canShoot = true;
        ammoUiElement.text = ammoInMag + "/" + TotalAmmo;
        gunAnimator.SetBool("isReloading",false);
    }

    void canShootNow(){
        canShoot = true;
        gunAnimator.SetBool("isShooting",false);
        playerView.GetComponent<Animator>().SetBool("isShooting",false);
    }

    void shoot(){
        canShoot = false;
        gunAnimator.SetBool("isShooting",true);
        //playerView.GetComponent<Animator>().SetBool("isShooting",true);
        RaycastHit hit;
        if(Physics.Raycast(shootRayFrom.transform.position , Camera.main.transform.forward+new Vector3(Random.Range(0.03f,-0.03f),Random.Range(0.02f,-0.02f),0f) , out hit ,Mathf.Infinity)){
            if(hit.collider.tag == "Enemy"){
                hit.collider.gameObject.GetComponent<EnemyTest>().takeDamage(damage);
            }
            Instantiate(bulletImpact,hit.point, Quaternion.identity);
        };
        ammoInMag--;
        ammoUiElement.text = ammoInMag + "/" + TotalAmmo;
        Invoke("canShootNow" , delayBetweenBullets);
        GetComponent<AudioSource>().Play();
    }

    
}