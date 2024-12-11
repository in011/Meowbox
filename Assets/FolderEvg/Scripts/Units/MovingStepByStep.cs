using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingStepByStep : MonoBehaviour
{
    [SerializeField] bool player2;
    CharacterController controller;
    SbS_PushObject pushObjectChecker;

    [SerializeField] float speed = 6f;
    float turnSmoothTime = 0.01f; // скорость поворота в сторону движения
    private float turnSmoothVelocity;

    [Header("Jumping")]
    [SerializeField] float gravity = -25f;
    [SerializeField] float gravityScale = 10f;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float jumpCooldown = 1f;

    private float horizontal;
    private float vertical;
    private KeyCode jumpButton;
    private float velocity;
    private bool readyToJump = true;

    private bool alive = true;
    public Vector3 safePos;

    bool moving = false;
    bool movingFB = false; // forward or back
    bool movingLR = false; // left or right
    Vector3 targetLocation = new Vector3(0f,0f,0f);

    private void Start()
    {
        safePos = transform.position;

        controller = gameObject.GetComponent<CharacterController>();
        pushObjectChecker = gameObject.GetComponent<SbS_PushObject>();

        if (!player2) // Назначаем кнопку прыжка (временное решение)
        {
            jumpButton = KeyCode.Space; 
        }
        else
        {
            jumpButton = KeyCode.RightControl;
        }
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
            if (Input.GetKeyDown(KeyCode.W) && !movingFB)
            {
                Debug.Log(transform.position + " " + targetLocation);

                targetLocation = transform.position + new Vector3(0f, 0f, 2f);
                RotateToDirection();
                Debug.Log("CheckForward");
                if (pushObjectChecker.ObjectCollisionCheck(new Vector3(0f, 0f, 2f), true, false))
                {
                    movingFB = true;
                    Debug.Log("Move");
                }
                else
                {
                    Debug.Log("Object");
                    targetLocation = transform.position;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.S) && !movingFB)
                {
                    Debug.Log(transform.position + " " + targetLocation);

                    targetLocation = transform.position + new Vector3(0f, 0f, -2f);
                    RotateToDirection();
                    if (pushObjectChecker.ObjectCollisionCheck(new Vector3(0f, 0f, -2f), true, false))
                    {
                        movingFB = true;
                    }
                    else
                    {
                        targetLocation = transform.position;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.A) && !movingLR)
            {
                targetLocation = transform.position + new Vector3(-2f, 0f, 0f);
                RotateToDirection();
                if (pushObjectChecker.ObjectCollisionCheck(new Vector3(-2f, 0f, 0f), true, false))
                {
                    movingFB = true;
                }
                else
                {
                    targetLocation = transform.position;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.D) && !movingLR)
                {
                    targetLocation = transform.position + new Vector3(2f, 0f, 0f);
                    RotateToDirection();
                    if (pushObjectChecker.ObjectCollisionCheck(new Vector3(2f, 0f, 0f), true, false))
                    {
                        movingFB = true;
                    }
                    else
                    {
                        targetLocation = transform.position;
                    }
                }
            }
        }
        else
        {
            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.I)) && !movingFB)
            {
                Debug.Log(transform.position + " " + targetLocation);

                targetLocation = transform.position + new Vector3(0f, 0f, 2f);
                RotateToDirection();
                Debug.Log("CheckForward");
                if (pushObjectChecker.ObjectCollisionCheck(new Vector3(0f, 0f, 2f), true, false))
                {
                    movingFB = true;
                    Debug.Log("Move");
                }
                else
                {
                    Debug.Log("Object");
                    targetLocation = transform.position;
                }
            }
            else
            {
                if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.K)) && !movingFB)
                {
                    Debug.Log(transform.position + " " + targetLocation);

                    targetLocation = transform.position + new Vector3(0f, 0f, -2f);
                    RotateToDirection();
                    if (pushObjectChecker.ObjectCollisionCheck(new Vector3(0f, 0f, -2f), true, false))
                    {
                        movingFB = true;
                    }
                    else
                    {
                        targetLocation = transform.position;
                    }
                }
            }
            if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.J)) && !movingLR)
            {
                targetLocation = transform.position + new Vector3(-2f, 0f, 0f);
                RotateToDirection();
                if (pushObjectChecker.ObjectCollisionCheck(new Vector3(-2f, 0f, 0f), true, false))
                {
                    movingFB = true;
                }
                else
                {
                    targetLocation = transform.position;
                }
            }
            else
            {
                if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.L)) && !movingLR)
                {
                    targetLocation = transform.position + new Vector3(2f, 0f, 0f);
                    RotateToDirection();
                    if (pushObjectChecker.ObjectCollisionCheck(new Vector3(2f, 0f, 0f), true, false))
                    {
                        movingFB = true;
                    }
                    else
                    {
                        targetLocation = transform.position;
                    }
                }
            }
        }
    }

    private void RotateToDirection()
    {
        Vector3 direction = targetLocation - transform.position;

        // поворот в сторону движения
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg; // Atan2 = угол между осью y и вектором(x,z) и добавить "+ cam.eulerAngles.y;" в конец для реакции на поворот камеры
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f); // before was angle
    }

    private void Move()
    {
        // Moving
        if (movingLR || movingFB)
        {
            Debug.Log("Moving" + transform.position + " " + targetLocation);
            transform.position = targetLocation;
            movingLR = false;
            movingFB = false;
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
