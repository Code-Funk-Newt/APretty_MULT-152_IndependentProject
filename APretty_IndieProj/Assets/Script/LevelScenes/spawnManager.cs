using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    public GameObject xRayPowerUp;
    private int powerUpCount;
    public Transform[] waypoints;





       

    // Start is called before the first frame update
    void Start()
    {   

        Instantiate(xRayPowerUp, locationShift() ,xRayPowerUp.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        powerUpCount = FindObjectsOfType<powerUp>().Length;

        if(powerUpCount < 1){
            Instantiate(xRayPowerUp, locationShift() ,xRayPowerUp.transform.rotation);

        }
        
    }


    private Vector3 locationShift(){
        Vector3 powerPos = waypoints[Random.Range(0,waypoints.Length)].position;
        powerPos.x += 1.0f;
        return powerPos;
    }
}
