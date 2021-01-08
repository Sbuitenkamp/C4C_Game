using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorEnd : MonoBehaviour, Interactable
{
    public void Interact(PlayerMovement controller, FirstPersonCamera playerCamera)
    {
        SceneManager.LoadScene(2);
    }
}
