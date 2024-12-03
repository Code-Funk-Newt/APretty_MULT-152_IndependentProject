using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class consoleButton : MonoBehaviour
{
    // Start is called before the first frame update
public GameObject console;



private void OnMouseUpAsButton(){
    
    Debug.Log("RedButton Clicked");
    console.GetComponent<CameraConsole>().OnRedButtonClick();
}
}
