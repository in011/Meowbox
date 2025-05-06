using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SubtitleTrackMixer : PlayableBehaviour
{
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        TextMeshProUGUI text = playerData as TextMeshProUGUI;
        if (!text) return;

        Image background = text.GetComponentInParent<Image>();
        string currentText = "";
        bool hasActiveClip = false;

        // Стили по умолчанию (если нет активного клипа)
        TMP_FontAsset currentFont = text.font;
        FontStyles currentStyle = FontStyles.Normal;
        Color currentColor = Color.white;
        bool currentUnderline = false;
        float currentFontSize = 0f;

        int inputCount = playable.GetInputCount();
        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            if (inputWeight > 0f)
            {
                ScriptPlayable<SubtitleBehaviour> inputPlayable = (ScriptPlayable<SubtitleBehaviour>)playable.GetInput(i);
                SubtitleBehaviour input = inputPlayable.GetBehaviour();

                currentText = input.subtitleText;
                currentFont = input.fontAsset ?? currentFont; // Если шрифт не задан, оставляем текущий
                currentStyle = input.fontStyle;
                currentColor = input.textColor;
                currentUnderline = input.underline;
                currentFontSize = input.fontSize;
                hasActiveClip = true;
            }
        }

        // Применяем стили
        text.text = currentText;
        text.font = currentFont;
        text.fontStyle = currentUnderline ? currentStyle | FontStyles.Underline : currentStyle; // Добавляем подчёркивание, если нужно
        text.color = new Color(currentColor.r, currentColor.g, currentColor.b, info.weight); // Альфа-канал зависит от веса

        if (currentFontSize > 0f)
        {
            text.fontSize = currentFontSize;
        }

        if (background != null)
        {
            background.enabled = hasActiveClip;
            text.enabled = hasActiveClip;

            if (hasActiveClip)
            {
                Canvas.ForceUpdateCanvases();
                Vector2 textSize = text.GetPreferredValues(currentText);
                float paddingX = 40f;
                float paddingY = 20f;

                background.rectTransform.sizeDelta = new Vector2(
                    Mathf.Max(120f, textSize.x + paddingX),
                    Mathf.Max(60f, textSize.y + paddingY)
                );

                text.rectTransform.anchoredPosition = Vector2.zero;
                text.alignment = TextAlignmentOptions.Center;
            }
        }
    }
}