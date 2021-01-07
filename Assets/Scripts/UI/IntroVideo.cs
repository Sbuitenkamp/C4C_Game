using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class IntroVideo : MonoBehaviour
{
    public GameObject VideoImage;
    public GameObject Menu;
    
    private VideoPlayer Player;

    public void Start()
    {
        Player = gameObject.GetComponentInChildren<VideoPlayer>();
        StartCoroutine(SwitchScene());
    }

    private IEnumerator SwitchScene()
    {
        yield return new WaitUntil(() => Player.isPlaying);
        while (Player.isPlaying) yield return null;
        VideoImage.SetActive(false);
        Menu.SetActive(true);
    }
}
