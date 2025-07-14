using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private InputAction thrust;
    [SerializeField] private InputAction Rotation;
    [SerializeField] private float Speed;
    [SerializeField] private float RotationSpeed = 10;
    [SerializeField] private AudioClip MainEngineAudio;
    [SerializeField] private ParticleSystem mainEngineParticle;
    [SerializeField] private ParticleSystem rightEngineParticle;
    [SerializeField] private ParticleSystem leftEngineParticle;

    private AudioSource audioSourse;
    private Rigidbody rb;
    public bool isThrustPressed; // برای دکمه لمسی
    public bool isRotatingLeft; // برای دکمه لمسی چرخش چپ
    public bool isRotatingRight; // برای دکمه لمسی چرخش راست

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSourse = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Rotation.Enable();
        thrust.Enable();
    }

    private void OnDisable()
    {
        Rotation.Disable();
        thrust.Disable();
    }

    private void FixedUpdate()
    {
        Processthrust();
        ProcessRotation();
    }

    private void Processthrust()
    {
        if (thrust.IsPressed() || isThrustPressed)
        {
            rb.AddRelativeForce(Vector3.up * (Speed * Time.fixedDeltaTime));
            if (audioSourse != null && !audioSourse.isPlaying)
            {
                audioSourse.PlayOneShot(MainEngineAudio);
            }
            if (mainEngineParticle != null && !mainEngineParticle.isPlaying)
            {
                mainEngineParticle.Play();
            }
        }
        else
        {
            if (audioSourse != null)
                audioSourse.Pause();
            if (mainEngineParticle != null)
                mainEngineParticle.Pause();
        }
    }

    private void ProcessRotation()
    {
        float InputValue = Rotation.ReadValue<float>();
        if (isRotatingLeft) InputValue = 1f; // چرخش چپ لمسی
        else if (isRotatingRight) InputValue = -1f; // چرخش راست لمسی
        if (InputValue != 0)
        {
            rb.freezeRotation = true;
            transform.Rotate(Vector3.forward * (-InputValue * RotationSpeed * Time.fixedDeltaTime));
            rb.freezeRotation = false;

            if (InputValue > 0 && leftEngineParticle != null && !leftEngineParticle.isPlaying)
            {
                leftEngineParticle.Play();
            }
            else if (InputValue < 0 && rightEngineParticle != null && !rightEngineParticle.isPlaying)
            {
                rightEngineParticle.Play();
            }
        }
        else
        {
            if (leftEngineParticle != null)
                leftEngineParticle.Stop();
            if (rightEngineParticle != null)
                rightEngineParticle.Stop();
        }
    }
}