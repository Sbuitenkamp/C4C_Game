using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour, Interactable
{
    public GameObject TeleportLocation;

    private Transform PlayerTransform;
    private Image FadeImage;
    private bool Teleport = false;
    private bool Fading = false;

    public void Start()
    {
        List<Image> images = new List<Image>(FindObjectsOfType<Image>(true));
        FadeImage = images.Find(x => x.name == "Fade");
    }

    public void Update()
    {
        // screen fading to black while teleporting
        if (Fading) {
            FadeImage.CrossFadeAlpha(1, 2.0f, false);
        } else {
            FadeImage.CrossFadeAlpha(0, 2.0f, false);
        }

    }

    public void FixedUpdate()
    {
        if (!Teleport) return;
        PlayerTransform.position = TeleportLocation.transform.position;
        PlayerTransform.rotation = TeleportLocation.transform.rotation;
        Teleport = false;
    }

    public void Interact(PlayerMovement controller, FirstPersonCamera playerCamera)
    {
        PlayerTransform = playerCamera.PlayerBody;
        Teleport = true;
        Fading = true;
    }
}
