using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Moving : MonoBehaviour
{
    AudioSource audioSource;
    AudioManager audioManager;

    [SerializeField] bool player2;
    CharacterController controller;
    PushObject pushObjectChecker;
    [SerializeField] Animator animator;
    [SerializeField] float sitTimer = 5f;
    private float lastMoveTime = 0.1f;

    [Header("Movement")]
    [SerializeField] float speed = 6f;
    [SerializeField] float turnSmoothTime = 0.1f; // �������� �������� � ������� ��������
    private float turnSmoothVelocity;

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
            PlayerInput();
            Jump();
        }

        if(controller.velocity.magnitude > 0.1 && controller.isGrounded)
        {
            Debug.Log("Moving");
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

        // ��������� ���� �� ���������� ������� ������� �� ������� 
        if (direction.magnitude >= 0.1f)
        {
            // ��������� ��������
            animator.SetBool("isRunning", true);
            animator.SetBool("isSitting", false);
            lastMoveTime = Time.time;

            // ������� � ������� ��������
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg; // Atan2 = ���� ����� ���� y � ��������(x,z) � �������� "+ cam.eulerAngles.y;" � ����� ��� ������� �� ������� ������
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // ���� ������� ����������� � �� ����� �� �����, �������� �������
            // Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; // ����������� ��� ������� �� ������� ������
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

        // ������� ���������
        // controller.Move(direction * speed * Time.deltaTime);
        controller.Move(direction * Time.deltaTime);
    }

    // ��������� ����������� �� ��� Y, ������� ������������ � Move()
    private void Jump()
    {
        if (Input.GetKeyDown(jumpButton) || delayedJump)
        {
            audioManager.PlaySFX(audioManager.catJump);

            currentForce = 0; // �������� ����������� ���� ������

            if (controller.isGrounded && readyToJump && alive)
            {
                readyToJump = false;
                velocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
                Invoke(nameof(ResetJump), jumpCooldown); // �������� ����� ResetJump ����� jumpCooldown ������
                
                delayedJump = false;
            }
            else
            {
                delayedJump = true;
                Invoke(nameof(resetJumpDelay), delayedJumpWait); // 0.3f reccomended
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

    IEnumerator SitTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (Time.time - lastMoveTime >= seconds)
        {
            animator.SetBool("isSitting", true);
        }
    }
}
