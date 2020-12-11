using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController Controller;
    public Transform GroundCheck;
    public LayerMask GroundMask;
    public AudioSource FootSteps;
    public float Speed = 5f;
    public float Gravity = -9.81f;
    public float GroundDistance = 0.4f;
    public bool Controlling = true;

    private Vector3 Velocity;
    private bool IsGrounded;

    public void Update()
    {
        // lock movement if doing activity
        if (!Controlling) return;
        
        IsGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        if (IsGrounded && Velocity.y < 0) Velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Transform transform1 = transform;
        Vector3 move = transform1.right * x + transform1.forward * z;

        Controller.Move(Speed * Time.deltaTime * move);
        Velocity.y += Gravity * Time.deltaTime;
        Controller.Move(Velocity * Time.deltaTime);
        
        // footstep sound effect will play for as long as one of the movement keys is pressed
        if ((Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")) && !FootSteps.isPlaying) FootSteps.Play();
        else if (!(Input.GetButton("Horizontal") || Input.GetButton("Vertical"))) FootSteps.Stop();
    }
}
