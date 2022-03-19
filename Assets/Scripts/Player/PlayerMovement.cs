using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController characterController;
    private float characterSpeed = 3f;

    //User Data Messages
    [Header("Messages")]
    public GameObject firstMessage;
    public GameObject secondMessage;
    public GameObject thirdMessage;
    public GameObject fourthMessage;
    public GameObject fifthMessage;

    [Header("StepsCylindres")]
    public Queue<GameObject> steps;
    public GameObject firstStep;
    public GameObject secondStep;
    public GameObject thirdStep;
    public GameObject spaceBar;
    public GameObject escKey;

    private void Start()
    {
        
    }

    private void Update(){
        // Get A, D keyboards and mouse. 
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * x + transform.forward * z;
        characterController.Move(movement * characterSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other){
        switch (other.tag)
        {
            case "Item":
                Debug.Log("Item");
                Destroy(other.gameObject);
                Destroy(firstMessage.gameObject);
                Destroy(secondMessage.gameObject);
                //firstMessage.SetActive(false);
                //secondMessage.SetActive(false);
                thirdMessage.SetActive(true);
                secondStep.SetActive(true);
                break;
            case "SecondStep":
                Debug.Log("SecondStep");
                Destroy(other.gameObject);
                Destroy(thirdMessage.gameObject);
                spaceBar.SetActive(true);
                fourthMessage.SetActive(true);
                thirdStep.SetActive(true);
                break;
            case "ThirdStep":
                Debug.Log("ThirdStep");
                Destroy(other.gameObject);
                Destroy(fourthMessage.gameObject);
                Destroy(spaceBar.gameObject);
                fifthMessage.SetActive(true);
                escKey.SetActive(true);
                break;
        }
    }
}
