using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerToggle : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    public TMP_Text prompt;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the entering object has the "Player" tag
        {
            Debug.Log("Player entered the trigger!");
        // You can perform actions like activating a dialogue, opening a door, etc.
            player.GetComponent<PlayerStats>().isInverted = true;
        }
        prompt.text = "Entered the area of hallucinations \n dev note: player's mouse input is inverted";
        Invoke("removePrompt",10f);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the entering object has the "Player" tag
        {
            Debug.Log("Player exited the trigger!");
        // You can perform actions like activating a dialogue, opening a door, etc.
            player.GetComponent<PlayerStats>().isInverted = false;
        }
        prompt.text = "you have exited the area of hallucinations";
        Invoke("removePrompt",2f);
    }


    void removePrompt(){
        prompt.text = "";
    }
}
