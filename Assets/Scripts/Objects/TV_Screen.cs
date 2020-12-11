using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_Screen : MonoBehaviour
{
    public Renderer ScreenRenderer;

    private string Frame1;
    private string Frame2;
    private bool IsFrame1;
    
    public void Start()
    {
        ScreenRenderer = gameObject.GetComponent<Renderer>();
        
        Frame1 = "Graphics/Textures/News_Animation_1.mat";
        Frame2 = "Graphics/Textures/News_Animation_2.mat";
    }

    public void Update()
    {
        InvokeRepeating(nameof(ChangeScreen), 1f, 1f);
    }

    private void ChangeScreen()
    {
        ScreenRenderer.material.mainTexture = Resources.Load<Texture>(IsFrame1 ? Frame1 : Frame2);
        IsFrame1 = !IsFrame1;
    }
}
