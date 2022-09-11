using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FeetMovement : MonoBehaviour
{
    [SerializeField] private float isMovingThreshold = 0.12f;
    [Range(0, 1)]
    [SerializeField] private float smoothnessFactor = 0.2f;

    private Animator animator;
    private Transform bodyPos;
    private Vector3 previousPos;

    void Start()
    {
        animator = GetComponent<Animator>();
        bodyPos = GetComponent<Transform>();
        previousPos = bodyPos.position;
    }

    void Update()
    {
        Vector3 speed = (bodyPos.position - previousPos) / Time.deltaTime;
        speed.y = 0f;
        previousPos = bodyPos.position;

        float prevX = animator.GetFloat("x");
        float prevY = animator.GetFloat("y");

        animator.SetFloat("x", Mathf.Lerp(prevX, Mathf.Clamp(speed.x, -1f, 1f), smoothnessFactor));
        animator.SetFloat("y", Mathf.Lerp(prevY, Mathf.Clamp(speed.z, -1f, 1f), smoothnessFactor));
        animator.SetBool("isMoving", speed.magnitude > isMovingThreshold);
    }

    private void OnDisable()
    {
        animator.SetFloat("x", 0f);
        animator.SetFloat("y", 0f);
        animator.SetBool("isMoving", false);
    }
}
