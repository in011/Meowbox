using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] float resetTime = 2f;
    public Vector3 safePos;

    void Start()
    {
        safePos = transform.position;
    }

    public void Respawn()
    {
        Invoke(nameof(Return), resetTime);
    }
    private void Return()
    {
        transform.position = safePos;
    }
}
