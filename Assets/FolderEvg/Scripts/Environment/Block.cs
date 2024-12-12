using UnityEngine;

public class Block : MonoBehaviour
{
    public Vector3 safePos;

    void Start()
    {
        safePos = transform.position;
    }

    public void Respawn()
    {
        Invoke(nameof(Return), 2f);
    }
    private void Return()
    {
        transform.position = safePos;
    }
}
