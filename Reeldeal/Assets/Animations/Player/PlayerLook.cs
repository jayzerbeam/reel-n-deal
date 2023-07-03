using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    PlayerInput _playerInput;

    // Can modify in Unity
    [SerializeField]
    float _rotationSpeed = 75f;
    float _rotate;

    void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.CharacterControls.Look.performed += OnLook;
    }

    void Update()
    {
        HandleLook();
    }

    void OnLook(InputAction.CallbackContext context)
    {
        _rotate = context.ReadValue<Vector2>().x;
    }

    void HandleLook()
    {
        if (Mouse.current != null && Mouse.current.delta.ReadValue().magnitude > 0f)
        {
            _rotationSpeed = 5f;
        }
        transform.Rotate(Vector3.up * _rotate * _rotationSpeed * Time.deltaTime);
    }
}
