using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButtonFrontCube : MonoBehaviour
{
    
public GameObject ePanel;



private void OnMouseUpAsButton(){
    
    ePanel.GetComponent<frontSideElectricalPanel>().OnRedButtonClick();
}
}

