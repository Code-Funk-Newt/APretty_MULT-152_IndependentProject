using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenButton : MonoBehaviour
{

public GameObject ePanel;
private void OnMouseUpAsButton(){
        
    ePanel.GetComponent<ElectricalPanel>().OnGreenButtonClick();

}
}
