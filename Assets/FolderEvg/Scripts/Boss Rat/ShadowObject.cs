using UnityEngine;

public class ShadowObject : MonoBehaviour
{
    public GameObject bottleObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("Activate", 1f);

        Destroy(gameObject, 2);
    }
    void Activate()
    {
        bottleObject.SetActive(true);
    }
}
