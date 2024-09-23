using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenButton : MonoBehaviour
{

public GameObject ePanel;
private void OnMouseUpAsButton(){
    
    Debug.Log("Yuupiee");
    
    ePanel.GetComponent<ElectricalPanel>().OnGreenButtonClick();

}
}
