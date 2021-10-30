using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Camera Variables
    [SerializeField] Transform playerCamera = null;
    public float cameraSensitivity = 3.5f;

    float cameraPitch = 0.0f;

    public Camera camera;

    //Player Movement Variables
    [SerializeField] float speed = 12f;
    [SerializeField] float Gravity = -9.81f;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] float JumpHeight = 3f;

    //Knife Variables
    public Animator anim;

    public CharacterController controller;

    Vector3 Velocity;

    public Transform groundCheck;

    public LayerMask groundMask;

    bool isGrounded;
    bool SilentWalking;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();

    }

    //Contains all the mouse look functionality
    void UpdateMouseLook()
    {

        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        cameraPitch -= mouseDelta.y * cameraSensitivity;

        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        transform.Rotate(Vector3.up * mouseDelta.x * cameraSensitivity);

    }

    void UpdateMovement()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        Velocity.y += Gravity * Time.deltaTime;

        controller.Move(Velocity * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && Velocity.y < 0)
        {
            Velocity.y = -2f;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 18f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 12f;
        }
    }
}
