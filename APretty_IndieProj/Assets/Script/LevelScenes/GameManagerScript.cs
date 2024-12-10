using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class GameManagerScript : MonoBehaviour
{
    public GameObject player;
    public GameObject gameOverUI;
    public GameObject winScreen;            // screen UIs 
    public GameObject mainMenuUI;
    public GameObject instructionUI;


    public GameObject stalker;

    public float timeStart = 0;      //gametime start



    public TextMeshProUGUI livesWasted;     //number of retries 
    public TextMeshProUGUI livesWastedii;
    public TextMeshProUGUI timeCount;
    public TextMeshProUGUI timeCountii;



    public bool gameActive; 


    public AudioClip gameoveralarm;
    public AudioClip winscreensong;
    public AudioClip maingameambience;
    private AudioSource asPlayer;
    public static int numOfRetries; 


    // Start is called before the first frame update
    void Start()
    {


        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        asPlayer = GetComponent<AudioSource>();


        if(!anyScreenActive()){
        asPlayer.clip = maingameambience;
        asPlayer.Play();
        }

        //Debug.Log("Started. Number of Tries: " + numOfRetries);
    }

    // Update is called once per frame
    void Update()
    {   
        
        if(anyScreenActive()){
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        }
        else{
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        }
        


        
        if(!gameActive){
            pauseGame();
        }
        else{
            resumeGame();

            timeStart += Time.deltaTime;
            int seconds = ((int)timeStart % 60);                                //timer 
            int minutes = ((int) timeStart / 60);
            timeCount.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        
        
    }

    public void gameOver(){
        gameOverUI.SetActive(true); // trigger gameover screen
        player.GetComponent<PlayerController>().isCaught = true;   // stop player movement 

        asPlayer.clip = gameoveralarm;
        asPlayer.Play();

        livesWasted.text = "Total patients lost: " + numOfRetries;

        
    }


    public void mainMenu(){

        if(instructionUI.activeInHierarchy){
            instructionUI.SetActive(false);
        }
        if(winScreen.activeInHierarchy){
            SceneManager.LoadScene(0);
        }
        if(gameOverUI.activeInHierarchy){
            SceneManager.LoadScene(0);

        }

        
    
        //mainMenuUI.SetActive(true);
    

    }

    public void instructions(){
        instructionUI.SetActive(true);

    }

    public void win(){
        
        player.GetComponent<PlayerController>().isCaught = true;   // stop player movement 
        stalker.GetComponent<StalkerAI>().playerIsCaught = true;

        asPlayer.clip = winscreensong;
        asPlayer.Play();

        winScreen.SetActive(true);

        int seconds = ((int)timeStart % 60);                                //timer 
        int minutes = ((int) timeStart / 60);
        timeCountii.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        livesWastedii.text = ""+ numOfRetries;




    }

    public void startNewGame(){
        
        numOfRetries = 0;
        Debug.Log("startNewGame called. numOfRetries set to: " + numOfRetries);


        SceneManager.LoadScene(1);


    }

    public void restart(){

        numOfRetries += 1;
        Debug.Log("restart: "+numOfRetries);


        SceneManager.LoadScene(1);



    }




    public void quit(){
        
        Application.Quit();

    }


    public void pauseGame ()
    {
        Time.timeScale = 0;
    }

    public void resumeGame ()
    {
        Time.timeScale = 1;
    }


    public bool anyScreenActive(){        
         bool ifActive = false;

        if(gameOverUI.activeInHierarchy
        ||mainMenuUI.activeInHierarchy
        ||winScreen.activeInHierarchy
        ||instructionUI.activeInHierarchy){
            ifActive = true;            
        }
        else{
            ifActive = false;
        }

        return ifActive;   
    }
    

}
