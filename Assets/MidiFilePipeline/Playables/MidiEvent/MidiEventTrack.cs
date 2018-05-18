using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.2257476f, 0.3470083f, 0.5377358f)]
[TrackClipType(typeof(MidiEventClip))]
[TrackBindingType(typeof(MidiEventReceiver))]
public class MidiEventTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<MidiEventMixerBehaviour>.Create (graph, inputCount);
    }
}
