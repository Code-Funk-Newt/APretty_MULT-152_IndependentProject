using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
public GameObject camerasecuritySystem;
public Material greenBtnColor;
public AudioClip btnClick; 
public AudioClip SystemOn;
public AudioSource asPlayer; 





private void OnMouseUpAsButton(){
    
    //Debug.Log("Cube Clicked");


    asPlayer.PlayOneShot(btnClick, 0.7f);               // sound of the button
    GetComponent<Renderer>().material = greenBtnColor;      // color of the activation

    camerasecuritySystem.GetComponent<CameraConsole>().puzzleSolved = true;     // passing info to puzzleSolve script

    asPlayer.PlayOneShot(SystemOn, 0.5f);               // sound of the button

}   

}
