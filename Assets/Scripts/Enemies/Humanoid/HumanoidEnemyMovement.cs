using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class HumanoidEnemyMovement : MonoBehaviour
{
    [SerializeField] private AudioSource stepsAudio;
    [SerializeField] private float stepThreshold = 0.4f;
    private Animator animator;
    private NavMeshAgent navAgent;
    

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navAgent.updatePosition = false;
        navAgent.updateRotation = false;
    }

    private void Update()
    {
        Vector3 moveVectorWorld = navAgent.nextPosition - transform.position;

        float deltaX = Vector3.Dot(transform.right, moveVectorWorld) / Time.deltaTime;
        float deltaY = Vector3.Dot(transform.forward, moveVectorWorld) / Time.deltaTime;

        animator.SetFloat("x", deltaX);
        animator.SetFloat("y", deltaY);

        if (stepsAudio != null) {
            if (!stepsAudio.isPlaying && moveVectorWorld.magnitude / Time.deltaTime >= stepThreshold) {
                stepsAudio.Play();
            }
            if (stepsAudio.isPlaying && moveVectorWorld.magnitude / Time.deltaTime < stepThreshold) {
                stepsAudio.Stop();
            }
        }
    }

    private void OnAnimatorMove()
    {
        transform.position = navAgent.nextPosition;
    }
}
