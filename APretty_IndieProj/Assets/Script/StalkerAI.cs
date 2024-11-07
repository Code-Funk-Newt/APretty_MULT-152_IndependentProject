using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using Unity.VisualScripting;
using System.IO;
using System.Data.Common;
using UnityEngine.ProBuilder.MeshOperations;


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

    
    public bool hasPowerUp = false;
    public bool hasRotated = false;
    public GameObject laser;



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
        
        if(!playerIsCaught || !hasPowerUp){

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
 
         if(hasPowerUp){
            Debug.Log("hasPowerUp");
            StartCoroutine(scan());

            
            
         }
    
        
    }

void Patrol() 
{   
    


    agent.speed = patrolSpeed; 
    if (agent.remainingDistance < 0.5f) 
    { 
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; 
        agent.SetDestination(waypoints[currentWaypointIndex].position); 

    } 

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
        Debug.Log("stop");

        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Handle player losing the game
            Debug.Log("Player caught by the stalker! Game Over.");
            gameM.GetComponent<GameManagerScript>().gameOver();
            playerIsCaught = true;



        }
    }






    IEnumerator scan(){
        
        if(!hasRotated){
        StartCoroutine(Rotate(5));
        hasRotated = true;
        }
        Stop();
        yield return new WaitForSeconds(10f);
        hasPowerUp = false;
        hasRotated = false;



    }
    
    

      IEnumerator Rotate(float duration){   
        


        Vector3 startRotation = transform.eulerAngles;
        float endRotation = startRotation.z + 360.0f;
        float t = 0.0f;
        Debug.Log("Pre-while func");



        while ( t  < duration)
        {   
            laser.SetActive(true);


            Stop();

            Debug.Log(" while - t : " + t);

            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation.z, endRotation, t / duration) % 360.0f;
            transform.eulerAngles = new Vector3(startRotation.x, yRotation, startRotation.z);
            yield return null;
        }


        laser.SetActive(false);
        Debug.Log("laser off");  
        //Stop();
        //yield return new WaitForSeconds(10f);




        //Debug.Log("coroutine DONE");
        //hasPowerUp = false;


    }






    void BoostedCharge(){
        Rigidbody rbStalkerBot = gameObject.GetComponent<Rigidbody>();

        Vector3 boostedPath = player.position - transform.position;

        rbStalkerBot.AddForce(boostedPath, ForceMode.Impulse );
    }
}