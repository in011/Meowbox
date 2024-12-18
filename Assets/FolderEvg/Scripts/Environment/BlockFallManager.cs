using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFallManager : MonoBehaviour
{
    AudioManager audioManager;

    [SerializeField] private GameObject[] blockPrefabs;
    [SerializeField] private Vector3[] blockCoordinates;
    [SerializeField] private float timeBetweenBlocks = 3f;
    [SerializeField] private int maxBlocks = 4;
    private int blockIndex = 0;
    private int blockCount = 0;
    [SerializeField] private float newLavaSpeed;
    private LavaScript lavaScript;

    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        lavaScript = FindAnyObjectByType<LavaScript>();
    }

    public void Activate()
    {
        DropBlock(); // Первый блок
        audioManager.MusicChange();
        lavaScript.riseSpeed = newLavaSpeed;
    }
    private void DropBlock()
    {
        if(blockCount > maxBlocks)
        {
            return;
        }

        if (blockCoordinates.Length == 0)
        {
            int randXPos = Random.Range(-7, 5);
            int randZPos = Random.Range(-7, 5);
            blockIndex = Random.Range(0, blockPrefabs.Length);

            if (randXPos % 2 == 0)
            {
                randXPos -= 1;
            }
            if (randZPos % 2 == 0)
            {
                randZPos -= 1;
            }

            Instantiate(blockPrefabs[blockIndex], new Vector3((float)randXPos, 20f, (float)randZPos), blockPrefabs[blockIndex].transform.rotation);
            Invoke(nameof(DropBlock), timeBetweenBlocks); // Вызываем следующий блок
        }
        else
        {
            int randPrefabIndex = Random.Range(0, blockPrefabs.Length);
            Instantiate(blockPrefabs[randPrefabIndex], blockCoordinates[blockIndex], blockPrefabs[randPrefabIndex].transform.rotation);
            if(blockIndex == blockCoordinates.Length - 1)
            {
                blockIndex = 0;
            }
            else
            {
                blockIndex++;
            }
        }
        blockCount++;

        Invoke(nameof(DropBlock), timeBetweenBlocks); // Вызываем следующий блок
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
