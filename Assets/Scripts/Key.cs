using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    private GameObject player;
    [SerializeField] TMP_Text prompt;
    public int keyId;
    private float distanceFromPlayer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void Update()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        transform.Rotate(Vector3.up , rotationSpeed*Time.deltaTime); 
        
        if(distanceFromPlayer < 3){
            prompt.text = "press e to pick up the key\n";
            if(Input.GetKeyDown(KeyCode.E)){
                player.GetComponent<PlayerStats>().pickUpKey(keyId);
                prompt.text = "";
                Destroy(gameObject);
            }
        }
        else{
            prompt.text = "";
        }
    }
}
