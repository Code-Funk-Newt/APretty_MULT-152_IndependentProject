using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenButtonElectricalP2switch : MonoBehaviour
{
public GameObject ePanel;

private void OnMouseUpAsButton(){
        
    ePanel.GetComponent<ElectricalP2switch>().OnGreenButtonClick();
    ePanel.GetComponent<ElectricalP2switch>().activateSwitch();

}

}
