using UnityEngine;
using UnityEngine.UIElements;

public class BossHorn : MonoBehaviour
{
    [Header("Hint")]
    private HintPause hint;
    [SerializeField] private string hintHorn;
    private bool secondHint = false;

    private void Start()
    {
        this.hint = FindFirstObjectByType<GameManager>().hint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1" || other.tag == "Player2")
        {
            if (!secondHint)
            {
                Invoke(nameof(CallHint), 0f);
                secondHint = true;
                Debug.Log("HINT");
            }
            other.GetComponent<Moving>().hasDashAbility = true;
            Destroy(gameObject);
        }
    }

    private void CallHint()
    {
        hint.TextField = hintHorn;
        hint.Pause();
        Debug.Log("HINT PRINT");
    }
}
