using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ElectricalPanelLaser : MonoBehaviour
{
    public GameObject greenButton;
    public GameObject redButton;
    public GameObject offline;
    public GameObject online;
    public GameObject laserWall;
    public GameObject lockLight;
    public Material redlight;
    public Material greenlight;
    public Material offlight;
    public Material greenBtnColor;
    public Material redBtnColor;

    public AudioSource asPlayer;
    public AudioClip powerDown;
    public AudioClip buttonActive;

    public bool puzzleSolved = false; // This should be set to true when the puzzle is solved

     


    private void Start()
    {
        Debug.Log("test panel");

     // Ensure buttons are interactive only if the puzzle is solved
        greenButton.SetActive(puzzleSolved);
        redButton.SetActive(puzzleSolved);
        online.SetActive(puzzleSolved);
        offline.SetActive(puzzleSolved);
        asPlayer=GetComponent<AudioSource>();


        
        
    }




    private void Update()
    {
        
        


        // Check if the puzzle is solved
        if (puzzleSolved)
        {
            // Enable button interaction
            greenButton.GetComponent<Renderer>().material = greenBtnColor;
            redButton.GetComponent<Renderer>().material = redBtnColor;
            greenButton.SetActive(true);
            redButton.SetActive(true);
            online.SetActive(true);
            offline.SetActive(false);

        }
        else
        {
            // Disable button interaction
            greenButton.GetComponent<Renderer>().material = offlight;
            redButton.GetComponent<Renderer>().material = offlight;
            offline.SetActive(true);
            online.SetActive(false);
            lockLight.GetComponent<Renderer>().material = offlight;

        }

       




    }





    public void OnGreenButtonClick()
    {
        if (puzzleSolved)
        {
            //Debug.Log("greenclick");
            asPlayer.PlayOneShot(buttonActive,0.5f); //sound
            OpenDoor();
        }
    }

    public void OnRedButtonClick()
    {
        if (puzzleSolved)
        {
            //Debug.Log("redclick");
            asPlayer.PlayOneShot(buttonActive,0.5f); //sound
            LockDoor();
        }
    }

    public void OpenDoor()
    {
        
        // Turn On laserwall

        laserWall.GetComponent<LaserWall>().turnOnLaserWall();
        lockLight.GetComponent<Renderer>().material = greenlight; //green light on
    }

    public void LockDoor()
    {
        // Turn Off laserwall
        asPlayer.PlayOneShot(powerDown, 0.7f);
        laserWall.GetComponent<LaserWall>().turnOffLaserWall();
        lockLight.GetComponent<Renderer>().material = redlight; //red light on
    }

    
}