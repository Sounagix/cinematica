using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private ParticleSystem ptc;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();  
    }


    private void OnCollisionEnter(Collision collision)
    {
        audioSource.Play();
        ptc.Play();
    }
}
