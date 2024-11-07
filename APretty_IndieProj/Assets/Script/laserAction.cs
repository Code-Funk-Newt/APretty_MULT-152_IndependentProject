using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserAction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Handle player losing the game
            Debug.Log("Player caught by the stalker! Game Over.");
        }
    }

}
