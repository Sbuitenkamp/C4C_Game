using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StickyNote : MonoBehaviour, Interactable
{
    public string StickyTextContent;

    private Image Sticky { get; set; }
    private Text StickyText { get; set; }
    private Button CloseButton { get; set; }
    private PlayerMovement PlayerController { get; set; }
    private FirstPersonCamera PlayerCamera { get; set; }
    private AudioSource AudioPlayer { get; set; }

    public void Start()
    {
        AudioPlayer = gameObject.GetComponentInChildren<AudioSource>();
        
        GameObject UI = GameObject.Find("UserInterface");
        Sticky = UI.GetComponentsInChildren<Image>(true).ToList().Find(x => x.name == "StickyNoteImage");
        StickyText = UI.GetComponentsInChildren<Text>(true).ToList().Find(x => x.name == "StickyText");
        CloseButton = UI.GetComponentsInChildren<Button>(true).ToList().Find(x => x.name == "Close");
        CloseButton.onClick.AddListener(Close);
    }
    
    public void Interact(PlayerMovement controller, FirstPersonCamera playerCamera)
    {
        AudioPlayer.Play();
        PlayerController = controller;
        PlayerCamera = playerCamera;

        Cursor.lockState = CursorLockMode.Confined;

        PlayerController.Controlling = false;
        PlayerCamera.Looking = false;
        Sticky.gameObject.SetActive(true);
        StickyText.gameObject.SetActive(true);
        CloseButton.gameObject.SetActive(true);
        
        // allow usage of \n in the fields to make newlines easier to implement
        StickyText.text = StickyTextContent.Replace("\\n","\n");
        
        PlayerController.gameObject.GetComponent<Speech>().Talk("hmm", Resources.Load<AudioClip>("Audio/VoiceLines/Hmm"));
    }

    private void Close()
    {
        Cursor.lockState = CursorLockMode.Locked;

        PlayerController.Controlling = true;
        PlayerCamera.Looking = true;
        Sticky.gameObject.SetActive(false);
        StickyText.gameObject.SetActive(false);
        CloseButton.gameObject.SetActive(false);
        StickyText.text = null;
    }
}
