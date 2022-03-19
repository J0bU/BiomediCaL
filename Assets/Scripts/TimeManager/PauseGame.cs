using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public bool gamePaused = false;
    public GameObject pauseMenu;
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if(gamePaused == false)
            {
                Time.timeScale = 0;
                gamePaused = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                pauseMenu.SetActive(true);
            }
            else
            {
                pauseMenu.SetActive(false);
                Cursor.visible = false;
                gamePaused = false;
                Time.timeScale = 1;
            }
        }
    }
}
