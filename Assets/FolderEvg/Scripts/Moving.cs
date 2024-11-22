using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] Transform cam; // ������ ��� ������� ���������� �� ������� ������

    [SerializeField] float speed = 6f;
    [SerializeField] float turnSmoothTime = 0.1f; // �������� �������� � ������� ��������
    float turnSmoothVelocity;

    // For jumping
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float gravityScale = 1f;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float jumpCooldown = 1f;

    float horizontal;
    float vertical;
    float velocity;
    bool readyToJump = true;

    void Update()
    {
        PlayerInput();
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void PlayerInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void Move()
    {
        // Moving
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // ������� � ������� ��������
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg; // Atan2 = ���� ����� ���� y � ��������(x,z) � �������� "+ cam.eulerAngles.y;" � ����� ��� ������� �� ������� ������
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // ��������
            // Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; // ����������� ��� ������� �� ������� ������
            controller.Move(direction * speed * Time.deltaTime); // �������� direction �� moveDir.normalized, ����� �������� ����������� �� ������� ������ 
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded && readyToJump)
        {
            readyToJump = false;
            velocity = Mathf.Sqrt(jumpHeight * -2f * (gravity * gravityScale));
            Invoke(nameof(ResetJump), jumpCooldown); // �������� ����� ResetJump ����� jumpCooldown ������
        }
        velocity += gravity * gravityScale * Time.deltaTime;
        controller.Move(new Vector3(0, velocity, 0) * Time.deltaTime);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
