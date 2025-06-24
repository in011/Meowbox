using UnityEngine;

public class Block : BaseBlock
{
    [SerializeField] protected float resetTime = 5f;
    [SerializeField] protected float lavaDamping = 11f;

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
        rb.linearDamping = lavaDamping;
        Invoke(nameof(Return), resetTime);
    }
    public void Return()
    {
        rb.linearDamping = originalDamping;
        transform.position = safePos;
    }
}
