using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogMananger : MonoBehaviour
{
    private Animator animator;
    private Queue<string> dialogQueue;
    public TMP_Text messageText;
    Text text;

    public void ActivateMessage(Text objectText)
    {
        animator.SetBool("Message", true);
        text = objectText;
    }

    public void ActiveText()
    {
        dialogQueue.Clear();
        foreach (string saveText in text.textArray)
        {
            dialogQueue.Enqueue(saveText);
        }
        NextMessage();
    }

    public void NextMessage()
    {
        if(dialogQueue.Count == 0)
        {
            CloseDialog();
            return;
        }

        string currentMessage = dialogQueue.Dequeue();
        messageText.text = currentMessage;

    }

    private void CloseDialog()
    {
        animator.SetBool("Message", false);
    }
}
