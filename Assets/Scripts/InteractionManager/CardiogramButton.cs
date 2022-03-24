using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardiogramButton : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] public Animator vitalSignsAnimation;
    [SerializeField] private string vitalSignsName= "VitalSignsAnimation";
    public GameObject cardiogramImage;
    //User Data Messages
    [Header("Messages")]
    public GameObject vitalSignsMessage;

    [Header("Interactable objects")]
    public GameObject cardiogramButton;
    private bool isActive = true;
    public GameObject answers;

    [Header("Sounds")]
    public AudioSource playSound;

    public void SendMessage()
    {
        Debug.Log("Click!");
        vitalSignsMessage.SetActive(false);
        playSound.Stop();
        if (isActive)
        {
            cardiogramImage.SetActive(true);
            vitalSignsAnimation.Play(vitalSignsName, 0, 0.0f);
            isActive = false;

        }
        else
        {
            isActive = true;
            cardiogramImage.SetActive(false);
            answers.SetActive(true);
        }
    }
}
