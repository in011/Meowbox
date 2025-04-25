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

        int inputCount = playable.GetInputCount();
        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            if (inputWeight > 0f)
            {
                ScriptPlayable<SubtitleBehaviour> inputPlayable = (ScriptPlayable<SubtitleBehaviour>)playable.GetInput(i);
                SubtitleBehaviour input = inputPlayable.GetBehaviour();
                currentText = input.subtitleText;
                hasActiveClip = true;
            }
        }

        text.text = currentText;
        text.color = new Color(1, 1, 1, info.weight);

        if (background != null)
        {
            background.enabled = hasActiveClip;
            text.enabled = hasActiveClip;

            if (hasActiveClip)
            {
                Canvas.ForceUpdateCanvases();
                Vector2 textSize = text.GetPreferredValues(currentText);
                float paddingX = 40f; // Увеличенные отступы по ширине
                float paddingY = 20f; // Увеличенные отступы по высоте

                // Размер подложки (с минимальными значениями)
                background.rectTransform.sizeDelta = new Vector2(
                    Mathf.Max(120f, textSize.x + paddingX),
                    Mathf.Max(60f, textSize.y + paddingY)
                );

                // Жёсткая фиксация центра текста
                text.rectTransform.anchoredPosition = Vector2.zero;
                text.alignment = TextAlignmentOptions.Center; // Выравнивание по центру
            }
        }
    }
}