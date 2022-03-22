using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInteraction : MonoBehaviour
{
    //User Data Messages
    [Header("Messages")]
    public GameObject startSimulationMessage;

    [Header("StepsCylindres")]
    public GameObject startSimulation;
    public GameObject greenBall;


    [Header("InteractionUserElements")]
    public GameObject cardiogramButton;
    public GameObject transitionPanel;
    public AudioSource playSound;

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "StartSimulation":
                cardiogramButton.SetActive(true);
                Destroy(other.gameObject);
                Destroy(startSimulationMessage.gameObject);
                Destroy(greenBall.gameObject);
                Destroy(transitionPanel.gameObject);
                playSound.Play();
                break;
        }
    }
}
