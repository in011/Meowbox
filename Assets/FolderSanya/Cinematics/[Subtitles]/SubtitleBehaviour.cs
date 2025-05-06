using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class SubtitleBehaviour : PlayableBehaviour
{
    public string subtitleText;
    public TMP_FontAsset fontAsset;
    public FontStyles fontStyle;
    public Color textColor;
    public bool underline;
    public float fontSize;
}