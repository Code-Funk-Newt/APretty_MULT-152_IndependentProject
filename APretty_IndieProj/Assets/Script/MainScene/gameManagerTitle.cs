using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class gameManagerTitle : MonoBehaviour
{

    public GameObject mainMenuUI;
    public GameObject instructionUI;
    public GameObject otherGameManager;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void mainMenu(){

        if(instructionUI.activeInHierarchy){
            instructionUI.SetActive(false);
        }

        mainMenuUI.SetActive(true);
    }

    public void instructions(){
        instructionUI.SetActive(true);

    }

    public void startNewGame(){

        GameManagerScript.numOfRetries = 1;   //Reset the number of tries from other script 
        SceneManager.LoadScene(1);

    }

    public void quit(){
        
        Application.Quit();

    }

}
