using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressKey : MonoBehaviour
{

    [Header("Elements")]
    public GameObject pressMessage;

    private void OnTriggerEnter(Collider other)
    {
        pressMessage.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        pressMessage.SetActive(false);
    }
}
