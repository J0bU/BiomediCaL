using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUserInteraction : MonoBehaviour
{
    //User Data Messages
    [Header("Messages")]
    public GameObject firstMessage;
    public GameObject secondMessage;
    public GameObject perfectMessage;
    public GameObject thirdMessage;
    public GameObject fourthMessage;
    public GameObject fifthMessage;

    [Header("StepsCylindres")]
    public GameObject firstStep;
    public GameObject secondStep;
    public GameObject thirdStep;
    public GameObject spaceBar;
    public GameObject escKey;


    [Header("Hearts")]
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject finalHeart;
    public GameObject lastStep;


    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Item":
                Debug.Log("Item");
                Destroy(other.gameObject);
                Destroy(firstMessage.gameObject);
                Destroy(secondMessage.gameObject);
                Destroy(heart1.gameObject);
                heart2.SetActive(true);
                thirdMessage.SetActive(true);
                secondStep.SetActive(true);
                StartCoroutine(LoadPerfectMessage());
                break;
            case "SecondStep":
                Debug.Log("SecondStep");
                Destroy(other.gameObject);
                Destroy(thirdMessage.gameObject);
                Destroy(heart2.gameObject);
                heart3.SetActive(true);
                spaceBar.SetActive(true);
                fourthMessage.SetActive(true);
                thirdStep.SetActive(true);
                break;
            case "ThirdStep":
                Debug.Log("ThirdStep");
                Destroy(other.gameObject);
                Destroy(fourthMessage.gameObject);
                Destroy(spaceBar.gameObject);
                Destroy(heart3.gameObject);
                fifthMessage.SetActive(true);
                escKey.SetActive(true);
                finalHeart.SetActive(true);
                lastStep.SetActive(true);
                break;
        }
    }

    IEnumerator LoadPerfectMessage()
    {
        perfectMessage.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        perfectMessage.SetActive(false);
    }
}
