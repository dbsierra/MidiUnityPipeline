using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class LightControlClip : PlayableAsset, ITimelineClipAsset
{
    public LightControlBehaviour template = new LightControlBehaviour ();

    public override double duration
    {
        get
        {
            return (double)10.0;
        }
    }

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
      //  Debug.Log("Create Playable");
        var playable = ScriptPlayable<LightControlBehaviour>.Create (graph, template);
        return playable;
    }
}
