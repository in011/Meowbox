using UnityEngine;

public class DualButtonManager : MonoBehaviour
{
    [Header("Buttons")]
    public DualButton button1;
    public DualButton button2;

    [Header("Object to Spawn")]
    public GameObject objectToSpawn;
    public Transform spawnPoint;

    private bool hasSpawned = false;

    void Update()
    {
        if (!hasSpawned && button1.IsPressed && button2.IsPressed)
        {
            SpawnObject();
            hasSpawned = true;
        }
    }

    void SpawnObject()
    {
        Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
    }

    public void SpawnReset()
    {
        hasSpawned = false;
    }
}
