using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWall : MonoBehaviour
{
    public bool laserWallOn;
    public GameObject laserWall1;
    public GameObject laserWall2;
    public GameObject laserWall3;


    public AudioSource asPlayer;
    public AudioClip laserHum;


    // Start is called before the first frame update
    void Start()
    {
        laserWallOn = true;
        laserWall1.SetActive(true);
        laserWall2.SetActive(true);
        asPlayer.loop = true;
        asPlayer.Play();
        

        
    }

    // Update is called once per frame
    void Update()
    {




    }

    public void turnOffLaserWall(){


            laserWall1.SetActive(false);
            laserWall2.SetActive(false);
            laserWall3.SetActive(false);

            laserWallOn = false;
            asPlayer.Stop();
    }
    public void turnOnLaserWall(){
            

            laserWallOn = true;
            asPlayer.loop = true;
            asPlayer.Play();

            laserWall1.SetActive(true);
            laserWall2.SetActive(true);
            laserWall3.SetActive(true);
    }



}
