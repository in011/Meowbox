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
        // ����� ��������� TMP � ���� ��� �������� ��������
        TextMeshPro localTMP = GetComponentInChildren<TextMeshPro>();
        if (localTMP == null)
        {
            Debug.LogWarning("FloatingText: �� ������ ��������� TextMeshPro!");
            return;
        }

        string message = localTMP.text;
        localTMP.text = "...";

        // ����� ������ � ����������� DialogMenu �� �����
        dialogMenu = FindObjectOfType<DialogMenu>();
        if (dialogMenu == null)
        {
            Debug.LogWarning("FloatingText: �� ������ ������ � DialogMenu �� �����!");
            return;
        }
        dialogMenu.WakeUp();

        dialogMenu.dialogTMP.text = message;
        /*
        // ����� TextMeshProUGUI (UI-������ ������)
        TextMeshProUGUI dialogText = dialogMenu.GetComponentInChildren<TextMeshProUGUI>();
        if (dialogText == null)
        {
            Debug.LogWarning("FloatingText: �� ������ TextMeshProUGUI � DialogMenu!");
            return;
        }

        // ��������� �����
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
