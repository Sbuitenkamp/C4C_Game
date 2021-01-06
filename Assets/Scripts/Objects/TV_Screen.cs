using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_Screen : MonoBehaviour
{
    private bool IsFrame1 { get; set; }
    private Renderer ScreenRenderer { get; set; }
    private Texture Texture1 { get; set; }
    private Texture Texture2 { get; set; }
    private Texture Off { get; set; }
    private AudioSource AudioPlayer { get; set; }
    private IEnumerator Coroutine { get; set; }
    
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
        
        GameObject.Find("Doorbell").GetComponent<AudioSource>().Play();
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
