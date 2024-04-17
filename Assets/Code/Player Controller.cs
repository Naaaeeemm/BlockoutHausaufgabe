using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class Player : MonoBehaviour
{
    private InputAction move;
    private InputAction look;

    private float cameraXRotation;

    private Vector3 velocity;

    //-------------------------

    public PlayerInput PlayerInput;
    public CharacterController Controller;
    public Transform Camera;

    public float Speed = 10f;

    public float Gravity = -9.8f; //m/s/s



    // Start is called before the first frame update
    void Start()
    {
        move = PlayerInput.actions.FindAction("Move");
        look = PlayerInput.actions.FindAction("Look");
        cameraXRotation = Camera.rotation.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = move.ReadValue<Vector2>();

        Vector3 moveAmount = transform.forward * moveInput.y + transform.right * moveInput.x;

        velocity.x = moveAmount.x;
        velocity.y += Gravity * Time.deltaTime;
        velocity.z = moveAmount.z;

        moveAmount.y += Gravity * Time.deltaTime;

        if (Controller.Move(velocity * Speed * Time.deltaTime) == CollisionFlags.Below)
            velocity.y = 0;

        Vector2 lookInput = look.ReadValue<Vector2>();
        Vector3 cameraRotation = Camera.rotation.eulerAngles;
        cameraXRotation += lookInput.y;

        cameraXRotation = Mathf.Clamp(cameraXRotation, 0, 90);

        cameraRotation.x = cameraXRotation;

        Camera.eulerAngles = cameraRotation;

        transform.Rotate(0, lookInput.x, 0);

    }
}

