using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("JetPack")]
    [SerializeField] private InputActionReference[] jumpSequence;  // [init, init, vertical, horizontal]
    [SerializeField] private Vector3 jetPackAcceleration = new Vector3(3f, 13f, 3f);

    [SerializeField] private AudioSource audioJetOn, audioJetFlying, audioJetOff;
    [SerializeField] private Transform cameraTransform;

    [Header("Walking")]
    [Range(0, 1)]
    [SerializeField] private float walkSmoothness = 0.2f;
    [SerializeField] private InputActionReference walkingActionReference;
    [SerializeField] private Vector2 walkingSpeed = new Vector2(2.5f, 0.72f);
    [SerializeField] private float minSidewalkSpeed = 0.32f;
    [SerializeField] private LayerMask walkingSurfaceMask;


    [Header("Collider Settings")]
    [SerializeField] private float minColliderHeight = 0.5f;
    [SerializeField] private float maxColliderHeight = 3f;


    private XROrigin xROrigin;
    private CapsuleCollider playerPhysicsCollider;
    private Rigidbody rb;
    private bool isJetOn;
    private bool inAir;

    private void Awake()
    {
        xROrigin = GetComponent<XROrigin>();
        playerPhysicsCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        isJetOn = false;
        inAir = true;
        CenterPhysicsCollider();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        CenterPhysicsCollider();
        //rb.AddForce(Vector3.up.normalized * 13f, ForceMode.Acceleration);
        //return;
        float initZero = jumpSequence[0].action.ReadValue<float>();
        float initOne = jumpSequence[1].action.ReadValue<float>();
        if (isJetOn && initZero > 0f && initOne > 0f) {
            Fly();
        } else if (isJetOn && (initZero == 0f || initOne == 0f)) {
            JetPackOff();
        } else if (!isJetOn && initZero > 0f && initOne > 0f) {
            JetPackOn();
        }
        if (!inAir) {
            Walk();
        }
    }

    private void JetPackOn()
    {
        // audio
        audioJetOff.Stop();
        audioJetFlying.Play();
        audioJetFlying.volume = 0.15f;
        audioJetOn.Play();

        isJetOn = true;
    }

    private void JetPackOff()
    {
        //audio
        audioJetOn.Stop();
        audioJetFlying.Stop();
        audioJetOff.Play();

        isJetOn = false;

    }

    private void Fly()
    {
        Vector3 cameraForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
        Vector3 cameraRight = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;

        float vertical = Mathf.Clamp(jumpSequence[2].action.ReadValue<Vector2>().y, 0f, 1f);
        Vector2 horizontal = jumpSequence[3].action.ReadValue<Vector2>();

        Vector3 jetPackForceDir = new Vector3(horizontal.y, vertical, horizontal.x);
        Vector3 jetPackForce = cameraForward * jetPackForceDir.x * jetPackAcceleration.x 
            + Vector3.up * jetPackForceDir.y * jetPackAcceleration.y 
            + cameraRight * jetPackForceDir.z * jetPackAcceleration.z;

        rb.AddRelativeForce(jetPackForce, ForceMode.Acceleration);

        audioJetFlying.volume = 0.2f * Vector3.Magnitude(jetPackForceDir) + 0.15f;
        if (!audioJetFlying.isPlaying)
            audioJetFlying.Play();
    }

    private void CenterPhysicsCollider()
    {
        Vector3 cameraCenter = xROrigin.CameraInOriginSpacePos;
        playerPhysicsCollider.center = new Vector3(cameraCenter.x, xROrigin.CameraInOriginSpaceHeight / 2, cameraCenter.z);
        playerPhysicsCollider.height = Mathf.Clamp(xROrigin.CameraInOriginSpaceHeight, minColliderHeight, maxColliderHeight);
    }

    private void Walk()
    {
        Vector3 cameraForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
        Vector3 cameraRight = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;

        Vector2 walkVector = walkingActionReference.action.ReadValue<Vector2>();

        //walkVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Vector3 targetVelocity = (cameraForward * walkVector.y * walkingSpeed.x 
            + cameraRight * walkVector.x * walkingSpeed.y * Mathf.Max(minSidewalkSpeed, Mathf.Abs(walkVector.y))) 
            + new Vector3(0f, rb.velocity.y, 0f);
        rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, walkSmoothness);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & walkingSurfaceMask.value) != 0) {
            inAir = false;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & walkingSurfaceMask.value) != 0) {
            inAir = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & walkingSurfaceMask.value) != 0) {
            inAir = false;
        }
    }
}
