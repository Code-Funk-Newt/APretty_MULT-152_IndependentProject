using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PickUpScript : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;
    public Transform rotatePos;

    public float pickUpRange = 10f; //how far the player can pickup the object from
    private GameObject heldObj; //object which we pick up
    public GameObject raycastOrigin; // the raycast origin
    private Vector3 heldObjRotOffset;
    private Rigidbody heldObjRb; //rigidbody of object we pick up
    private bool canDrop = true; //this is needed so we don't throw/drop object when rotating the object
    private int LayerNumber; //layer index

    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("holdLayer"); //if your holdLayer is named differently make sure to change this ""
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))    // E to pick up
        {
            if (heldObj == null) //if currently not holding anything
            {   
                //perform raycast to check if player is looking at object within pickuprange
                RaycastHit hit;
                if (Physics.Raycast(raycastOrigin.transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    // Debug.Log("hitting raycast");
                    Debug.Log(hit.transform.gameObject.tag);

                    //make sure pickup tag is attached
                    if (hit.transform.gameObject.tag == "canPickUp")
                    {
                        Debug.Log("ray hit canPickUp tag");
                        //pass in object hit into the PickUpObject function
                        PickUpObject(hit.transform.gameObject);
                    }
                }
            }
        }

        //  vvvvv DROP THE CUBE vvvvv
        if (Input.GetKeyDown(KeyCode.F))   //F to drop
        {
            if (canDrop == true)
            {
                StopClipping(); //prevents object from clipping through walls
                DropObject();
            }
        }

        if (heldObj != null) //if player is holding object
        {
            MoveObject(); //keep object position at holdPos
            RotateObject();
        }
    }

    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>()) //make sure the object has a RigidBody
        {
            heldObj = pickUpObj; //assign heldObj to the object that was hit by the raycast (no longer == null)
            heldObjRb = pickUpObj.GetComponent<Rigidbody>(); //assign Rigidbody
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPos.transform; //parent object to holdposition

            Vector3 playerEulerAngles = player.transform.eulerAngles;
            heldObjRb.transform.rotation = Quaternion.Euler(0 , playerEulerAngles.y, 0); //parallel rotation to the camera
             

            heldObj.layer = LayerNumber; //change the object layer to the holdLayer
            //make sure object doesnt collide with player, it can cause weird bugs
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }

    void DropObject()
    {
        Debug.Log("DROPPED");
        //re-enable collision with player
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0; //object assigned back to default layer
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null; //unparent object
        heldObj = null; //undefine game object
        heldObjRb = null; //undefine Rigidbody
    }

    void MoveObject()
    {
        //keep object position the same as the holdPosition position
        heldObj.transform.position = holdPos.transform.position;
    }

    void RotateObject()
    {
        if (Input.GetKey(KeyCode.R)) //hold R key to rotate, change this to whatever key you want
        {

            canDrop = false; //make sure throwing can't occur during rotating

            

            // Smoothly transition the object's position to Vector3(0,0,0) relative to its parent
            StartCoroutine(SmoothTransition(heldObj.transform, rotatePos.transform.position));

            // always forces the object to face the camera rotation
            heldObj.transform.localRotation = rotatePos.transform.localRotation * Quaternion.Euler(heldObjRotOffset);

            // New mouse functions for free movement within the space of the object
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            //heldObj.transform.Rotate(Vector3.up, -mouseX, Space.World);
            //heldObj.transform.Rotate(Vector3.right, mouseY, Space.World);     // rotate to the mouse axises



            // Rotate 90 degrees on the y-axis when Q is pressed
            if (Input.GetKeyDown(KeyCode.Q))
            {
                heldObjRotOffset += Vector3.up * 90f;
                heldObjRotOffset = new Vector3(heldObjRotOffset.x, heldObjRotOffset.y % 360, heldObjRotOffset.z);
            }

        }
        else
        {
            canDrop = true;
        }
    }

    IEnumerator SmoothTransition(Transform obj, Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        float duration = 0.2f; // duration of the transition
        Vector3 startingPos = obj.position;

        while (elapsedTime < duration)
        {
            obj.position = Vector3.Lerp(startingPos, targetPosition, (elapsedTime / duration));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.position = targetPosition;
    }

    void StopClipping() //function only called when dropping/throwing
    {
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position); //distance from holdPos to the camera
        //have to use RaycastAll as object blocks raycast in center screen
        //RaycastAll returns array of all colliders hit within the cliprange
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        //if the array length is greater than 1, meaning it has hit more than just the object we are carrying
        if (hits.Length > 1)
        {
            //change object position to camera position 
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f); //offset slightly downward to stop object dropping above player 
            //if your player is small, change the -0.5f to a smaller number (in magnitude) ie: -0.1f
        }
    }

    private IEnumerator MoveCube(Vector3 targetPosition)
    {
        while (transform.position != targetPosition)
        {
            heldObjRb.transform.position = Vector3.MoveTowards(heldObj.transform.position, targetPosition, 20 * Time.deltaTime);
            yield return null;
        }
    }
}