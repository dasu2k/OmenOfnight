using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BasementDoor : MonoBehaviour
{
    GameObject player;
    float playerDistance;
    public TMP_Text prompt;


    bool open;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        open = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector3.Distance(transform.position,player.transform.position);
        if(playerDistance < 4 && player.GetComponent<PlayerStats>().basementDoorKey)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                if(!open){
                    Debug.Log("trying to open door");
                    transform.Rotate(new Vector3(0f,90f,0f));
                    open = true;
                }
                else{
                    transform.Rotate(new Vector3(0f,-90f,0f));
                    open = false;
                }
            }
        }
        else if(playerDistance < 4){
            prompt.text = "Find the key to unlock the main door and escape";
        }
        else{
            prompt.text = "";
        }
    }
}
