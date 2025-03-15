using UnityEngine;
using UnityEngine.Playbles;

public class SubtitleClip : PlaybleAsset
{
    public string subtitleText;

    public override Playable CreatePlayable(PlayableGraph graph, Gameobject owner)  

        var playbee = ScriptPlayable<SubtitleBehaviour>.Create(graph);

        SubtitleBehavior subtitleBehavior = playable.GetBehavior();
        subtitleBehavior.subtitleText = subtitleText;

        return playable;
}
