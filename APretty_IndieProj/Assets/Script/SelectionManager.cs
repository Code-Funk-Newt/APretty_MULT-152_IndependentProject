using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    // bringing in info for refrence
    public GameObject[] Panel;



    void Update()
    {   
        //mouse left click down
        if(Input.GetKey(KeyCode.Mouse0)){
        
        //created ray
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit)){
            //creating name var that raycast hit
            var selection = hit.collider.gameObject.name;


            if(selection == "Green"){
            Debug.Log("Selection Script: green button");
            Panel[0].GetComponent<ElectricalPanel>().OnGreenButtonClick();
            }


            if(selection == "Red"){
                Debug.Log("Selection Script: red button");
            Panel[0].GetComponent<ElectricalPanel>().OnRedButtonClick();

            }
        }
        }
    }
}
