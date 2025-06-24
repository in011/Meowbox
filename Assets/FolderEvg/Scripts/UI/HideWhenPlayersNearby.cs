using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HideWhenPlayersNearby : MonoBehaviour
{
    private Transform player1;
    private Transform player2;
    public float detectionRadius = 5f;

    private SpriteRenderer[] spriteRenderers;

    public float fadeSpeed = 2f;
    private bool shouldBeVisible = true;
    private float targetAlpha = 1f;

    void Start()
    {
        GameManager manager = FindAnyObjectByType<GameManager>();
        player1 = manager.player1.transform;
        player2 = manager.player2.transform;

        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        if (spriteRenderers.Length == 0)
        {
            Debug.LogWarning("Нет SpriteRenderer'ов среди потомков объекта " + gameObject.name);
        }
    }

    void Update()
    {
        bool player1Near = Vector3.Distance(transform.position, player1.position) <= detectionRadius;
        bool player2Near = Vector3.Distance(transform.position, player2.position) <= detectionRadius;

        //bool shouldBeHidden = player1Near || player2Near;
        shouldBeVisible = player1Near || player2Near;

        /*foreach (var sr in spriteRenderers)
        {
            //sr.enabled = !shouldBeHidden;
            sr.enabled = shouldBeVisible;
        }*/

        targetAlpha = !shouldBeVisible ? 1f : 0f;

        // Постепенно меняем альфа
        foreach (var sr in spriteRenderers)
        {
            Color c = sr.color;
            c.a = Mathf.MoveTowards(c.a, targetAlpha, fadeSpeed * Time.deltaTime);
            sr.color = c;
        }
    }
}
