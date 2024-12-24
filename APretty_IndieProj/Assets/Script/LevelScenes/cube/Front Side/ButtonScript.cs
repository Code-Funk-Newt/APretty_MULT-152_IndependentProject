using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public delegate void ButtonClickedHandler(GameObject button);

    public bool clickedOn;
    public GameObject cubeScript;



    private void start(){
        clickedOn = false;

    }

    private void update(){
        

    }

    public void OnMouseUp()
    {
       if(!clickedOn){

        cubeScript.GetComponent<MultiButtonScript>().ButtonClickedOn(gameObject);
        clickedOn = true;
       
        }
        else{

        
        cubeScript.GetComponent<MultiButtonScript>().ButtonClickedOff(gameObject);
        clickedOn = false;



        }

        


    }
}
