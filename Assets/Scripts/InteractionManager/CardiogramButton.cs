using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardiogramButton : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] public Animator vitalSignsAnimation;
    [SerializeField] public Animator historyAnimation;
    [SerializeField] private string vitalSignsName= "VitalSignsAnimation";
    [SerializeField] private string historyStartName= "HistoryStart";
    [SerializeField] private string historyEndName= "HistoryEnd";


    //User Data Messages
    [Header("Messages")]
    public GameObject vitalSignsMessage;
    public GameObject historyMessage;
    public GameObject historyArrow;

    [Header("Interactable objects")]
    public GameObject cardiogramButton;
    public GameObject answers;
    public GameObject cardiogramImage;
    public GameObject redCodeMessage;
    private bool isActive = true;
    private bool isHistoryActive = true;

    [Header("Sounds")]
    public AudioSource playSound;

    public void CheckCardiogram()
    {
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
            redCodeMessage.SetActive(true);
        }
    }

    public void CheckHistory()
    {
        historyMessage.SetActive(false);
        historyArrow.SetActive(false);
        if (isHistoryActive)
        {
            historyAnimation.Play(historyStartName, 0, 0.0f);
            isHistoryActive = !isHistoryActive;
        }
        else
        {
            historyAnimation.Play(historyEndName, 0, 0.0f);
            isHistoryActive = !isHistoryActive;
        }
    }
}
