using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class OutroVideo : MonoBehaviour
{
    public GameObject VideoImage;
    public Text ThankYouMessage;
    public Image Background;
    
    private VideoPlayer Player { get; set; }

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
        Background.gameObject.SetActive(true);
        ThankYouMessage.gameObject.SetActive(true);
    }
}
