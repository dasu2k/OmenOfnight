using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainDoor : MonoBehaviour
{
    
    GameObject player;
    public TMP_Text prompt;
    float playerDistance;
    
    // Start is called before the first frame update
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector3.Distance(player.transform.position, transform.position);
        if(playerDistance < 3f){
            if(!player.GetComponent<PlayerStats>().mainDoorKey)
                prompt.text = "Find the key to unlock the main door and escape";
            else{
                prompt.text = "Press e to unlock the main door";
                if(Input.GetKeyDown(KeyCode.E)){
                    SceneManager.LoadScene("UI");
                }
            }
        }
        else{
            prompt.text = "";
        }
    }
}
