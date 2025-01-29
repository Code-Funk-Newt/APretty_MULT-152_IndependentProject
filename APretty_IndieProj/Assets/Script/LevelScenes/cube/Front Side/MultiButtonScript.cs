using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



[System.Serializable]
public class BooleanItem{
        public bool value;
    }

public class MultiButtonScript : MonoBehaviour
{
    public GameObject doorSecuritySystem;
    public Material greenBtnColor;
    public Material offColor;
    public AudioClip btnClick;
    public AudioClip btnOff;
    public AudioClip SystemOn;
    public AudioSource asPlayer;


    public List<GameObject> buttons; // List of all button GameObjects



 

    // Public list of BooleanItem
    public List<BooleanItem> booleanLockList = new List<BooleanItem>();





    private int buttonsClicked = 0;

    public bool soundSwitch;       // boolean to make sure if statement stops repeating



    private void Start()
    {
    
        // Initialize the list with some values
        booleanLockList.Add(new BooleanItem { value = false });
        booleanLockList.Add(new BooleanItem { value = false });
        booleanLockList.Add(new BooleanItem { value = false });


        soundSwitch = true;          // boolean to make sure if statement stops repeating

    }

    private void Update(){

        

        if (buttonsClicked == 3 && doorSecuritySystem.GetComponent<frontSideElectricalPanel>().CheckMatch() && soundSwitch)         // the number of buttons needed to unlock a system and if the puzzle is solved = sound
        {   
            asPlayer.PlayOneShot(SystemOn, 0.3f);
            Debug.Log("MultiButton : Sound and Puzzle solved ");
            soundSwitch = false;
            
        }
        
        if(buttonsClicked < 3 && !doorSecuritySystem.GetComponent<frontSideElectricalPanel>().CheckMatch() && !soundSwitch){

            soundSwitch = true;
            
        }


        // DEBUG LIST CHECK:

        /**        
        if (booleanLockList.Count > 0)
        {
            for(int x = 0; x < 9; x++){
            Debug.Log( x +"boolean value: " + booleanLockList[x].value);
            }

        }
        **/

        
        // DEBUG LIST CHECK for frontCube:
        /**
        if (buttons.Count > 0)
        {
            for(int x = 0; x < 9; x++){
            Debug.Log( x +" boolean value: " + buttons[x].GetComponent<ButtonScript>().clickedOff);
            }

        }
        **/
        


        
        }


 



    public void ButtonClickedOn(GameObject button){

        if(button.GetComponent<ButtonScript>().clickedOff == false ){        // perameter so we cant click twice on button

        asPlayer.PlayOneShot(btnClick, 0.7f);
        button.GetComponent<Renderer>().material = greenBtnColor;
        buttonsClicked++;  


        Debug.Log("ON cube button");

        }






    }

    public void ButtonClickedOff(GameObject button){


        if(button.GetComponent<ButtonScript>().clickedOff == true ){
            
            asPlayer.PlayOneShot(btnOff, 0.9f);
            button.GetComponent<Renderer>().material = offColor;
            buttonsClicked--;  

        
         Debug.Log("OFF cube button");

        }
    }
}