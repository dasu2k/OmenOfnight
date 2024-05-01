using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ai : MonoBehaviour
{   
    public NavMeshAgent navMeshAgent;
    public float StartWaitTime = 4;
    public float timeToRotate = 2;
    public float speedWalk = 5;
    public float speedRun = 10;
    public float viewRadius = 15;
    public float viewAngle = 120;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public float meshResolution = 1f;
    public int edgeIterations = 4;  
    public float edgeDistance = 0.5f;

    public Transform[] waypoints;
    int m_CurrentWaypointIndex = 0;

    Vector3 playerLastPosition = Vector3.zero;
    Vector3 m_PlayerPosition = Vector3.zero;
    float m_WaitTime;
    float m_TimeToRotate;
    bool m_PlaterInRange;
    bool m_PlayerNear;
    bool m_IsPatrol;
    bool m_CaughtPlayer;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerPosition = Vector3.zero;
        navMeshAgent = GetComponent<NavMeshAgent>();
        m_WaitTime = StartWaitTime;
        m_TimeToRotate = timeToRotate;
        m_IsPatrol = true;
        m_PlaterInRange = false;
        m_CaughtPlayer = false; 

        m_CurrentWaypointIndex = 0;

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedWalk;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        
    }


    void Update()
    {
        EnviromentView();

     

        if (!m_IsPatrol)
        {
            Chasing();
        }
        else
        {
            Patroling();
        }
    }

    private void Chasing()
    {
        m_PlayerNear = true;
        playerLastPosition = Vector3.zero;
        if(!m_CaughtPlayer){
           
            Move(speedRun);
            navMeshAgent.SetDestination(m_PlayerPosition);
          
        }
        if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance){
            if(m_WaitTime <= 0 && !m_CaughtPlayer && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 6f){
                m_IsPatrol = true;
                m_PlayerNear = false;
                Move(speedWalk);
                m_TimeToRotate = timeToRotate;
                m_WaitTime = StartWaitTime;
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
            else{
                if(Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 2.5f){
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
                
            }
        }
    }

    private void Patroling(){
        if(m_PlayerNear){
            if(m_TimeToRotate <= 0){
                Move(speedWalk);
                LookingPlayer(playerLastPosition);
            }
            else{
                Stop();
                m_TimeToRotate -= Time.deltaTime;
            }
        }
        else{
            m_PlayerNear = false;
            playerLastPosition = Vector3.zero;
            if (m_CurrentWaypointIndex >= waypoints.Length)
            {
                m_CurrentWaypointIndex = 0;
            }
            
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance){
                if(m_WaitTime <= 0)
                {
                    NextPoint();
                    Move(speedWalk);
                    m_WaitTime = StartWaitTime;
                }
                else{
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
    }
    private void Move(float speed){
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
    }

    private void Stop(){
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }

    void CaughtPlayer()
    {
        m_CaughtPlayer = true;
    }

    public void NextPoint()
    {
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }

    void LookingPlayer(Vector3 playerPosition)
    {
        navMeshAgent.SetDestination(playerPosition);
        if(Vector3.Distance(transform.position, playerPosition) <= 0.3)
        {
            if(m_WaitTime <=0){
                m_PlayerNear = false;
                Move(speedWalk);
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                m_WaitTime = StartWaitTime;
                m_TimeToRotate = timeToRotate;
            }
            else{
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
    }

    void EnviromentView(){
        Collider[] playerInRadius = Physics.OverlapSphere(transform.position, viewRadius, playerMask);
        
        for(int i =0; i< playerInRadius.Length; i++){
            Transform player = playerInRadius[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2){
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);
                if(!Physics.Raycast(transform.position, dirToPlayer, distanceToPlayer, obstacleMask)){
                    m_PlayerPosition = player.position;
                    m_PlaterInRange = true;
                    m_PlayerNear = true;
                    m_IsPatrol = false;
                    LookingPlayer(m_PlayerPosition);
                }
                else{
                    m_PlaterInRange = false;
                    m_PlayerNear = false;
                }
            }
            if(Vector3.Distance(transform.position, player.position) > viewRadius){
                m_PlaterInRange = false;
            }
        }
        if(m_PlaterInRange){
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                m_PlayerPosition = player.transform.position;
            }
            else
            {
                Debug.Log("Player object not found");
            }
        }
    }
}
