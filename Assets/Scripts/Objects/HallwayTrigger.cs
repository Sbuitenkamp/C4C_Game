using UnityEngine;

public class HallwayTrigger : MonoBehaviour
{
    public GameObject DoorBell;

    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        DoorBell.GetComponent<AudioSource>().Play();
    }
}
