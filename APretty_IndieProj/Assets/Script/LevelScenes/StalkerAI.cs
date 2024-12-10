using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using Unity.VisualScripting;
using System.IO;
using System.Data.Common;
using UnityEngine.ProBuilder.MeshOperations;
using TMPro;


public class StalkerAI : MonoBehaviour
{
    public Transform[] waypoints;
    public Transform player;
    public GameObject gameManager;
    public float detectionRange = 15f;
    public float chaseSpeed = 3.5f;
    public float patrolSpeed = 2f;
    
    

    private NavMeshAgent agent;
    private int currentWaypointIndex;
    private int waypointCounter;
    public int waypointRequired = 5;



    private bool isChasing;
    public bool playerIsCaught;


    
    public bool hasPowerUp = false;
    private bool hasRotated = false;
    private bool laserHit = false;


    public GameObject laser;
    public GameObject laserVisual;
    public GameObject targetLockedHUD;




    public GameObject laserActiveHUDIcon;
    public TextMeshProUGUI requiredNumUI;




    public AudioClip patrolSound;
    public AudioClip chaseSound;
    public AudioClip creepywoosh;
    private AudioSource asPlayer;
   

    void Start()
    {   

        waypointCounter = 0;

        agent = GetComponent<NavMeshAgent>();
        currentWaypointIndex = 0;
        isChasing = false;
        asPlayer = GetComponent<AudioSource>();
        requiredNumUI.text = "" + waypointRequired;



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
                CancelInvoke(); 
            }
    
        
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

        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rbStalker = gameObject.GetComponent<Rigidbody>();
            rbStalker.constraints = RigidbodyConstraints.FreezeRotation;

            // Handle player losing the game
            Debug.Log("Player caught by the stalker! Game Over.");
            gameManager.GetComponent<GameManagerScript>().gameOver();
            playerIsCaught = true;
            



        }
    }






    IEnumerator scan(){                         //scan routine
        if(!playerIsCaught){

        if(!hasRotated){                //checks for 360 rotation hass happend. only triggers once
        StartCoroutine(Rotate(5));
        hasRotated = true;
        laserActiveHUDIcon.SetActive(true);  //HUD laser Icon: ON

        }

        Stop();


        yield return new WaitForSeconds(5f);
        laser.SetActive(false);
        laserVisual.SetActive(false);

        


        if(laserHit){
            asPlayer.PlayOneShot(creepywoosh, 0.9f);
            BoostedCharge();
        }
        yield return new WaitForSeconds(2f);

        laserHit = false;
        hasPowerUp = false;
        hasRotated = false;
        
        laserActiveHUDIcon.SetActive(false);    //HUD laser Icon: OFF

        }

    }
    


    

      IEnumerator Rotate(float duration){                   // 360 spin coroutine
        


        Vector3 startRotation = transform.eulerAngles;
        float endRotation = startRotation.z + 360.0f;
        float t = 0.0f;



        while ( t  < duration)          //while loop for rotation
        {   
            if(laserHit == true){       //stops rotation


            laser.SetActive(false);                 // turns laser off if player is hit by laser
            laserVisual.SetActive(false);
            targetLockedHUD.SetActive(true);


            yield break;
            }


            laser.SetActive(true);
            laserVisual.SetActive(true);



            Stop();

            //Debug.Log(" while - t : " + t);    // <-- counting the relative rotation

            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation.z, endRotation, t / duration) % 360.0f;
            transform.eulerAngles = new Vector3(startRotation.x, yRotation, startRotation.z);
            yield return null;
        }


        laser.SetActive(false);                     //turns laser off after rotation
        laserVisual.SetActive(false);
        

        
        Debug.Log("laser off");  


    }






    public void BoostedCharge(){

        targetLockedHUD.SetActive(false);           // turns target HUD off


        Rigidbody rbStalkerBot = gameObject.GetComponent<Rigidbody>();

        Vector3 boostedPath = player.position - transform.position;

        rbStalkerBot.AddForce(boostedPath, ForceMode.Impulse );

    }





    public void laserHitPlayer(){

        laserHit = true; 
        



        Rigidbody rbStalker = gameObject.GetComponent<Rigidbody>();             //stops robo
        rbStalker.constraints = RigidbodyConstraints.FreezeRotation;
        
    }





    void waypointcounter(){
         

        if(waypointCounter > waypointRequired){

            if(waypointRequired > 1 && !hasRotated){                     //makes sure the waypoint required does go negative & 
                waypointRequired -= 1;                                   //only happens once per coroutine

                //Debug.Log("Waypoint Required: "+ waypointRequired);
                requiredNumUI.text = "" + waypointRequired;                // Required Waypoints for laser, displayed on HUD

            }
            
            waypointCounter = 0;

            StartCoroutine(scan());
        }
    }


    public IEnumerator cameraChase(){

        
        
        Debug.Log("player is targeted");
        isChasing = true;
        InvokeRepeating("ChasePlayer", 0.0f, 0.001f);

  

        yield return new WaitForSeconds(10);


        CancelInvoke("ChasePlayer");
        isChasing = false; 
        
        Debug.Log("DONE chase");


       
        
     }
        
}