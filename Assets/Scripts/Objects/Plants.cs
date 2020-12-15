using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using Image = UnityEngine.UI.Image;

public class Plants : MonoBehaviour, Interactable
{
    private Camera Cam;
    private List<Plant> PlantsToWater;
    private GameObject CamPosition;
    private GameObject TargetPosition;
    private GameObject WateringCan;
    private GameObject UI;
    private Vector3 WateringCanPosition;
    private Quaternion WateringCanRotation;
    private PlayerMovement PlayerController;
    private FirstPersonCamera PlayerCamera;
    private List<Plant> Watered = new List<Plant>();
    private AudioSource AudioPlayer;
    private bool Interacting = false;
    private bool Focussing = false;
    private bool UnFocussing = false;
    private bool Watering = false;
    private float Speed = 3f;
    private float LerpTime = 1f;
    private float CurrentLerpingTime;

    public void Start()
    {
        WateringCan = GameObject.Find("WateringCan");
        AudioPlayer = WateringCan.GetComponent<AudioSource>();
        
        WateringCanPosition = WateringCan.transform.position;
        WateringCanRotation = WateringCan.transform.rotation;
        
        UI = GameObject.Find("UserInterface");
        PlantsToWater = new List<Plant>(gameObject.GetComponentsInChildren<Plant>());
        PlantsToWater.Sort((a, b) => a.PlaceInOrder.CompareTo(b.PlaceInOrder));
    }

    public void Update()
    {
        if (!Interacting) return;
        
        if (Input.GetMouseButtonDown(0) && !Watering) {
            Watering = true;
            Ray ray = Cam.ScreenPointToRay(Input.mousePosition);

            // if the ray hits
            if (Physics.Raycast(ray, out RaycastHit hit, 100)) {
                Plant plant = hit.collider.GetComponent<Plant>();
                if (plant == null) return;
                plant.gameObject.GetComponent<MeshCollider>().enabled = false;
                Watered.Add(plant);
                Water(plant);
            }
        }

        if (Input.GetButtonDown("Cancel")) ResetCamera();
    }

    public void FixedUpdate()
    {
        // move cam in front of task.
        if (Focussing) {
            Transform camTransform = Cam.transform;
            camTransform.position = Vector3.Lerp(camTransform.position, TargetPosition.transform.position, Speed * Time.deltaTime);
            camTransform.rotation = Quaternion.Lerp(camTransform.rotation, TargetPosition.transform.rotation, Speed * Time.deltaTime);
            
            if (camTransform.position == TargetPosition.transform.position && camTransform.rotation == TargetPosition.transform.rotation) Focussing = false;
        } else if (UnFocussing) {
            CurrentLerpingTime = Mathf.Clamp01(CurrentLerpingTime + Speed * Time.deltaTime);
            float t = CurrentLerpingTime / LerpTime;
            
            Transform camTransform = Cam.transform;
            camTransform.position = Vector3.Lerp(camTransform.position, CamPosition.transform.position, t);
            camTransform.rotation = Quaternion.Lerp(camTransform.rotation, CamPosition.transform.rotation, t);

            if (!(t >= 1)) return;
            UnFocussing = false;
            Destroy(CamPosition);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Speech playerVoice = other.gameObject.GetComponent<Speech>();
        playerVoice.Talk("oja, ik moet de planten nog water geven.", null);
    }

    public void Interact(PlayerMovement controller, FirstPersonCamera playerCamera)
    {
        if (TargetPosition == null) {
            TargetPosition = new GameObject();
            TargetPosition.transform.position = new Vector3(-13.3f, 25.6f, -39.1f);
            TargetPosition.transform.rotation = Quaternion.Euler(15f, -90f, 0);
        }
        
        PlayerController = controller;
        PlayerCamera = playerCamera;
        Cam = PlayerController.gameObject.GetComponentInChildren<Camera>();

        if (CamPosition == null) {
            CamPosition = new GameObject();
            CamPosition.transform.position = new Vector3(Cam.transform.position.x, Cam.transform.position.y, Cam.transform.position.z);
            CamPosition.transform.rotation = new Quaternion(Cam.transform.rotation.x, Cam.transform.rotation.y, Cam.transform.rotation.z, 0f);
        }

        // disable player movement
        PlayerController.Controlling = false;
        PlayerCamera.Looking = false;
        
        // Disable UI
        PlayerCamera.Hand.gameObject.SetActive(false);
        UI.GetComponentInChildren<Image>().enabled = false;
        
        // Disable camera movement
        Cursor.lockState = CursorLockMode.Confined;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        Focussing = true;
        Interacting = true;
    }

    private void CheckOrder()
    {
        if (PlantsToWater.SequenceEqual(Watered)) {
            gameObject.tag = "Untagged";
            Interacting = false;
            Destroy(gameObject.GetComponent<CapsuleCollider>());
            ResetCamera();
        } else {
            // reset the plants
            foreach (Plant wateredPlant in Watered) wateredPlant.gameObject.GetComponent<MeshCollider>().enabled = true;
            Watered = new List<Plant>();
        }
    }

    private void ResetCamera()
    {
        Focussing = false;
        UnFocussing = true;
        UI.GetComponentInChildren<Image>().enabled = true;
        PlayerController.Controlling = true;
        PlayerCamera.Looking = true;
        UI.GetComponentInChildren<Image>().enabled = true;
        gameObject.GetComponent<BoxCollider>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Destroy(TargetPosition);
    }

    private void Water(Plant plant)
    {
        Vector3 plantPosition = plant.transform.position;
        Vector3 abovePlant = new Vector3(plantPosition.x, plantPosition.y + 0.5f, plantPosition.z - 0.3f);
        
        WateringCan.transform.position = abovePlant;
        WateringCan.transform.Rotate(0, 0, -45f);
        WateringCan.transform.Rotate(-50f, 0, 0);
        
        AudioPlayer.Play();
        
        StartCoroutine(AddWater());
//        Quaternion wateringCanRotation = WateringCan.transform.rotation;
//        Quaternion targetRotation = new Quaternion(wateringCanRotation.x - 0.5f, wateringCanRotation.y + 0.5f, wateringCanRotation.z, wateringCanRotation.w);

//        WateringCan.transform.rotation = Quaternion.Lerp(wateringCanRotation, targetRotation, 2f);
    }

    private IEnumerator AddWater()
    {
        yield return new WaitForSeconds(1);
        WateringCan.transform.position = WateringCanPosition;
        WateringCan.transform.rotation = WateringCanRotation;
        
        AudioPlayer.Stop();
        
        Watering = false;
        if (Watered.Count > 2) CheckOrder();
    }
}