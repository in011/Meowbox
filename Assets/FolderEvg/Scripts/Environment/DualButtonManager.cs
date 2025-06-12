using UnityEngine;

public class DualButtonManager : MonoBehaviour
{
    [Header("Buttons")]
    public DualButton button1;
    public DualButton button2;

    [Header("Object to Spawn")]
    public GameObject objectToSpawn;
    public Transform spawnPoint;

    [Header("Object to Move")]
    public MoveObject[] objectToMove;

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
        if (objectToSpawn != null)
        {
            Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
        }
        if (objectToMove != null)
        {
            foreach(MoveObject obj in objectToMove)
            {
                obj.GoUp();
            }
        }
    }

    public void SpawnReset()
    {
        hasSpawned = false;
    }
}
