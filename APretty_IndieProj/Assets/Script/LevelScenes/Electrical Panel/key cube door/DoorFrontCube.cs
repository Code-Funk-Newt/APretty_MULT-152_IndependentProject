using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DoorFrontCube : MonoBehaviour
{
    private bool isOpen = false;
    public float moveSpeed = 3f; // Speed at which the door moves up and down
    public float openHeight = 9f; // Height to which the door moves up
    private Vector3 initialPosition;

    public AudioClip doorUpsound;

    private AudioSource asPlayer;




    private void Start()
    {

        initialPosition = transform.position;
        asPlayer = GetComponent<AudioSource>();


    }
    private void Update(){
        
        
        
    }








    public void Open()
    {
        if (!isOpen)
        {
            Debug.Log("Door now isOpen.");
            isOpen = true;
            // Add animation or other effects here
        }
    }

    public void Lock()
    {
        if (isOpen)
        {
            Debug.Log("Door is now locked.");
            isOpen = false;
            // Add animation or other effects here
        }
    }

    public void shutDown(){
        isOpen = false;
    }













    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Triggered");
        if (isOpen)
        {   
            
            Open();
            StopAllCoroutines(); // Stop any ongoing movement
            StartCoroutine(MoveDoor(initialPosition + new Vector3(0, openHeight, 0)));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Untriggered");
        if (isOpen)
        {   
            
            //Lock();
            StopAllCoroutines(); // Stop any ongoing movement
            StartCoroutine(MoveDoor(initialPosition));
        }
    }

    private IEnumerator MoveDoor(Vector3 targetPosition)
    {
        while (transform.position != targetPosition)
        {
            asPlayer.PlayOneShot(doorUpsound, 0.5f);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}