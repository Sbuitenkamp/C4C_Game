using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;

public class FirstPersonCamera : MonoBehaviour
{
    public Text Hand;
    public Transform PlayerBody;
    public float MouseSensitivity = 3f;
    public float DistanceToSee = 3.0f;
    public bool Looking = true;
    public bool Hovering = false;

    private RaycastHit Hit;
    private PlayerMovement Controller;
    private float XRotation = 0f;
    private bool HoverCheck = false;

    // Start is called before the first frame update
    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Hand.gameObject.SetActive(false);
        Controller = gameObject.GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    public void Update()
    {
        // lock cursor if doing activity
        if (!Looking) return;
        
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
            
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown("space") || Input.GetKeyDown("e")) Hit.collider.gameObject.GetComponent<Interactable>().Interact(Controller, this);
        } else {
            Hand.gameObject.SetActive(false);
            HoverCheck = false;
        }
    }
}
