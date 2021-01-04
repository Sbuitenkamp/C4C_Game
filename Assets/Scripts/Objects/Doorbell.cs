using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorbell : MonoBehaviour
{
    private AudioSource AudioPlayer;
    private Speech PlayerVoice;
    private bool Talk = false;
    
    public void Start()
    {
        AudioPlayer = gameObject.GetComponent<AudioSource>();
        PlayerVoice = GameObject.Find("Player").GetComponent<Speech>();
    }

    public void Update()
    {
        if (AudioPlayer.isPlaying) StartCoroutine(StartTalking());
        if (Talk) {
            PlayerVoice.Talk("oh daar zul je Hans hebben, laat ik naar de voordeur gaan.", Resources.Load<AudioClip>("Audio/VoiceLines/Oh_Hans"));
            Talk = false;
        }
    }

    private IEnumerator StartTalking()
    {
        yield return new WaitUntil(() => !AudioPlayer.isPlaying);
        Talk = true;
    }
}
