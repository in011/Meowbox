using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    [SerializeField] bool player2;
    CharacterController controller;

    [SerializeField] float speed = 6f;
    [SerializeField] float turnSmoothTime = 0.1f; // скорость поворота в сторону движения
    private float turnSmoothVelocity;

    [Header("Jumping")]
    [SerializeField] float gravity = -50f;
    [SerializeField] float gravityScale = 1f;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float jumpCooldown = 1f;

    private float horizontal;
    private float vertical;
    private KeyCode jumpButton;
    private float velocity;
    private bool readyToJump = true;

    private bool alive = true;
    public Vector3 safePos;

    private void Start()
    {
        safePos = transform.position;

        controller = gameObject.GetComponent<CharacterController>();
    }
    void Update()
    {
        if (alive)
        {
            PlayerInput();
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (alive)
        {
            Move();
        }
    }

    private void PlayerInput()
    {
        if (!player2)
        {
            horizontal = Input.GetAxisRaw("Horizontal1");
            vertical = Input.GetAxisRaw("Vertical1");
            jumpButton = KeyCode.Space;
        }
        else
        {
            horizontal = Input.GetAxisRaw("Horizontal2");
            vertical = Input.GetAxisRaw("Vertical2");
            jumpButton = KeyCode.Keypad0;
        }

    }

    private void Move()
    {
        // Moving
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // поворот в сторону движения
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg; // Atan2 = угол между осью y и вектором(x,z) и добавить "+ cam.eulerAngles.y;" в конец для реакции на поворот камеры
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // движение
            // Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; // добавляется для реакции на поворот камеры
            controller.Move(direction * speed * Time.deltaTime); // заменить direction на moveDir.normalized, чтобы движение реагировало на поворот камеры 
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(jumpButton) && controller.isGrounded && readyToJump && alive)
        {
            readyToJump = false;
            velocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            Invoke(nameof(ResetJump), jumpCooldown); // вызывает метод ResetJump через jumpCooldown секунд
        }
        velocity += gravity * Time.deltaTime;
        controller.Move(new Vector3(0, velocity, 0) * Time.deltaTime);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    public void Death()
    {
        alive = false;
    }

    public void Revive()
    {
        alive = true;
    }
}
