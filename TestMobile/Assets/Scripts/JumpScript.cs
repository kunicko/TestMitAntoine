using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpScript : MonoBehaviour
{
    [SerializeField] private float _jumpheight = 5;
    private Rigidbody _rb;

    public InputAction jump;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        jump.started += OnJumpInput;
    }

    private void OnJumpInput(InputAction.CallbackContext obj)
    {
        _rb.AddForce(Vector3.up * _jumpheight, ForceMode.Impulse);
    }

    private void OnEnable()
    {
        jump.Enable();
    }

    private void OnDisable()
    {
        jump.Disable();
    }
}
