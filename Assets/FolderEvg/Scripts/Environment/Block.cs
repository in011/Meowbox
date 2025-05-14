using UnityEngine;

public class Block : BaseBlock
{
    [SerializeField] protected float resetTime = 5f;
    public Vector3 safePos;
    private float originalDamping;
    private Rigidbody rb;

    void Start()
    {
        safePos = transform.position;
        rb = gameObject.GetComponent<Rigidbody>();
        originalDamping = rb.linearDamping;
    }

    public override void Respawn()
    {
        rb.linearDamping = 10;
        Invoke(nameof(Return), resetTime);
    }
    private void Return()
    {
        rb.linearDamping = originalDamping;
        transform.position = safePos;
    }
}
