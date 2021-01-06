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
    public bool Looking;
    public bool Hovering;

    private RaycastHit _hit;
    private PlayerMovement Controller { get; set; }
    private Vector3 SittingDownPosition { get; set; }
    private Quaternion SittingDownRotation { get; set; }
    private Vector3 StandingUpPosition { get; set; }
    private Quaternion StandingUpRotation { get; set; }
    private Speech PlayerVoice { get; set; }
    private float CurrentLerpingTime { get; set; }
    private float XRotation { get; set; }
    private bool HoverCheck { get; set; }
    private bool StandingUp { get; set; }
    private bool SittingDown { get; set; }

    public void Start()
    {
        SittingDownPosition = new Vector3(-11.6f, 25.1f, -44.4f);
        SittingDownRotation = new Quaternion(0, -0.7f, 0, -0.7f);
        XRotation = 0f;
        HoverCheck = false;
        StandingUp = false;
        SittingDown = true;
        
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
        Hovering = Physics.Raycast(transform.position,transform.forward, out _hit, DistanceToSee) && _hit.collider.gameObject.CompareTag("Interactable");

        if (Hovering) {
            // show hand
            if (!HoverCheck) Hand.gameObject.SetActive(true);
            HoverCheck = true;
            
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown("space") || Input.GetKeyDown("e")) _hit.collider.gameObject.GetComponent<Interactable>().Interact(Controller, this);
        } else {
            Hand.gameObject.SetActive(false);
            HoverCheck = false;
        }
    }
}
