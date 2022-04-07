using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PressKey : MonoBehaviour
{

    [Header("Elements")]
    public GameObject rightAnswer;
    public GameObject wrongAnswer;
    public GameObject nurseCharacter;
    public GameObject redCodeMessage;
    public GameObject initialRedCodeMessage;
    public GameObject fixAnswerElement;
    public GameObject fixAnswerTrigger;
    public GameObject historyMessage;
    public GameObject historyArrow;

    [Header("Figures")]
    public GameObject greenObject;
    public GameObject redObject;
    public GameObject greenObjectTrigger;
    public GameObject redObjectTrigger;

    //[Header("PointsSystem")]
    [SerializeField] private int pointsNumber = 1;
    [SerializeField] private PointsSystem pointsSystem;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            pointsSystem.MorePoints(pointsNumber);
            nurseCharacter.SetActive(true);
            historyMessage.SetActive(true);
            historyArrow.SetActive(true);
            DisableAnswers();
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            DisableAnswers();
            fixAnswerElement.SetActive(true);
            fixAnswerTrigger.SetActive(true);
            redCodeMessage.SetActive(true);
        }
    }

    void DisableAnswers()
    {
        Destroy(greenObject.gameObject);
        Destroy(redObject.gameObject);
        Destroy(greenObjectTrigger.gameObject);
        Destroy(redObjectTrigger.gameObject);
        Destroy(initialRedCodeMessage.gameObject);
        Destroy(rightAnswer.gameObject);
        Destroy(wrongAnswer.gameObject);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "RightAnswer":
                rightAnswer.SetActive(true);
                break;
            case "WrongAnswer":
                wrongAnswer.SetActive(true);
                break;
            case "FixAnswer":
                historyMessage.SetActive(true);
                historyArrow.SetActive(true);
                nurseCharacter.SetActive(true);
                Destroy(fixAnswerElement.gameObject);
                Destroy(fixAnswerTrigger.gameObject);
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "RightAnswer":
                rightAnswer.SetActive(false);
                break;
            case "WrongAnswer":
                wrongAnswer.SetActive(false);
                break;
        }
    }
}
