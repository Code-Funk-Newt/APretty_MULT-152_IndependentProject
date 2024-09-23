using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButton : MonoBehaviour
{
public GameObject ePanel;



private void OnMouseUpAsButton(){
    
    Debug.Log("Duupiee");
    ePanel.GetComponent<ElectricalPanel>().OnRedButtonClick();
}
}
