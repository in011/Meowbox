using UnityEngine;

public class Block : BaseBlock
{
    [SerializeField] protected float resetTime = 5f;
    public Vector3 safePos;

    void Start()
    {
        safePos = transform.position;
    }

    public override void Respawn()
    {
        gameObject.GetComponent<Rigidbody>().linearDamping = 10;
        Invoke(nameof(Return), resetTime);
    }
    private void Return()
    {
        gameObject.GetComponent<Rigidbody>().linearDamping = 0;
        transform.position = safePos;
    }
}
