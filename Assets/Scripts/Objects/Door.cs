using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour, Interactable
{
    public GameObject TeleportLocation;
    public GameObject WanderLocation;

    private AudioSource AudioPlayer { get; set; }
    private AudioClip Open { get; set; }
    private AudioClip Close { get; set; }
    private Transform PlayerTransform { get; set; }
    private bool Teleport { get; set; }
    private bool Wandering { get; set; }
    private int Wandered { get; set; }

    public void Start()
    {
        Teleport = false;
        Wandering = false;
        Wandered = 0;
        AudioPlayer = gameObject.GetComponent<AudioSource>();
        GameObject ui = GameObject.Find("UserInterface");
        Open = Resources.Load<AudioClip>("Audio/Deuropen2");
        Close = Resources.Load<AudioClip>("Audio/Deurdicht2");
    }

    public void FixedUpdate()
    {
        if (!Teleport) return;
        Wander();
        if (Wandering) {
            PlayerTransform.position = WanderLocation.transform.position;
            PlayerTransform.rotation = WanderLocation.transform.rotation;
            Wandering = !Wandering;
        } else {
            PlayerTransform.position = TeleportLocation.transform.position;
            PlayerTransform.rotation = TeleportLocation.transform.rotation;    
        }
        
        Teleport = false;
    }

    public void Interact(PlayerMovement controller, FirstPersonCamera playerCamera)
    {
        PlayerTransform = playerCamera.PlayerBody;
        Teleport = true;

        if (AudioPlayer.isPlaying) AudioPlayer.Stop();
        StartCoroutine(Interaction());
    }

    private IEnumerator Interaction()
    {
        AudioPlayer.PlayOneShot(Open);
        yield return new WaitForSeconds(.5f);
        AudioPlayer.PlayOneShot(Close);
    }

    // chance to move to the wrong room
    private void Wander()
    {
        // scripted for the demo
        if (gameObject.name == "KitchenDoor" && Wandered < 1) {
            Wandering = true;
            Wandered++;
        }
    }
}
