using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorbell : MonoBehaviour
{
    private AudioSource AudioPlayer;
    private Speech PlayerVoice;
    private bool Talk = false;
    private int Talked = 0;
    
    public void Start()
    {
        AudioPlayer = gameObject.GetComponent<AudioSource>();
        PlayerVoice = GameObject.Find("Player").GetComponent<Speech>();
    }

    public void Update()
    {
        if (AudioPlayer.isPlaying) StartCoroutine(StartTalking());
        if (!Talk || Talked > 2) return;
        if (Talked == 0) PlayerVoice.Talk("oh daar zul je Hans hebben, laat ik naar de voordeur gaan.", Resources.Load<AudioClip>("Audio/VoiceLines/Oh_Hans"));
        else PlayerVoice.Talk("oh daar zul je Elise hebben, laat ik naar de voordeur gaan.", Resources.Load<AudioClip>("Audio/VoiceLines/Oh_Elise"));
        Talk = false;
        Talked++;
    }

    private IEnumerator StartTalking()
    {
        yield return new WaitUntil(() => !AudioPlayer.isPlaying);
        Talk = true;
    }
}
