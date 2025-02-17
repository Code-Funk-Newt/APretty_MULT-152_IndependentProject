using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ElectricalP2switch : MonoBehaviour
{
    public GameObject greenButton;
    public GameObject redButton;
    public GameObject offline;
    public GameObject online;
    public GameObject door;
    public GameObject lockLight;
    public GameObject secondSwitch;
    public Material redlight;
    public Material greenlight;
    public Material offlight;
    public Material greenBtnColor;
    public Material redBtnColor;

    public AudioSource asPlayer;
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
            // Enable button interactions/ONLINE Mode
            greenButton.GetComponent<Renderer>().material = greenBtnColor;
            redButton.GetComponent<Renderer>().material = redBtnColor;
            greenButton.SetActive(true);
            redButton.SetActive(true);
            online.SetActive(true);
            offline.SetActive(false);

        }
        else
        {
            // Disable buttons interactions/OFFLINE Mode
            greenButton.GetComponent<Renderer>().material = offlight;
            redButton.GetComponent<Renderer>().material = offlight;
            offline.SetActive(true);
            online.SetActive(false);
            lockLight.GetComponent<Renderer>().material = offlight;
            door.GetComponent<Door>().shutDown();

        }

       




    }





    public void OnGreenButtonClick()
    {
        if (puzzleSolved)
        {
            Debug.Log("greenclick");
            asPlayer.PlayOneShot(buttonActive,0.5f); //sound
            OpenDoor();
        }
    }

    public void OnRedButtonClick()
    {
        if (puzzleSolved)
        {
            Debug.Log("redclick");
            asPlayer.PlayOneShot(buttonActive,0.5f); //sound
            LockDoor();
        }
    }

    private void OpenDoor()
    {
        // Logic to open the door
        Debug.Log("greenclick:ii");
        door.GetComponent<Door>().Open();
        lockLight.GetComponent<Renderer>().material = greenlight; //green light on
    }

    private void LockDoor()
    {
        // Logic to lock the door
        door.GetComponent<Door>().Lock();
        lockLight.GetComponent<Renderer>().material = redlight; //red light on
    }


    public void activateSwitch(){
        secondSwitch.GetComponent<ElectricalPanelLaser>().OpenDoor();
    }

    public void deactivateSwitch(){
        secondSwitch.GetComponent<ElectricalPanelLaser>().LockDoor();
    }



    
}