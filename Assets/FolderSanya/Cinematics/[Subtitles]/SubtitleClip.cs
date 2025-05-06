using UnityEngine;
using UnityEngine.Playables;
using TMPro;

public class SubtitleClip : PlayableAsset
{
    [TextArea] public string subtitleText;
    public TMP_FontAsset fontAsset;
    public FontStyles fontStyle = FontStyles.Normal; // Жирный, курсив и т. д.
    public Color textColor = Color.white; // Цвет текста
    public bool underline = false; // Подчёркивание
    public float fontSize = 0f; // 0 = авторазмер (если нужно переопределить)

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<SubtitleBehaviour>.Create(graph);
        SubtitleBehaviour subtitleBehavior = playable.GetBehaviour();

        subtitleBehavior.subtitleText = subtitleText;
        subtitleBehavior.fontAsset = fontAsset;
        subtitleBehavior.fontStyle = fontStyle;
        subtitleBehavior.textColor = textColor;
        subtitleBehavior.underline = underline;
        subtitleBehavior.fontSize = fontSize;

        return playable;
    }
}