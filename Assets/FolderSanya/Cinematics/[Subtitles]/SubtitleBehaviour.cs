using TMPro;
using UnityEngine;

public class SubtitleBehaviour : PlayableBehaviour
{
    public string subtitleText;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)

       TextMeshProUGUI text = playerData as TextMeshProUGUI
       text.text = subtitleText
       text.color = new color (1, 1, 1, info.weight);
}
