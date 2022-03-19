using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public Text texts;

    public void OnMouseDown()
    {
        FindObjectOfType<DialogMananger>().ActivateMessage(texts);
    }
}
