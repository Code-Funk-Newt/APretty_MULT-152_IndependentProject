using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Door : MonoBehaviour
{
    private bool isOpen = false;
    public float moveSpeed = 3f; // Speed at which the door moves up and down
    public float openHeight = 9f; // Height to which the door moves up

    public GameObject doorSelected;

   

    private Vector3 initialPosition;

    private void Start()
    {

        initialPosition = transform.position;


    }

    private void Update(){
        
        OnTriggerEnter();
        OnTriggerExit();
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

    private void OnTriggerEnter()
    {
        if (isOpen)
        {   
            Open();
            StopAllCoroutines(); // Stop any ongoing movement
            StartCoroutine(MoveDoor(initialPosition + new Vector3(0, openHeight, 0)));
        }
    }

    private void OnTriggerExit()
    {
        if (!isOpen)
        {   
            Lock();
            StopAllCoroutines(); // Stop any ongoing movement
            StartCoroutine(MoveDoor(initialPosition));
        }
    }

    private IEnumerator MoveDoor(Vector3 targetPosition)
    {
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}