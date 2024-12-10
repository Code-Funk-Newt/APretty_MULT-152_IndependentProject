using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public delegate void ButtonClickedHandler(GameObject button);
    public event ButtonClickedHandler OnButtonClicked;
    public bool clickedOn;

    private void start(){
        clickedOn = true;
    }

    private void OnMouseUpAsButton()
    {
        OnButtonClicked?.Invoke(gameObject);
    }
}
