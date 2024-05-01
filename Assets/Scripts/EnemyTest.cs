using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class EnemyTest : MonoBehaviour
{
    private float distanceFromPlayer;
    private Animator animator;

    [SerializeField] float health;
    [SerializeField] GameObject player;
    [SerializeField] Transform target;
    [SerializeField] NavMeshAgent navMeshAgent;


    [SerializeField] private float spottingRange;
    [SerializeField] private float attackRange;
    [SerializeField] private int runningSpeed;
    [SerializeField] private int damage;


    public float attackDuration;



    public bool isCoroutineRunning;
    public bool isAttackAnimating;

    // Start is called before the first frame update
    void Start()
    {
        health = 20;
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponentInChildren<Animator>();
    }
    void Awake(){
        navMeshAgent = GetComponent<NavMeshAgent>();
        
    }

    void Update(){
        distanceFromPlayer = Vector3.Distance(player.transform.position,transform.position);
        if(distanceFromPlayer < spottingRange && !animator.GetBool("attacking"))
            navMeshAgent.destination = target.position;

        if(animator.GetBool("attacking"))
        {
            navMeshAgent.speed = 0f;
        }


        if(player != null){
            if(distanceFromPlayer < attackRange)
            {
                //attack them
                //Debug.Log("attacking ");
                animator.SetBool("attacking",true);
                animator.SetBool("idle",false);
                animator.SetBool("running",false);
                

                isAttackAnimating = true;
                Invoke("AttackAnimationDone",1.7f);
                navMeshAgent.speed = 0f;

                if(!isCoroutineRunning)
                    StartCoroutine(Attack());
            }

            else if(distanceFromPlayer < spottingRange && !isAttackAnimating)
            {
                //run towards them
                //Debug.Log("running towards player");
                
                animator.SetBool("attacking",false);
                animator.SetBool("idle",false);
                animator.SetBool("running",true);
                navMeshAgent.speed = runningSpeed;
            }
            else{
                //go back into idle
                //Debug.Log("going into idle");
                navMeshAgent.speed = 0f;

                animator.SetBool("attacking",false);
                animator.SetBool("idle",true);
                animator.SetBool("running",false);
            }
        }
    }


    IEnumerator Attack()
    {
        isCoroutineRunning =true;
        yield return new WaitForSeconds(attackDuration);

        if (distanceFromPlayer < 3)
        {
            player.GetComponent<PlayerStats>().takeDamage(damage);
        }
        else{
            isCoroutineRunning = false;
            StopCoroutine(Attack());
        }
        StartCoroutine(Attack());
    }
    
    public void takeDamage(float damage){
        health-=damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }


    void AttackAnimationDone(){
        isAttackAnimating = false;
    }
}
