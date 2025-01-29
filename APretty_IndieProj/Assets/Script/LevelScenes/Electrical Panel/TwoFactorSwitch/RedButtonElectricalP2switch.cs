using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButtonElectricalP2switch : MonoBehaviour
{
public GameObject ePanel;



private void OnMouseUpAsButton(){
    
    ePanel.GetComponent<ElectricalP2switch>().OnRedButtonClick();
    ePanel.GetComponent<ElectricalP2switch>().deactivateSwitch();
}

}
