using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerBody : Entity
{
    [SerializeField] private InputActionReference restartActionReference;

    void Start()
    {
        restartActionReference.action.performed += Restart;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (health <= 0f) {
            SceneManager.LoadScene(0);
        }
    }

    public void Restart(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(0);
    }
}
