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
    private int waypointCounter;
    private int waypointRequired;



    private bool isChasing;
    public bool playerIsCaught;


    
    public bool hasPowerUp = false;
    public bool hasRotated = false;
    public bool laserHit = false;
    public GameObject laser;




    public AudioClip patrolSound;
    public AudioClip chaseSound;
    public AudioClip creepywoosh;
    private AudioSource asPlayer;
   

    void Start()
    {   

        waypointRequired = 5;   // whats required for laser activation 
        waypointCounter = 0;

        agent = GetComponent<NavMeshAgent>();
        currentWaypointIndex = 0;
        isChasing = false;
        asPlayer = GetComponent<AudioSource>();


        Patrol();   //start moving
        
    }

    void Update()
    {

        waypointcounter();

        
        
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
            if(playerIsCaught){
                Stop();
            }

            //if(hasPowerUp){
            //Debug.Log("hasPowerUp");
            //StartCoroutine(scan());
            
            //}
    
        
    }

void Patrol() 
{   
    


    agent.speed = patrolSpeed; 
    if (agent.remainingDistance < 0.5f) 
    {   


        
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; 
        agent.SetDestination(waypoints[currentWaypointIndex].position); 


        Debug.Log("Waypoint Counter: "+ waypointCounter);
        waypointCounter +=1;
        
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






    IEnumerator scan(){                         //scan routine
        
        if(!hasRotated){                //checks for 360 rotation hass happend. only triggers once
        StartCoroutine(Rotate(5));
        hasRotated = true;
        }

        Stop();


        yield return new WaitForSeconds(5f);
        laser.SetActive(false);
        if(laserHit){
            asPlayer.PlayOneShot(creepywoosh, 0.9f);
            BoostedCharge();
        }
        yield return new WaitForSeconds(2f);

        laserHit = false;
        hasPowerUp = false;
        hasRotated = false;



    }
    
    

      IEnumerator Rotate(float duration){                   // 360 spin coroutine
        


        Vector3 startRotation = transform.eulerAngles;
        float endRotation = startRotation.z + 360.0f;
        float t = 0.0f;
        Debug.Log("Pre-while func");



        while ( t  < duration)          //while loop for rotation
        {   
            if(laserHit == true){       //stops rotation
            yield break;
            }


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


    }






    public void BoostedCharge(){
        Rigidbody rbStalkerBot = gameObject.GetComponent<Rigidbody>();

        Vector3 boostedPath = player.position - transform.position;

        rbStalkerBot.AddForce(boostedPath, ForceMode.Impulse );
    }


    public void laserHitPlayer(){
        Debug.Log("laserHitPlayer: stop corountine");
        laserHit = true; 
        





        Rigidbody rbStalker = gameObject.GetComponent<Rigidbody>();
        rbStalker.constraints = RigidbodyConstraints.FreezeRotation;

       
        
    }

    void waypointcounter(){
         

        if(waypointCounter > waypointRequired){

            if(waypointRequired > 1 && !hasRotated){                     //makes sure the waypoint required does go negative & 
                waypointRequired -= 1;                                   //only happens once per coroutine
                Debug.Log("Waypoint Required: "+ waypointRequired);

            }
            
            waypointCounter = 0;

            StartCoroutine(scan());
        }
    }
        
}