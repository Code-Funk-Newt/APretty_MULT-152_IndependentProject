using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textInstruction : MonoBehaviour
{

    public bool playerInTrigger;


    private Animator textAnimator;


    // Start is called before the first frame update
    void Start()
    {
        playerInTrigger = false;
        textAnimator = gameObject.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInTrigger){
            textAnimator.SetBool("textAppear",true);
        }
        else{
            textAnimator.SetBool("textAppear", false);
        }
        
    }
}
