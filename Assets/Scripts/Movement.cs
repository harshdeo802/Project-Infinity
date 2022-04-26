using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //PARAMETERS - for tuning, typically set in the editor
    //CACHE - e.g. references for readability or speed
    //STATE - private instance (member) variables

    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem thrusterParticle1;
    [SerializeField] ParticleSystem thrusterParticle2;
    [SerializeField] ParticleSystem jetParticle;
    
    Rigidbody rb;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up* mainThrust *Time.deltaTime);
            thrusterParticle1.Play();
            thrusterParticle2.Play();
            jetParticle.Play();
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
        }
        else
        {
            audioSource.Stop();
        }    
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // Unfreeze the rotation so the physics rotation continues
    }
}
