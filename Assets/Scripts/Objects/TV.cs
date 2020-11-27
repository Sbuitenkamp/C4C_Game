using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour, Interactable
{
    public bool Active = true;
    public GameObject Screen;

    private void Start()
    {
//        Screen = GetComponentInChildren<Plane>();
    }

    public void Interact()
    {
        Screen.gameObject.SetActive(!Active);
        Active = !Active;
    }
}
