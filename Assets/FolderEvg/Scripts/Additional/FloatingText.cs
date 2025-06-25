using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    [SerializeField] public bool LastLine = false;
    [SerializeField] public float LifeTime = 3f;
    [SerializeField] public Vector3 TextOffset = new Vector3(0,0,0);
    [SerializeField] public Vector3 RandomizeIntensity = new Vector3(0,0,0);

    private Animator animator;
    private DialogMenu dialogMenu;

    private void Start()
    {
        // Найти локальный TMP в себе или дочерних объектах
        TextMeshPro localTMP = GetComponentInChildren<TextMeshPro>();
        if (localTMP == null)
        {
            Debug.LogWarning("FloatingText: не найден локальный TextMeshPro!");
            return;
        }

        string message = localTMP.text;
        localTMP.text = "...";

        // Найти объект с компонентом DialogMenu на сцене
        dialogMenu = FindObjectOfType<DialogMenu>();
        if (dialogMenu == null)
        {
            Debug.LogWarning("FloatingText: не найден объект с DialogMenu на сцене!");
            return;
        }
        dialogMenu.WakeUp();

        dialogMenu.dialogTMP.text = message;
        /*
        // Найти TextMeshProUGUI (UI-версия текста)
        TextMeshProUGUI dialogText = dialogMenu.GetComponentInChildren<TextMeshProUGUI>();
        if (dialogText == null)
        {
            Debug.LogWarning("FloatingText: не найден TextMeshProUGUI в DialogMenu!");
            return;
        }

        // Перенести текст
        dialogText.text = message;*/

        gameObject.TryGetComponent<Animator>(out animator);
        transform.localPosition += TextOffset;

        transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
            Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y),
            Random.Range(-RandomizeIntensity.z, RandomizeIntensity.z));

        Destroy(gameObject, LifeTime);

        if(animator != null)
        {
            Invoke(nameof(Close), LifeTime-1f);
        }
    }

    private void OnDestroy()
    {
        if (LastLine)
        {
            if (dialogMenu == null)
            {
                Debug.LogWarning("No dialogMenu to turn off!");
                return;
            }
            else
            {
                dialogMenu.Sleep();
            }
        }
    }

    private void Close()
    {
        animator.SetTrigger("Close");
    }
}
