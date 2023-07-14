using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    PlayerInput _playerInput;

    // Can modify in Unity
    [SerializeField]
    float _rotationSpeed = 2.5f;
    float _inputX;

    void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.CharacterControls.Look.performed += OnLook;
    }

    void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }

    void Update()
    {
        HandleLook();
    }

    void OnLook(InputAction.CallbackContext context)
    {
        _inputX = context.ReadValue<Vector2>().x;
    }

    void HandleLook()
    {
        bool isFishing = GameObject.FindWithTag("Bobber");
        if (isFishing)
        {
            return;
        }
        // https://discussions.unity.com/t/how-to-rotate-gameobject-with-mouse-using-new-inputsystem/246683
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        float rotationX = _inputX * _rotationSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up * rotationX);
    }
}
