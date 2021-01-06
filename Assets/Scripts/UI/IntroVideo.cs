using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class IntroVideo : MonoBehaviour
{
    public GameObject VideoImage;
    public RawImage Controls;
    
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
        Controls.enabled = true;
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(1);
    }
}
