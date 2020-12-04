using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, Interactable
{
    public GameObject TeleportLocation;

    private Transform PlayerTransform;
    private bool Teleport = false;

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
    }
}
