using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserAction : MonoBehaviour
{
    public GameObject stalker;
    public GameObject player;
    public GameObject robotEye;
    private AudioSource asPlayer;
    public AudioClip TargetLocked;
    public AudioClip LaserBeam;


    // Start is called before the first frame update
    void Start()
    {
        asPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {   
            //asPlayer.PlayOneShot(TargetLocked, 0.9f);

            player.GetComponent<PlayerController>().asPlayer.PlayOneShot(TargetLocked, 0.7f);


            stalker.GetComponent<StalkerAI>().laserHitPlayer();
            
            // Handle player losing the game
            Debug.Log("Player caught by the Scanner");


        }
    }

}