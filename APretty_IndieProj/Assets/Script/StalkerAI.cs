using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;


public class StalkerAI : MonoBehaviour
{
    public Transform[] waypoints;
    public Transform player;
    public GameObject gameM;
    public float detectionRange = 15f;
    public float chaseSpeed = 3.5f;
    public float patrolSpeed = 2f;
    
    

    private NavMeshAgent agent;
    private int currentWaypointIndex;
    private bool isChasing;
    public bool playerIsCaught;

    public AudioClip patrolSound;
    public AudioClip chaseSound;
    private AudioSource asPlayer;
   

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentWaypointIndex = 0;
        isChasing = false;
        asPlayer = GetComponent<AudioSource>();
        Patrol();
        
    }

    void Update()
    {
        
        if(!playerIsCaught){

        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
            DetectPlayer();
        }

        }
        else{
            Stop();
        }

    
        
    }

void Patrol() 
{   
    


    agent.speed = patrolSpeed; 
    if (agent.remainingDistance < 0.5f) 
    { 
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; 
        agent.SetDestination(waypoints[currentWaypointIndex].position); 
        StartCoroutine(RandomPause()); 
    } 

} 
 
IEnumerator RandomPause() 
{ 
    float pauseDuration = Random.Range(1f, 3f); 
    yield return new WaitForSeconds(pauseDuration); 
} 






    

    void DetectPlayer()
    {
        float distanceToPlayer = (player.position - transform.position).sqrMagnitude;
        if (distanceToPlayer < detectionRange * detectionRange)
        {
            
            asPlayer.clip = chaseSound; //switch to chaseSound 
            asPlayer.Play();


            isChasing = true;
        }
    }

    void ChasePlayer()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);

        float distanceToPlayer = (player.position - transform.position).sqrMagnitude;
        if (distanceToPlayer > detectionRange * detectionRange)
        {
            isChasing = false;    // chase is over


            asPlayer.clip = patrolSound;    //switch to patrolSound
            asPlayer.Play();


        }
        
    }

    void Stop(){

        agent.SetDestination( transform.position );
        agent.speed = 0;
        asPlayer.Stop();
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Handle player losing the game
            Debug.Log("Player caught by the stalker! Game Over.");
            gameM.GetComponent<GameManagerScript>().gameOver();
            playerIsCaught = true;



            // Add your game over logic here
        }
    }
}