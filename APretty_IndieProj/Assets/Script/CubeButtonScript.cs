using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
public GameObject camerasecuritySystem;
public Material greenBtnColor;




private void OnMouseUpAsButton(){
    
    Debug.Log("Cube Clicked");
    //camerasecuritySystem.GetComponent<CameraConsole>().puzzleSolved = true;
}

}
