using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public float mouseSensitive = 100f;
    public Transform playerBody;
    private float xRotation = 0f;
    public bool cameraPause = false;


    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitive * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitive * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            cameraPause = !cameraPause;
            if (cameraPause)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

            }
            else
            {
                transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
                playerBody.Rotate(Vector3.up * mouseX);
                Cursor.visible = false;
            }
        }
        if (!cameraPause)
        {
            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            playerBody.Rotate(Vector3.up * mouseX);
            Cursor.visible = false;
        }
    }
}
