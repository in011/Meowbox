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

    [Header("Affordances")]
    public GameObject[] affordances;

    private bool hasSpawned = false;
    private bool active = true;


    void Update()
    {
        if (active && !hasSpawned && button1.IsPressed && button2.IsPressed)
        {
            SpawnObject();
            hasSpawned = true;
        }
    }

    public void Activate()
    {
        Debug.Log("Active");
        active = true;
        if(affordances != null)
        {
            foreach(GameObject gameObj in affordances)
            {
                gameObj.SetActive(true);
            }
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
