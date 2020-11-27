using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, Interactable
{
    public int Target = 0;

    public void Interact()
    {
        SceneManager.LoadScene(Target);
    }
}
