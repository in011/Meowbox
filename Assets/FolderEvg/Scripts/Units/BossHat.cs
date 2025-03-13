using UnityEngine;

public class BossHat : MonoBehaviour
{
    [Header("Hint")]
    [SerializeField] private HintPause hint;
    [SerializeField] private string hintHat;
    private string currentHint;
    private bool secondHint = false;
    [SerializeField] private string hintGreed;
    private bool secondHintGreed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1" || other.tag == "Player2")
        {
            if (other.GetComponent<Moving>().hasDashAbility)
            {
                hint = FindAnyObjectByType<HintPause>();
                if (!secondHintGreed)
                {
                    currentHint = hintGreed;
                    Invoke(nameof(CallHint), 0.1f);
                    secondHintGreed = true;
                }
            }
            else
            {
                if (!secondHint)
                {
                    currentHint = hintHat;
                    Invoke(nameof(CallHint), 0.1f);
                    secondHint = true;
                }

                other.GetComponent<Petrification>().hasPetrificationAbility = true;
                Destroy(gameObject);
            }
        }
    }
    private void CallHint()
    {
        hint.TextField = currentHint;
        hint.Pause();
    }
}
