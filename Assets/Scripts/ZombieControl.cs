using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieControl : MonoBehaviour
{
    private float distanceFromPlayer;
    private GameObject player;
    public Animator animator;
    [SerializeField] private float spottingRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float runningSpeed;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator= GetComponentInChildren<Animator>();
    }

    void Update()
    {
        distanceFromPlayer = Vector3.Distance(player.transform.position,transform.position);
        
        if(player != null){
            if(distanceFromPlayer < attackRange)
            {
                //attack them
                Debug.Log("attacking ");
                animator.SetBool("attacking",true);
                animator.SetBool("idle",false);
                animator.SetBool("running",false);
            }

            else if(distanceFromPlayer < spottingRange && !animator.GetBool("attacking"))
            {
                //run towards them
                Debug.Log("running towards player");
                //transform.LookAt(player.transform.position);
                //transform.Translate(Vector3.forward * Time.deltaTime  * runningSpeed);

                animator.SetBool("attacking",false);
                animator.SetBool("idle",false);
                animator.SetBool("running",true);
            }
            else{
                //go back into idle
                Debug.Log("going into idle");

            
                animator.SetBool("attacking",false);
                animator.SetBool("idle",true);
                animator.SetBool("running",false);
            }
        }
    }
}
