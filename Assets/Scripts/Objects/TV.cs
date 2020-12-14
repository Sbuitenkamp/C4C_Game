using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour, Interactable
{
    public TV_Screen Screen;

    private void Start()
    {
        Screen = GetComponentInChildren<TV_Screen>();
    }

    public void Interact(PlayerMovement controller, FirstPersonCamera playerCamera)
    {
        Screen.StopTv();
        gameObject.tag = "Untagged";
    }
}
