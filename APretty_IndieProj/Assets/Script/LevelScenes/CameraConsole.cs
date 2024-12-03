using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConsole : MonoBehaviour
{
    public GameObject redButton;
    public GameObject offline;
    public GameObject online;
    public GameObject securityCamera;
    public Material redlight;
    public Material offlight;

    public bool puzzleSolved = false; // This should be set to true when the puzzle is solved

     


    private void Start()
    {
        Debug.Log("camera console panel");

     // Ensure buttons are interactive only if the puzzle is solved
        online.SetActive(puzzleSolved);
        offline.SetActive(puzzleSolved);


        
        
    }




    private void Update()
    {
        
        


        // Check if the puzzle is solved
        if (puzzleSolved)
        {
            // Enable button interaction
            redButton.GetComponent<Renderer>().material = redlight;
            online.SetActive(true);
            offline.SetActive(false);

        }
        else
        {
            // Disable button interaction
            redButton.GetComponent<Renderer>().material = offlight;
            offline.SetActive(true);
            online.SetActive(false);

        }

       




    }


    public void OnRedButtonClick()
    {
        if (puzzleSolved)
        {
            Debug.Log("shutoffredclick");
            shutDownCamera();
        }
    }

    private void shutDownCamera()
    {
        // shuts camera down
        securityCamera.GetComponent<SecurityCamera>().shutDown();

        puzzleSolved = false; // button turns grey again

    }

}
