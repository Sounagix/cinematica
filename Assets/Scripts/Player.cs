using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Transform[] points;

    [SerializeField]
    private float velocity;

    [SerializeField]
    private CameraMaster master;

    private int index = 0;

    private Vector3 dir;

    private bool move = true;

    private NavMeshAgent agent;

    [SerializeField]
    private Animator animator;

    private AudioSource audioSource;

    [SerializeField]
    private Animator carAnimator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();  
    }

    public void StartWalking()
    {
        agent.SetDestination(points[index].position);
        animator.SetBool("Walking", true);
        audioSource.Play();
        carAnimator.SetTrigger("Car");
        carAnimator.gameObject.GetComponent<AudioSource>().Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Point"))
        {
            index++;
            if (index >= points.Length)
            {
                move = false;
                animator.SetBool("Walking", false);
                audioSource.Stop();
                print("Destino alcanzado");

            }
            else
            {
                agent.SetDestination(points[index].position);
                //dir = (points[index].position - transform.position);
                //transform.forward = dir;
            }
        }
        else if (other.CompareTag("Trigger"))
        {
            master.OnTrigger();
            Destroy(other.gameObject);
        }
    }
}
