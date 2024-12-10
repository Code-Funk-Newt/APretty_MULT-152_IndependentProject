using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiButtonScript : MonoBehaviour
{
    public GameObject camerasecuritySystem;
    public Material greenBtnColor;
    public AudioClip btnClick;
    public AudioClip SystemOn;
    public AudioSource asPlayer;
    public List<GameObject> buttons; // List of all button GameObjects

    private int buttonsClicked = 0;

    private void Start()
    {
        foreach (GameObject button in buttons)
        {
            button.GetComponent<ButtonScript>().OnButtonClicked += ButtonClicked;
        }
    }

    private void ButtonClicked(GameObject button)
    {
        if(button.GetComponent<ButtonScript>().clickedOn == false ){        // perameter so we can click twice on button
        asPlayer.PlayOneShot(btnClick, 0.7f);
        button.GetComponent<Renderer>().material = greenBtnColor;
        buttonsClicked++;
        button.GetComponent<ButtonScript>().clickedOn = true;

         //if (buttonsClicked == buttons.Count)  // <---original buttonscript

        if (buttonsClicked >= 3)                // the amounted needed to unlock a system
        {   
            Debug.Log("3buttons clicked");
            asPlayer.PlayOneShot(SystemOn, 5f);
            camerasecuritySystem.GetComponent<CameraConsole>().puzzleSolved = true;
        }

        }

 
    }
}