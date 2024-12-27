using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public delegate void ButtonClickedHandler(GameObject button);

    public bool clickedOff;
    public GameObject cubeScript;



    private void start(){
        clickedOff = false;

    }

    private void update(){
        

    }

    public void OnMouseUp()
    {
       if(!clickedOff){

        cubeScript.GetComponent<MultiButtonScript>().ButtonClickedOn(gameObject);
        clickedOff = true;
       
        }
        else{

        
        cubeScript.GetComponent<MultiButtonScript>().ButtonClickedOff(gameObject);
        clickedOff = false;



        }

        


    }
}
