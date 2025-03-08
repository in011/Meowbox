using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] public float LifeTime = 3f;
    [SerializeField] public Vector3 TextOffset = new Vector3(0,0,0);
    [SerializeField] public Vector3 RandomizeIntensity = new Vector3(0,0,0);

    private Animator animator;

    private void Start()
    {
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
    private void Close()
    {
        animator.SetTrigger("Close");
    }
}
