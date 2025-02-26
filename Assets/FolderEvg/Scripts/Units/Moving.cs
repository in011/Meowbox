using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Moving : MonoBehaviour
{
    AudioSource audioSource;
    AudioManager audioManager;

    [SerializeField] public bool player2 = false;
    CharacterController controller;
    PushObject pushObjectChecker;
    [SerializeField] Animator animator;
    [SerializeField] float sitTimer = 5f;
    private float lastMoveTime = 0.1f;

    [Header("Movement")]
    [SerializeField] float speed = 6f;
    [SerializeField] float turnSmoothTime = 0.1f; // скорость поворота в сторону движени€
    [SerializeField] float fallSpeed = 20f;
    private float turnSmoothVelocity;
    private Vector3 direction;
    bool oldMovement = false;

    [Header("Jumping")]
    [SerializeField] float gravity = -25f;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float jumpCooldown = 0f;
    [SerializeField] float delayedJumpWait = 0.5f;
    private bool delayedJump = false;

    [Header("Pushing Objects")]
    [SerializeField] float accumulatingForce = 1f;
    [SerializeField] private float forceNeeded = 20f;
    private float currentForce = 0f;

    [Header("Dash")]
    [SerializeField] private KeyCode abilityButton = KeyCode.LeftShift;
    [SerializeField] private bool hasDashAbility = true;
    [SerializeField] private float dashSpeed = 6f;
    [SerializeField] private float dashTime = 0.1f;
    [SerializeField] private float dashCD = 3f;
    [SerializeField] private float dashPushPower = 2f;
    // [SerializeField] private TrailRenderer dashTrail;
    private bool isDashing = false;
    private bool canDash = true;

    Vector3 targetLocation = new Vector3(0f, 0f, 0f);
    private float horizontal;
    private float vertical;
    private KeyCode jumpButton;
    private float velocity;
    private bool readyToJump = true;

    [Header("Respawn")]
    [SerializeField] public Vector3 safePos;
    private bool alive = true;

    private void Start()
    {
        safePos = transform.position;
        if (!player2)
        {
            jumpButton = KeyCode.Space;
        }
        else
        {
            jumpButton = KeyCode.RightShift;
        }

        audioManager = FindAnyObjectByType<AudioManager>(); // delete later

        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = audioManager.catWalk;
        audioSource.Play();
        audioSource.Pause();

        controller = gameObject.GetComponent<CharacterController>();
        pushObjectChecker = gameObject.GetComponent<PushObject>();

        
    }

    void Update()
    {
        if (alive)
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                oldMovement = false;
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                oldMovement = true;
            }

            PlayerInput();
            Jump();
            Dash();
        }

        if (controller.velocity.magnitude > 0.1 && controller.isGrounded)
        {
            audioSource.UnPause();
            //audioSource.Play();
        }
        else
        {
            audioSource.Pause();
            //audioSource.Stop();
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
        }
        else
        {
            horizontal = Input.GetAxisRaw("Horizontal2");
            vertical = Input.GetAxisRaw("Vertical2");
        }
    }

    private void Move()
    {
        if (oldMovement)
        {
            // Old Moving 
            direction = new Vector3(horizontal, 0f, vertical).normalized;
        }
        else
        {
            // Camera Direction
            direction = new Vector3(-1f, 0f, 1f) * vertical + new Vector3(1f, 0f, 1f) * horizontal;
            direction.Normalize();
        }

        // ѕровер€ем есть ли достаточно сильное нажатие на клавишу 
        if (direction.magnitude >= 0.1f)
        {
            // настройки анимаций
            animator.SetBool("isRunning", true);
            animator.SetBool("isSitting", false);
            lastMoveTime = Time.time;

            // поворот в сторону движени€
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg; // Atan2 = угол между осью y и вектором(x,z) и добавить "+ cam.eulerAngles.y;" в конец дл€ реакции на поворот камеры
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // ≈сли впереди преп€тствие и мы стоим на земле, начинаем толкать
            // Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; // добавл€етс€ дл€ реакции на поворот камеры
            if (pushObjectChecker.ObjectCollision() && controller.isGrounded)
            {
                animator.SetBool("isPushing", true);

                currentForce += accumulatingForce;

                if (currentForce > forceNeeded)
                {
                    audioManager.PlaySFX(audioManager.rockPush);

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
                animator.SetBool("isPushing", false);

                currentForce = 0f;
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
            StartCoroutine(SitTimer(sitTimer));
        }

        direction = direction * speed + new Vector3(0f, velocity, 0f);

        // ƒвигаем персонажа
        // controller.Move(direction * speed * Time.deltaTime);
        if (isDashing)
        {
            Vector3 dashDirection = new Vector3(direction.x, 0f, direction.z);

            if (pushObjectChecker.ObjectCollision())
            {
                Debug.Log("Object");
                Debug.Log(gameObject.transform.forward * 2);
                float originalPushForce = pushObjectChecker.pushForce;
                pushObjectChecker.pushForce *= dashPushPower;
                pushObjectChecker.ObjectCollisionCheck(gameObject.transform.forward * 2, true, false);
                pushObjectChecker.pushForce = originalPushForce;
                isDashing = false;
                currentForce = 0f;
            }

            controller.Move(dashDirection * dashSpeed * Time.deltaTime);
        }
        else
        {
            controller.Move(direction * Time.deltaTime);
        }
    }

    // ¬ычисл€ет перемещение по оси Y, которое используетс€ в Move()
    private void Jump()
    {
        if (Input.GetKeyDown(jumpButton) || delayedJump)
        {
            currentForce = 0; // обнул€ем накопленную силу толчка

            if (controller.isGrounded && readyToJump && alive)
            {
                readyToJump = false;
                PushUp();

                Invoke(nameof(ResetJump), jumpCooldown); // вызывает метод ResetJump через jumpCooldown секунд

                delayedJump = false;
            }
            else
            {
                delayedJump = true;
                Invoke(nameof(resetJumpDelay), delayedJumpWait); // 0.3f reccomended
            }
        }

        velocity += gravity * Time.deltaTime;
        velocity = Mathf.Clamp(velocity, -fallSpeed, 20f);
    }
    public void PushUp()
    {
        audioManager.PlaySFX(audioManager.catJump);
        animator.SetBool("isRunning", true);
        animator.SetBool("isSitting", false);


        velocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
    private void resetJumpDelay()
    {
        delayedJump = false;
    }

    // на врем€ модифицирует функцию Move
    private void Dash()
    {
        //dashTrail.emitting = false;
        if (Input.GetKeyDown(abilityButton) && canDash && hasDashAbility)
        {
            StartCoroutine(DashTimer());
            StartCoroutine(DashCD());
        }

        // провер€ть есть ли впереди объект
        // если есть, то по нажатию кнопки сильно толкаем его
        // если нет, то на врем€ добавл€ем горизонтальное перемещение

        IEnumerator DashTimer()
        {
            isDashing = true;
            Debug.Log("Dashing");
            yield return new WaitForSeconds(dashTime);

            isDashing = false;
            Debug.Log("Stop");
        }
        IEnumerator DashCD()
        {
            canDash = false;
            Debug.Log("no dash");
            yield return new WaitForSeconds(dashCD);

            canDash = true;
            Debug.Log("canDash!");
        }
    }

    public void Death()
    {
        alive = false;
    }

    public void Revive()
    {
        alive = true;
    }

    IEnumerator SitTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (Time.time - lastMoveTime >= seconds)
        {
            animator.SetBool("isSitting", true);
        }
    }
}