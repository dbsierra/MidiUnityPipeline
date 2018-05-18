using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MidiEventClip : PlayableAsset, ITimelineClipAsset
{
    public MidiEventBehaviour template = new MidiEventBehaviour ();
    public ExposedReference<TextAsset> MidiFile;
    public int BPM;
    public int NumberOfBeats;

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override double duration
    {
        get
        {
            // Get the length of this midi clip based on number of beats (different from the last midi event)
            return (float)NumberOfBeats * 60.0f/(float)BPM;
        }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<MidiEventBehaviour>.Create (graph, template);
        MidiEventBehaviour clone = playable.GetBehaviour ();
        clone.MidiFile = MidiFile.Resolve (graph.GetResolver ());
        clone.Bpm = BPM;
        clone.Director = owner.GetComponent<PlayableDirector>();
        return playable;
    }
}
