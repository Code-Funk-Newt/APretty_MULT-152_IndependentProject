using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{


    public Camera playerCamera;   // player movement
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 4f;
    public float gravity = 15f;
    public float lookSpeed = 2f;
    private float lookXLimit = 45f;
    private float defaultHeight = 3f;


    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;







    public bool isCaught;               //GameOver freeze
    private bool canMove = true;        





    
    private bool hasPowerUp = false;   //Power up 
    public GameObject xRayVisor;
    [SerializeField] private UniversalRendererData renderComponent;


    public AudioClip testTubeSmash;
    public AudioClip heartPounding;
    public AudioClip exhaleCooldown;
    public AudioClip lockedOn;
    public AudioSource asPlayer;






    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
    
    if(!isCaught){    
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        //running is triggered by right mouse click//
        bool isRunning = Input.GetKey(KeyCode.Mouse1);

        
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);




        //jump //
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        //grounding player//
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }


        //basic movements//
        characterController.height = defaultHeight;
        walkSpeed = 6f;
        runSpeed = 12f;
        characterController.Move(moveDirection * Time.deltaTime);


        //mouse camera pov//
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    }





    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("powerUp")){
        hasPowerUp = true;
        Destroy(other.gameObject);
        xRayVisor.SetActive(true);
        StartCoroutine(powerUpCountDown());
        renderComponent.rendererFeatures[1].SetActive(hasPowerUp);
        asPlayer.PlayOneShot(testTubeSmash, 0.5f);
        asPlayer.clip = heartPounding;
        asPlayer.loop = true;
        asPlayer.Play();



    }
    }

    IEnumerator powerUpCountDown(){
        yield return new WaitForSeconds(8);
        hasPowerUp = false;
        xRayVisor.SetActive(false);
        renderComponent.rendererFeatures[1].SetActive(hasPowerUp);
        asPlayer.loop=false;
        asPlayer.PlayOneShot(exhaleCooldown, 0.9f);

    }
}

