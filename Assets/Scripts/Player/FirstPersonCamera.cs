using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonCamera : MonoBehaviour
{
    public float MouseSensitivity = 3f;
    public Transform PlayerBody;
    public float DistanceToSee = 3.0f;
    public Text Hand;
    
    private RaycastHit Hit;
    private bool Hovering = false;
    private bool HoverCheck = false;
    private float XRotation = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Hand.gameObject.SetActive(false);
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
        
        // check if hovering over something interactable
        Hovering = Physics.Raycast(transform.position,transform.forward, out Hit, DistanceToSee) && Hit.collider.gameObject.CompareTag("Interactable");

        if (Hovering) {
            // show hand
            if (!HoverCheck) Hand.gameObject.SetActive(true);
            HoverCheck = true;
            
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("e")) {
                Hit.collider.gameObject.GetComponent<Interactable>().Interact();
            }
        } else {
            Hand.gameObject.SetActive(false);
            HoverCheck = false;
        }
    }
}
