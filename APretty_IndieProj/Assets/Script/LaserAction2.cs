using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAction2 : MonoBehaviour
{
    public GameObject stalker;
    public GameObject player;
    public GameObject robotEye;
    private AudioSource asPlayer;
    public AudioClip TargetLocked;
    public AudioClip LaserBeam;
    public float rayDistance = 10f; // Distance the ray will travel

    // Start is called before the first frame update
    void Start()
    {
        asPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 forward = robotEye.transform.TransformDirection(Vector3.forward) * rayDistance;

        // Draw the ray in the scene view for debugging
        Debug.DrawRay(robotEye.transform.position, forward, Color.red);

        // Perform the raycast
        if (Physics.Raycast(robotEye.transform.position, forward, out hit, rayDistance))
        {
            Debug.Log("Raycaster: " + hit.collider.tag);

            if (hit.collider.CompareTag("Player"))
            {
                
                // Play the TargetLocked sound
                asPlayer.PlayOneShot(TargetLocked, 0.9f);

                // Play the sound on the player's AudioSource
                player.GetComponent<AudioSource>().PlayOneShot(TargetLocked, 0.7f);

                // Call the laserHitPlayer method on the stalker
                stalker.GetComponent<StalkerAI>().laserHitPlayer();

                // Handle player losing the game
                Debug.Log("Player caught by the Scanner");
                
            }
        }
    }
}
