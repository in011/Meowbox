using UnityEngine;
using System.Collections;

public class DialogZone : MonoBehaviour
{
    [Header("Activation")]
    [SerializeField] private bool active = true;
    [SerializeField] private bool restartable = false;
    [SerializeField] private bool addNextLevelScore = false;

    [Header("QuestItems")]
    [SerializeField] private bool QuestItemNeeded = false;
    [SerializeField] private string ItemName = "Key_0";
    private QuestItem neededItem;
    [SerializeField] private GameObject[] ActivateObjects;
    [SerializeField] private GameObject[] DeactivateObjects;

    [Header("Links to other dialogs")]
    [SerializeField] private DialogZone[] NextDialog;
    [SerializeField] private DialogZone[] AbortDialogs;

    [Header("Lines")]
    [SerializeField] private GameObject[] TextQueuePrefabs;
    [SerializeField] public Vector3 TextOffset = new Vector3(0, 0, 0);
    [SerializeField] public Vector3 RandomizeIntensity = new Vector3(0, 0, 0);

    private bool played = false;
    private GameObject CurrentTextLine;
    private FloatingText ScriptLink;
    private float LineLifetime;

    private void OnTriggerEnter(Collider other)
    {
        if (active)
        {
            if (QuestItemNeeded)
            {
                if(other.TryGetComponent<QuestItem>(out neededItem))
                {
                    if(neededItem.ItemName == ItemName)
                    {
                        if (restartable)
                        {
                            StartCoroutine(SpawnObjects());
                            AbortOthers();
                        }
                        else
                        {
                            if (!played)
                            {
                                played = true;
                                StartCoroutine(SpawnObjects());
                                AbortOthers();
                            }
                        }
                    }
                }
            }
            else
            {
                if (other.tag == "Player1" || other.tag == "Player2")
                {
                    if (restartable)
                    {
                        StartCoroutine(SpawnObjects());
                        AbortOthers();
                    }
                    else
                    {
                        if (!played)
                        {
                            played = true;
                            StartCoroutine(SpawnObjects());
                            AbortOthers();
                        }
                    }
                }
            }
        }
    }

    IEnumerator SpawnObjects()
    {
        foreach (GameObject TextPrefab in TextQueuePrefabs)
        {
            if (TextPrefab.TryGetComponent<FloatingText>(out ScriptLink))
            {
                TextPrefab.GetComponent<FloatingText>().TextOffset = this.TextOffset;
                TextPrefab.GetComponent<FloatingText>().RandomizeIntensity = this.RandomizeIntensity;
                Instantiate(TextPrefab, transform.position, Quaternion.identity);
                LineLifetime = ScriptLink.LifeTime;
            }

            yield return new WaitForSeconds(LineLifetime); // Wait before spawning the next object
        }

        foreach (DialogZone dialog in NextDialog)
        {
            if (dialog)
            {
                dialog.active = true;
            }
            else
            {
                Debug.Log("NoDialog");
            }
        }

        foreach(GameObject obj in ActivateObjects)
        {
            if (obj)
            {
                obj.SetActive(true);
            }            
        }

        foreach (GameObject obj in DeactivateObjects)
        {
            if (obj)
            {
                obj.SetActive(false);
            }            
        }

        if (addNextLevelScore)
        {
            FindAnyObjectByType<GameManager>().AddScore(1);
        }
    }

    private void AbortOthers()
    {
        foreach(DialogZone dialog in AbortDialogs)
        {
            if (dialog)
            {
                dialog.active = false;
            }
            else
            {
                Debug.Log("NoDialog");
            }
        }
    }
}
