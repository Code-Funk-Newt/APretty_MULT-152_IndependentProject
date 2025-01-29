using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

[System.Serializable]
public class ButtonMod
{
    public bool buttonValue;  // Representing if the button is active (true) or not (false)
}

public class frontSideElectricalPanel : MonoBehaviour
{
    public GameObject greenButton;
    public GameObject redButton;
    public GameObject offline;
    public GameObject online;
    public GameObject door;
    public GameObject lockLight;
    public GameObject cubeFrontSide;
    public Material redlight;
    public Material greenlight;
    public Material offlight;
    public Material greenBtnColor;
    public Material redBtnColor;

    public AudioSource asPlayer;
    public AudioClip buttonActive;

    public bool puzzleSolved = false;

    public List<ButtonMod> buttonPatternList = new List<ButtonMod>();

    private void Start()
    {
        Debug.Log("test panel");

        if (asPlayer == null)
        {
            asPlayer = GetComponent<AudioSource>();
            if (asPlayer == null)
                Debug.LogWarning("AudioSource component not found.");
        }

        UpdateButtonState();
    }

    private void UpdateButtonState()
    {
        bool isActive = puzzleSolved;
        //greenButton.SetActive(isActive);
        //redButton.SetActive(isActive);
        online.SetActive(isActive);
        offline.SetActive(!isActive);
       
    }

    private void Update()
    {
        if (CheckMatch())
        {   

            puzzleSolved=true;
            greenButton.GetComponent<Renderer>().material = greenBtnColor;
            redButton.GetComponent<Renderer>().material = redBtnColor;
            greenButton.SetActive(true);
            redButton.SetActive(true);
            online.SetActive(true);
            offline.SetActive(false);
        }
        else
        {   
            puzzleSolved=false;
            greenButton.GetComponent<Renderer>().material = offlight;
            redButton.GetComponent<Renderer>().material = offlight;
            offline.SetActive(true);
            online.SetActive(false);
            lockLight.GetComponent<Renderer>().material = offlight;
        }

        //CheckMatch();
    }




    public void OnGreenButtonClick()
    {
        if (!puzzleSolved)
        {
            Debug.Log("Puzzle not solved yet.");
            return;
        }

        Debug.Log("Green button clicked");
        asPlayer.PlayOneShot(buttonActive, 0.5f);
        OpenDoor();
    }

    public void OnRedButtonClick()
    {
        if (!puzzleSolved)
        {
            Debug.Log("Puzzle not solved yet.");
            return;
        }

        Debug.Log("Red button clicked");
        asPlayer.PlayOneShot(buttonActive, 0.5f);
        LockDoor();
    }




    public void OpenDoor()
    {

        Debug.Log("LIGHT SHOULD BE GREEN");
        door.GetComponent<DoorFrontCube>().Open();
        lockLight.GetComponent<Renderer>().material = greenlight;
    }




    public void LockDoor()
    {
        door.GetComponent<DoorFrontCube>().Lock();
        lockLight.GetComponent<Renderer>().material = redlight;
    }









    public bool CheckMatch()
    {

    // Get the list of button states from MultiButtonScript
    var buttonStates = cubeFrontSide.GetComponent<MultiButtonScript>().buttons;

    // Ensure the button counts match
    if (buttonPatternList.Count != buttonStates.Count)
    {
        Debug.Log("Number of buttons are off");
        return false;
    }
    else{
    // Loop through the buttons and compare buttonValue with clickedOff state
    for (int i = 0; i < buttonPatternList.Count; i++)
    {
        // Get the boolean values to compare
        bool buttonValue = buttonPatternList[i].buttonValue;
        bool clickedOffState = buttonStates[i].GetComponent<ButtonScript>().clickedOff; // Assuming clickedOff is a boolean in MultiButtonScript
        
        if (buttonValue != clickedOffState)
        {
            //Debug.Log($"Mismatch at index {i}: buttonValue = {buttonValue}, clickedOff = {clickedOffState}");
            return false; // Return false if any mismatch is found
        }
    }

     // If all button values match the clickedOff states
    Debug.Log("Buttons are on");
    return true;
    }

    }

}

/**
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;



[System.Serializable]


public class ButtonMod{
        public bool buttonValue;     // creation of button value
    }



public class frontSideElectricalPanel : MonoBehaviour
{
    public GameObject greenButton;
    public GameObject redButton;
    public GameObject offline;
    public GameObject online;
    public GameObject door;
    public GameObject lockLight;
    public GameObject cubeFrontSide;
    public Material redlight;
    public Material greenlight;
    public Material offlight;
    public Material greenBtnColor;
    public Material redBtnColor;

    public AudioSource asPlayer;
    public AudioClip buttonActive;

    public bool puzzleSolved = false; // This should be set to true when the puzzle is solved




    public List<ButtonMod> buttonPatternList = new List<ButtonMod>();  // list insiated for button pattern squence


     


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
            //door.GetComponent<Door>().shutDown();

        }

       
       //Debug.Log(buttonPatternList.Count);

        CheckMatch();

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

    
    
    public bool CheckMatch(){

    if (buttonPatternList.Count != cubeFrontSide.GetComponent<MultiButtonScript>().buttons.Count){      //Checking whether the # of buttons are syncedd
        Debug.Log("Number of buttons are off");
        return false; 

        }
    else{
        
        Debug.Log(buttonPatternList.SequenceEqual(cubeFrontSide.GetComponent<MultiButtonScript>().buttons));

        /**
        for (int i = 0; i < buttonPatternList.Count; i++) {
            if (buttonPatternList[i] == )
        return false;
        }
        
        Debug.Log("Buttons are on");
        return true;

        }
        

    return true;
    }

    }
        

    
    
}
**/
