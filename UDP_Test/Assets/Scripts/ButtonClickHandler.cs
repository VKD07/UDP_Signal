using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickHandler : MonoBehaviour
{
    private void Start()
    {
        // Find all child buttons of the parent object
        Button[] childButtons = GetComponentsInChildren<Button>();

        // Iterate through the child buttons
        foreach (Button button in childButtons)
        {
            // Add a click event listener to each button
            button.onClick.AddListener(() => OnButtonClick(button));
        }
    }
    public void OnButtonClick(Button clickedButton)
    {
        string messageKey = clickedButton.gameObject.name; // Assuming button names correspond to message keys
        Debug.Log(messageKey);
        // Send the message with the assigned key
        UDPSend.GetInstance().SendUDPMsg(messageKey);
    }
}

