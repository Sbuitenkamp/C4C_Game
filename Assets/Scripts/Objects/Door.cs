using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour, Interactable
{
    public GameObject TeleportLocation;

    private Fade Fader;
    private AudioSource AudioPlayer;
    private AudioClip Open;
//    private AudioClip Close;
    private Transform PlayerTransform;
    private Image FadeImage;
    private bool Teleport = false;
    private bool Fading = false;

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
        PlayerTransform.position = TeleportLocation.transform.position;
        PlayerTransform.rotation = TeleportLocation.transform.rotation;
        Teleport = false;
    }

    public void Interact(PlayerMovement controller, FirstPersonCamera playerCamera)
    {
        PlayerTransform = playerCamera.PlayerBody;
        Teleport = true;
        
//        Fader.StartFade();
//        StartCoroutine(Interaction());
        AudioPlayer.PlayOneShot(Open);
    }

//    private IEnumerator Interaction()
//    {
//        AudioPlayer.PlayOneShot(Open);
//        yield return new WaitForSeconds(.3f);
//        AudioPlayer.PlayOneShot(Close);
//        FadeImage.CrossFadeAlpha(0, 2.0f, false);
//    }
}
