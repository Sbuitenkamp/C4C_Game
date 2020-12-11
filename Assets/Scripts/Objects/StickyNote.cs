using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StickyNote : MonoBehaviour, Interactable
{
    private Image Sticky;
    private Text StickyText;
    private Button CloseButton;
    private PlayerMovement PlayerController;
    private FirstPersonCamera PlayerCamera;
    public string StickyTextContent;

    public void Start()
    {
        GameObject UI = GameObject.Find("UserInterface");
        Sticky = UI.GetComponentsInChildren<Image>(true).ToList().Find(x => x.name == "StickyNoteImage");
        StickyText = UI.GetComponentsInChildren<Text>(true).ToList().Find(x => x.name == "StickyText");
        CloseButton = UI.GetComponentsInChildren<Button>(true).ToList().Find(x => x.name == "Close");
        CloseButton.onClick.AddListener(Close);
    }
    
    public void Interact(PlayerMovement controller, FirstPersonCamera playerCamera)
    {
        PlayerController = controller;
        PlayerCamera = playerCamera;

        Cursor.lockState = CursorLockMode.Confined;

        PlayerController.Controlling = false;
        PlayerCamera.Looking = false;
        Sticky.gameObject.SetActive(true);
        StickyText.gameObject.SetActive(true);
        CloseButton.gameObject.SetActive(true);
        StickyText.text = StickyTextContent;
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
