using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Speech : MonoBehaviour
{
    public AudioSource Voice { get; set; }
    
    private Text SubtitleBar { get; set; }

    public void Start()
    {
        GameObject ui = GameObject.Find("UserInterface");
        Voice = gameObject.GetComponentInParent<AudioSource>();
        Debug.Log(Voice);
        SubtitleBar = ui.GetComponentsInChildren<Text>(true).ToList().Find(x => x.name == "Subtitles");
    }

    public void Talk(string message, AudioClip speech)
    {
        SubtitleBar.text = "Oma Jansje: " + message;
        Voice.PlayOneShot(speech);
        StartCoroutine(StopTalking());
    }

    private IEnumerator StopTalking()
    {
        yield return new WaitUntil(() => !Voice.isPlaying);
        SubtitleBar.text = "";
    }
}
