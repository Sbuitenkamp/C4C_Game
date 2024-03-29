﻿using UnityEngine;
using UnityEngine.UI;

public class FirstPersonCamera : MonoBehaviour
{
    public Text Hand;
    public Transform PlayerBody;
    public float MouseSensitivity = 3f;
    public float DistanceToSee = 1f;
    public bool Looking;
    public bool Hovering;

    private RaycastHit Hit;
    private PlayerMovement Controller;
    private Vector3 SittingDownPosition = new Vector3(-11.6f, 25.1f, -44.4f);
    private Quaternion SittingDownRotation = new Quaternion(0, -0.7f, 0, -0.7f);
    private Vector3 StandingUpPosition;
    private Quaternion StandingUpRotation;
    private Speech PlayerVoice;
    private float CurrentLerpingTime;
    private float XRotation = 0f;
    private bool HoverCheck = false;
    private bool StandingUp = false;
    private bool SittingDown = true;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Hand.gameObject.SetActive(false);
        Looking = false;
        PlayerVoice = gameObject.GetComponentInParent<Speech>();

        Controller = gameObject.GetComponentInParent<PlayerMovement>();
        Controller.Controlling = false;

        StandingUpPosition = gameObject.transform.position;
        StandingUpRotation = gameObject.transform.rotation;

        gameObject.transform.position = SittingDownPosition;
        gameObject.transform.rotation = SittingDownRotation;

        PlayerVoice.Talk("er is tegenwoordig niks leuks meer op tv, laat ik hem uitzetten.", Resources.Load<AudioClip>("Audio/VoiceLines/Tv_Off"));
    }

    public void Update()
    {
        if (!PlayerVoice.Voice.isPlaying && SittingDown) StandingUp = true;
        // stand up at the beginning of the game.
        if (StandingUp) {
            float speed = .1f;
            float lerpTime = 4f;
            CurrentLerpingTime = Mathf.Clamp01(CurrentLerpingTime + speed * Time.deltaTime);
            
            float t = CurrentLerpingTime / lerpTime;
            
            gameObject.transform.position = Vector3.Slerp(gameObject.transform.position, StandingUpPosition, t);
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, StandingUpRotation, t);

            if (t >= .03f) {
                StandingUp = false;
                SittingDown = false;
                Looking = true;
                Controller.Controlling = true;
                PlayerVoice.Talk("Oef, vroeger ging opstaan toch een stuk gemakkelijker.", Resources.Load<AudioClip>("Audio/VoiceLines/Oef_Chair"));
            }
        }
        
        // lock cursor if doing activity
        if (!Looking) return;
        
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity;

        XRotation -= mouseY;
        XRotation = Mathf.Clamp(XRotation, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(XRotation, 0f, 0f);
        PlayerBody.Rotate(Vector3.up * mouseX);
        
        // check if hovering over something interactable
//        Hovering = Physics.Raycast(transform.position,transform.forward, out Hit, DistanceToSee) && Hit.collider.gameObject.CompareTag("Interactable");
        RaycastHit hit;
        Ray ray = gameObject.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.gameObject.CompareTag("Interactable")) {
                if (hit.distance > DistanceToSee) return;
                if (!HoverCheck) Hand.gameObject.SetActive(true);
                HoverCheck = true;
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown("space") || Input.GetKeyDown("e")) hit.collider.gameObject.GetComponent<Interactable>().Interact(Controller, this);
            } else {
                Hand.gameObject.SetActive(false);
                HoverCheck = false;
            }
        }

//        if (Hovering) {
//            // show hand
//            if (!HoverCheck) Hand.gameObject.SetActive(true);
//            HoverCheck = true;
//            
//            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown("space") || Input.GetKeyDown("e")) Hit.collider.gameObject.GetComponent<Interactable>().Interact(Controller, this);
//        } else {
//            Hand.gameObject.SetActive(false);
//            HoverCheck = false;
//        }
    }
}
