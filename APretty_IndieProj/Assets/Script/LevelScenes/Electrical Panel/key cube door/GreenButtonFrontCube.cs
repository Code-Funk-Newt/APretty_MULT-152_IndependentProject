using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenButtonFrontCube : MonoBehaviour
{

public GameObject ePanel;


private void OnMouseUpAsButton(){

    

        
    ePanel.GetComponent<frontSideElectricalPanel>().OnGreenButtonClick();

}

}
