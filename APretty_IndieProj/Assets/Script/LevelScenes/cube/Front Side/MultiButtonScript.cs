using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private int buttonsClicked = 0;

    private void Start()
    {


    }

    private void Update(){


        if (buttonsClicked >= 3)                // the amounted needed to unlock a system
        {   
            asPlayer.PlayOneShot(SystemOn, 0.3f);
            doorSecuritySystem.GetComponent<ElectricalPanel>().puzzleSolved = true;   // turns electrical panel on
        }
        else{

            doorSecuritySystem.GetComponent<ElectricalPanel>().puzzleSolved = false;  // turns electrical panel off
        }



    }



    public void ButtonClickedOn(GameObject button){
        if(button.GetComponent<ButtonScript>().clickedOn == false ){        // perameter so we cant click twice on button

        asPlayer.PlayOneShot(btnClick, 0.7f);
        button.GetComponent<Renderer>().material = greenBtnColor;
        buttonsClicked++;  


        Debug.Log("ON cube button");

        }
    }

    public void ButtonClickedOff(GameObject button){


        if(button.GetComponent<ButtonScript>().clickedOn == true ){
            
            asPlayer.PlayOneShot(btnOff, 0.9f);
            button.GetComponent<Renderer>().material = offColor;
            buttonsClicked--;  

        
         Debug.Log("OFF cube button");

        }
    }
}