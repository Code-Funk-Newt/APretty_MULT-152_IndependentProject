using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButtonLaser : MonoBehaviour
{
public GameObject ePanel;



private void OnMouseUpAsButton(){
    
    ePanel.GetComponent<ElectricalPanelLaser>().OnRedButtonClick();
}
}

