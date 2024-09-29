using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;


public class StalkerAI : MonoBehaviour
{
    public Transform[] waypoints;
    public Transform player;
    public float detectionRange = 15f;
    public float chaseSpeed = 3.5f;
    public float patrolSpeed = 2f;

    private NavMeshAgent agent;
    private int currentWaypointIndex;
    private bool isChasing;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentWaypointIndex = 0;
        isChasing = false;
        Patrol();
    }

    void Update()
    {
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
            isChasing = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Handle player losing the game
            Debug.Log("Player caught by the stalker! Game Over.");
            // Add your game over logic here
        }
    }
}