using UnityEngine;

public class TV : MonoBehaviour, Interactable
{
    public TV_Screen Screen;

    public void Start()
    {
        Screen = GetComponentInChildren<TV_Screen>();
    }

    public void Interact(PlayerMovement controller, FirstPersonCamera playerCamera)
    {
        Screen.StopTv();
        Destroy(gameObject.GetComponent<CapsuleCollider>());
        gameObject.tag = "Untagged";
    }
}
