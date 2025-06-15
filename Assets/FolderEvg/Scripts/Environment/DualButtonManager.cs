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
        active = true;
        if(affordances != null)
        {
            foreach(GameObject gameObj in affordances)
            {
                gameObj.SetActive(true);
            }
        }
    }
    public void Deactivate()
    {
        active = true;
        if (affordances != null)
        {
            foreach (GameObject gameObj in affordances)
            {
                gameObj.SetActive(false);
            }
        }
    }

    void SpawnObject()
    {
        if (objectToSpawn != null)
        {
            Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);

            Invoke("Deactivate", 2);
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
