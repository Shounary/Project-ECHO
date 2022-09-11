using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;
    public bool isOpen = true;

    void Start()
    {
        animator.SetBool("isOpen", isOpen);
    }

    public void Open()
    {
        isOpen = true;
        animator.SetBool("isOpen", isOpen);
    }

    public void Close()
    {
        isOpen = false;
        animator.SetBool("isOpen", isOpen);
    }
}
