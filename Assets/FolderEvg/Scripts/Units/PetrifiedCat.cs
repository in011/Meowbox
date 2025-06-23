using UnityEngine;
using System.Collections;

public class PetrifiedCat : BaseBlock
{
    [SerializeField] float petrificationResetTime = 3f;
    [SerializeField] int HP = 3;
    public bool player2 = true;
    private AbilityManager abilityManager;

    public Vector3 targetScale = new Vector3(1.2f, 1.2f, 1.2f); // до какого размера увеличивать
    public float time = 0.5f; // длительность увеличени€ и уменьшени€

    void Start()
    {
        abilityManager = FindAnyObjectByType<AbilityManager>();
        gameObject.GetComponent<Rigidbody>().linearDamping = 0;
    }

    public override void Respawn()
    {
        gameObject.GetComponent<Rigidbody>().linearDamping = 10;
        Invoke(nameof(ReTurn), petrificationResetTime);
    }
    private void ReTurn()
    {
        gameObject.GetComponent<Rigidbody>().linearDamping = 0;
        abilityManager.Cat2Restore(gameObject.transform.position + new Vector3(0f, 3f, 0f), gameObject.transform.rotation);
        Destroy(gameObject);
    }
    public void GetHit(int damage)
    {
        HP -= damage;
        StartScaling();

        if (HP < 1)
        {
            abilityManager.BrakePetrification();
        }
    }

    public void StartScaling()
    {
        StartCoroutine(ScaleUpAndDown());
    }

    private IEnumerator ScaleUpAndDown()
    {
        Vector3 originalScale = transform.localScale;

        // ”величение
        float elapsed = 0f;
        while (elapsed < time)
        {
            Debug.Log(transform.localScale);

            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsed / time);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;

        // ”меньшение
        elapsed = 0f;
        while (elapsed < time)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, elapsed / time);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;
    }
}
