using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instructionTrigger : MonoBehaviour
{
    public GameObject pickupInstructionText;  // script controlling the fade transitions
    public GameObject viewInstructionText;
    public GameObject rotateInstructionText;


    public GameObject playerCharacter; // to access PickUpScript

    private bool alltextdisplayed;   // will be true once all texts are displayed

    private int timesInstructionsInstan;


    // Start is called before the first frame update
    void Start()
    {

    alltextdisplayed = false; 

    }

    // Update is called once per frame
    void Update()
    {

        if(playerCharacter.GetComponent<PickUpScript>().objectIsHeld == true 
        && !alltextdisplayed){

            viewInstructionText.GetComponent<textInstruction>().playerInTrigger = true; // displays the view instructions

            rotateInstructionText.GetComponent<textInstruction>().playerInTrigger = false;

            if(Input.GetKey(KeyCode.R)){

                viewInstructionText.GetComponent<textInstruction>().playerInTrigger = false;

                rotateInstructionText.GetComponent<textInstruction>().playerInTrigger = true;


                if(Input.GetKey(KeyCode.Q)){
                
                alltextdisplayed = true;
                rotateInstructionText.GetComponent<textInstruction>().playerInTrigger = false;

                }

            }


        }

    }

    public void OnTriggerEnter(Collider other){

        
        if(other.tag == "Player" 
        && playerCharacter.GetComponent<PickUpScript>().objectIsHeld == false   // IF is objectIsHeld=false in PickUpScript
        && timesInstructionsInstan < 8) 
        {
        

            pickupInstructionText.GetComponent<textInstruction>().playerInTrigger = true; //sends bool to trigger text 
            timesInstructionsInstan +=1; // count number of times picked up

            Debug.Log("Number of instanicated : " + timesInstructionsInstan);

            

        }
    }
    
    public void OnTriggerExit(Collider other){
        if(other.tag == "Player")
        {
    

            pickupInstructionText.GetComponent<textInstruction>().playerInTrigger = false; 

        }
    }


}
