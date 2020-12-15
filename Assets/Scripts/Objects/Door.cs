using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour, Interactable
{
    public GameObject TeleportLocation;
    public GameObject WanderLocation;

    private Fade Fader;
    private AudioSource AudioPlayer;
    private AudioClip Open;
//    private AudioClip Close;
    private Transform PlayerTransform;
    private Image FadeImage;
    private bool Teleport = false;
    private bool Wandering = false;
//    private bool Fading = false;

    public void Start()
    {
        AudioPlayer = gameObject.GetComponent<AudioSource>();
        GameObject ui = GameObject.Find("UserInterface");
        Fader = ui.gameObject.GetComponentInChildren<Fade>();
        Open = Resources.Load<AudioClip>("Audio/Deuropendicht");
//        Close = Resources.Load<AudioClip>("Audio/Deurdicht");
    }

    public void FixedUpdate()
    {
        if (!Teleport) return;
        if (Wandering) {
            PlayerTransform.position = WanderLocation.transform.position;
            PlayerTransform.rotation = WanderLocation.transform.rotation;
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
        
//        Fader.StartFade();
//        StartCoroutine(Interaction());
        if (AudioPlayer.isPlaying) AudioPlayer.Stop();
        AudioPlayer.PlayOneShot(Open);
    }

//    private IEnumerator Interaction()
//    {
//        AudioPlayer.PlayOneShot(Open);
//        yield return new WaitForSeconds(.3f);
//        AudioPlayer.PlayOneShot(Close);
//        FadeImage.CrossFadeAlpha(0, 2.0f, false);
//    }

    // chance to move to the wrong room
    private void Wander()
    {
        
    }
}
