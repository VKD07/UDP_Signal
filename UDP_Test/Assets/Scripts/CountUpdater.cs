using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountUpdater : MonoBehaviour
{
    [SerializeField] UDPReceive udpReceiver;
    [SerializeField] Text[] textsToUpdate; // Assign your Text components to this array

    [SerializeField] GameObject[] panelsToActivate;
    // Store the currently active panel index
    [SerializeField] int currentActivePanelIndex = -1;
    // The duration (in seconds) for the incremental increase
    [SerializeField] float incrementDuration;
    [SerializeField] int maxCountValue; // Maximum value to reset to 0

    private void OnEnable()
    {
        udpReceiver.receiveUDP += OnReceiveUDP;
    }

    private void OnDisable()
    {
        udpReceiver.receiveUDP -= OnReceiveUDP;
    }

    private void Start()
    {
        StartCoroutine(IncrementTextValues());
    }
    private void OnReceiveUDP(string message)
    {
        // Check if the received message matches any of the assigned keys
        for (int i = 0; i < textsToUpdate.Length; i++)
        {
            if (message == textsToUpdate[i].name)
            {
                // Check if this panel is already active, if so, deactivate it
                if (i == currentActivePanelIndex)
                {
                    panelsToActivate[i].SetActive(false);
                    currentActivePanelIndex = -1;
                }
                else
                {
                    // Deactivate the current active panel (if any)
                    if (currentActivePanelIndex != -1)
                    {
                        panelsToActivate[currentActivePanelIndex].SetActive(false);
                    }

                    // Update the corresponding text field immediately
                    int currentValue = int.Parse(textsToUpdate[i].text);
                    currentValue++;
                    textsToUpdate[i].text = currentValue.ToString();

                    // Activate the corresponding panel
                    panelsToActivate[i].SetActive(true);
                    currentActivePanelIndex = i;
                }
                break; // Exit the loop once the update is done
            }
        }
    }
    private IEnumerator IncrementTextValues()
    {
        while (true)
        {
            for (int i = 0; i < textsToUpdate.Length; i++)
            {
                int currentValue = int.Parse(textsToUpdate[i].text);
                currentValue++;
                if (currentValue > maxCountValue)
                {
                    currentValue = 0; // Reset to 0 when reaching the maximum
                }
                textsToUpdate[i].text = currentValue.ToString();
            }

            yield return new WaitForSeconds(incrementDuration);
        }
    }
}
