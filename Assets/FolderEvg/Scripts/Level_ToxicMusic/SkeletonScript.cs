using UnityEngine;
using System.Collections;

public class SkeletonScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _KeySounds;
    [SerializeField] private Transform soundSpawn; // place where sounds will be spawned
    [SerializeField] private float attackStrengh = 7.5f; // block push speed needed to trigger animation
    [SerializeField] private float repeatTime = 5f; // time before repeating animation
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float flySpeed = 2f;
    [SerializeField] private float lifetime = 1.5f;

    private bool awake = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block") && !awake)
        {
            awake = true;
            Rigidbody rb = other.attachedRigidbody;
            if (rb.linearVelocity.magnitude > attackStrengh)
            {
                Debug.Log("Music Play");

                PlayMusic();
            }
        }
    }

    private void PlayMusic()
    {
        StartCoroutine(SpawnObjects());

        Invoke(nameof(PlayMusic), repeatTime);
    }

    IEnumerator SpawnObjects()
    {
        Debug.Log("Music started");

        foreach (GameObject soundObj in _KeySounds)
        {
            Debug.Log("spawn!");

            GameObject obj = Instantiate(soundObj, soundSpawn);
            StartCoroutine(MoveAndDestroy(obj));
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator MoveAndDestroy(GameObject obj)
    {
        float elapsedTime = 0f;
        while (elapsedTime < lifetime)
        {
            obj.transform.position += Vector3.up * flySpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(obj);
    }
}
