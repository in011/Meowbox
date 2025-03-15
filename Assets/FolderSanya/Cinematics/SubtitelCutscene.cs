using UnityEngine;
using TMPro;
using System.Collections;
using System.Globalization;

public class SubtitleManager : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;
    private Coroutine subtitleCoroutine;

    void Start()
    {
        subtitleText.text = "";
        subtitleText.color = new Color(subtitleText.color.r, subtitleText.color.g, subtitleText.color.b, 0);
    }

    public void ShowSubtitle(string subtitleData)
    {
        string[] parts = subtitleData.Split('|');
        string text = parts[0];
        float duration = 3f; // Значение по умолчанию

        if (parts.Length > 1)
        {
            if (!float.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out duration))
            {
                Debug.LogError($"Ошибка: не удалось преобразовать '{parts[1]}' в число!");
                return;
            }
        }

        if (subtitleCoroutine != null)
        {
            StopCoroutine(subtitleCoroutine);
        }

        subtitleCoroutine = StartCoroutine(DisplaySubtitle(text, duration));
    }

    IEnumerator DisplaySubtitle(string text, float duration)
    {
        subtitleText.text = text;
        subtitleText.color = new Color(subtitleText.color.r, subtitleText.color.g, subtitleText.color.b, 1);
        yield return new WaitForSeconds(duration);
        subtitleText.color = new Color(subtitleText.color.r, subtitleText.color.g, subtitleText.color.b, 0);
        subtitleText.text = "";
    }
}
