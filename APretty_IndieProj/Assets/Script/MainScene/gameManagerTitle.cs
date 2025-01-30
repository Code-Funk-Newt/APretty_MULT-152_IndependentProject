using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class gameManagerTitle : MonoBehaviour
{
    static bool credits_IsDeleted = false;

    public GameObject mainMenuUI;
    public GameObject instructionUI;
    public GameObject creditScreen;

    public AudioSource asPlayer;



    // Start is called before the first frame update
    void Start()
    {
        
        asPlayer.Play();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if(credits_IsDeleted){
            Destroy(creditScreen);
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;


        if(Input.GetKey(KeyCode.Space)){
            Destroy(creditScreen);
            credits_IsDeleted = true;
        }


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
