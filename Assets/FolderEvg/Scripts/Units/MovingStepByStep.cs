using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovingStepByStep : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    public float m_Speed = 10f;
    private bool m_MovementInputValue = false;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Получение вводных движения
            m_MovementInputValue = true;
        }
    }

    private void FixedUpdate()
    {
        if (m_MovementInputValue)
        {
            // Вычисляем разницу значения вектора положения за один кадр
            Vector3 movement = transform.forward * m_Speed * Time.deltaTime;

            // Добавляем её к положению персонажа
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
            m_MovementInputValue = false;
        }
    }
}
