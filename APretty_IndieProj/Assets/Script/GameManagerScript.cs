using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject player;
    public GameObject gameOverUI;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        if(gameOverUI.activeInHierarchy){
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        }
        else{
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void gameOver(){
        gameOverUI.SetActive(true); // trigger gameover screen
        player.GetComponent<PlayerController>().isCaught = true;   // stop player movement 
        
    }
    public void restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("restart");
    }


    public void quit(){
        
        Application.Quit();

    }

}
