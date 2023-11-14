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

    private int index = 0;

    private Vector3 dir;

    private bool move = true;

    private NavMeshAgent agent;

    [SerializeField]
    private Animator animator;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(points[index].position);
        animator.SetBool("Walking", true);


        //dir = (points[index].position - transform.position);
        //transform.forward = dir;
    }

    private void Update()
    {
        //if (move)
        //{
        //    transform.position = transform.position + (dir * velocity);
        //}
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
                print("Destino alcanzado");

            }
            else
            {
                agent.SetDestination(points[index].position);
                //dir = (points[index].position - transform.position);
                //transform.forward = dir;
            }
        }
    }
}
