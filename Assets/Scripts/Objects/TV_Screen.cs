using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_Screen : MonoBehaviour
{
    private bool IsFrame1;
    private Renderer ScreenRenderer;
    private Texture Texture1;
    private Texture Texture2;
    private Texture Off;
    private AudioSource AudioPlayer;
    private IEnumerator Coroutine;
    
    public void Start()
    {
        ScreenRenderer = gameObject.GetComponent<Renderer>();
        AudioPlayer = gameObject.GetComponent<AudioSource>();
        
        Texture1 = Resources.Load<Texture>("Graphics/Textures/News_Animation_1");
        Texture2 = Resources.Load<Texture>("Graphics/Textures/News_Animation_2");
        Off = Resources.Load<Texture>("Graphics/Textures/TV_Black");

        Coroutine = ChangeScreenRoutine();
        StartCoroutine(Coroutine);
    }

    public void StopTv()
    {
        StopCoroutine(Coroutine);
        ScreenRenderer.material.SetTexture("_MainTex", Off);
        AudioPlayer.Stop();
    }

    private void ChangeScreen()
    {
        ScreenRenderer.material.SetTexture("_MainTex", IsFrame1 ? Texture1 : Texture2);
        IsFrame1 = !IsFrame1;
    }

    // method for looping
    private IEnumerator ChangeScreenRoutine()
    {
        while (true) {
            yield return new WaitForSeconds(.3f);
            ChangeScreen();
        }
    }
}
