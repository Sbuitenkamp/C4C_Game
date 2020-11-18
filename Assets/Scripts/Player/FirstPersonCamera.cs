using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float MouseSensitivity = 3f;
    public Transform PlayerBody;

    private float XRotation = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity;

        XRotation -= mouseY;
        XRotation = Mathf.Clamp(XRotation, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(XRotation, 0f, 0f);
        PlayerBody.Rotate(Vector3.up * mouseX);
    }
}
