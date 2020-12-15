using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour, Interactable
{
    public TV_Screen Screen;

    public void Start()
    {
        Screen = GetComponentInChildren<TV_Screen>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Speech playerVoice = other.gameObject.GetComponent<Speech>();
        playerVoice.Talk("er is tegenwoordig niks leuks meer op tv, laat ik hem uitzetten.", null);
    }

    public void Interact(PlayerMovement controller, FirstPersonCamera playerCamera)
    {
        Screen.StopTv();
        Destroy(gameObject.GetComponent<CapsuleCollider>());
        gameObject.tag = "Untagged";
    }
}
