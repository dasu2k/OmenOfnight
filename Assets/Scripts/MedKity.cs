using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MedKity : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    private GameObject player;
    
    private float distanceFromPlayer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void Update()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        transform.Rotate(Vector3.forward , rotationSpeed*Time.deltaTime); 
        //Debug.Log("distance from player " + distanceFromPlayer);
        if(distanceFromPlayer < 2){
            if(player.GetComponent<PlayerStats>().health + 20 <= 100)
                player.GetComponent<PlayerStats>().health +=  20;
            else
                player.GetComponent<PlayerStats>().health = 100;

            player.GetComponent<PlayerStats>().updateHealth();
            Destroy(gameObject);
        }
    
        
    }
}
