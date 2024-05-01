using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    GameObject player;
    float playerDistance;
    // Start is called before the first frame update
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
        if(playerDistance < 4)
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
    }
}
