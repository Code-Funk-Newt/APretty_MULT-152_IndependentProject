using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class winScript : MonoBehaviour
{
    public GameObject _gameManager;
    public GameObject _Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            _gameManager.GetComponent<GameManagerScript>().win();


        }
    }
}
