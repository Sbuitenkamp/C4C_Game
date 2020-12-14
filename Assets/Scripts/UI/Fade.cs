using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public void StartFade()
    {
        StartCoroutine(DoFade());
    }

    private IEnumerator DoFade()
    {
        List<Image> images = new List<Image>(FindObjectsOfType<Image>(true));
        Image fade = images.Find(x => x.name == "Fade");
        fade.CrossFadeAlpha(1, 2.0f, false);
        yield return new WaitForSeconds(.3f);
    }
}
