using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    [SerializeField] bool player2;
    CharacterController controller;
    PushObject pushObjectChecker;

    [SerializeField] float speed = 6f;
    [SerializeField] float turnSmoothTime = 0.1f; // скорость поворота в сторону движени€
    private float turnSmoothVelocity;

    [Header("Jumping")]
    [SerializeField] float gravity = -25f;
    [SerializeField] float gravityScale = 1f;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float jumpCooldown = 1f;
    private bool delayedJump = false;

    [Header("Pushing Objects")]
    [SerializeField] float pushForce = 1f;
    [SerializeField] private float forceNeeded = 20f;
    private float currentForce = 0f;

    Vector3 targetLocation = new Vector3(0f, 0f, 0f);
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
        pushObjectChecker = gameObject.GetComponent<PushObject>();
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
            jumpButton = KeyCode.RightControl;
        }

    }

    private void Move()
    {
        // Moving
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // поворот в сторону движени€
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg; // Atan2 = угол между осью y и вектором(x,z) и добавить "+ cam.eulerAngles.y;" в конец дл€ реакции на поворот камеры
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // движение
            // Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; // добавл€етс€ дл€ реакции на поворот камеры
            if (pushObjectChecker.ObjectCollision())
            {
                currentForce += pushForce;

                if (currentForce > forceNeeded)
                {
                    pushObjectChecker.ObjectCollisionCheck(direction * 2, true, false);
                    currentForce = 0f;
                }
                else
                {
                    direction = new Vector3(0f, 0f, 0f).normalized;
                }
            }
            else
            {
                currentForce = 0f;
            }
        }

        direction = direction * speed + new Vector3(0f, velocity, 0f);

        //controller.Move(direction * speed * Time.deltaTime);
        controller.Move(direction * Time.deltaTime);
    }

    // ¬ычисл€ет перемещение по оси Y, которое используетс€ в Move()
    private void Jump()
    {
        if (Input.GetKeyDown(jumpButton) || delayedJump)
        {
            if (controller.isGrounded && readyToJump && alive)
            {
                readyToJump = false;
                velocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
                Invoke(nameof(ResetJump), jumpCooldown); // вызывает метод ResetJump через jumpCooldown секунд
                
                delayedJump = false;
            }
            else
            {
                delayedJump = true;
                Invoke(nameof(resetJumpDelay), 0.5f); // 0.3f reccomended
            }
        }

        velocity += gravity * Time.deltaTime;
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
    private void resetJumpDelay()
    {
        delayedJump = false;
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
