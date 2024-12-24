using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeButtonScript : MonoBehaviour
{
    // Start is called before the first frame update

public bool btnClicked; 

public GameObject camerasecuritySystem;

public Material greenBtnColor;
public Material offBtnColor;
public AudioClip btnClick; 
public AudioClip btnClickOff;
public AudioClip SystemOn;
public AudioSource asPlayer;

private void start(){
    btnClicked = false; 


}





private void OnMouseUpAsButton(){
    

    if(!btnClicked){

    btnClicked = true;
    asPlayer.PlayOneShot(btnClick, 0.7f);               // sound of the button
    GetComponent<Renderer>().material = greenBtnColor;      // color of the activation

    camerasecuritySystem.GetComponent<CameraConsole>().puzzleSolved = true;     // passing info to puzzleSolve script

    asPlayer.PlayOneShot(SystemOn, 0.3f);               // sound of the button

    }
    else{
    

    btnClicked = false; 
    asPlayer.PlayOneShot(btnClickOff, 0.7f);               // sound of the button
    GetComponent<Renderer>().material = offBtnColor;
    camerasecuritySystem.GetComponent<CameraConsole>().puzzleSolved = false;     // passing info to puzzleSolve script



    }


}   

}
