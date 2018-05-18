using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class MidiEventMixerBehaviour : PlayableBehaviour
{

    public override void PrepareData(Playable playable, FrameData info)
    {
        base.PrepareData(playable, info);

        Debug.Log("Prepare Data");
    }

    // NOTE: This function is called at runtime and edit time.  Keep that in mind when setting the values of properties.
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        MidiEventReceiver trackBinding = playerData as MidiEventReceiver;

        if (!trackBinding)
            return;

        int inputCount = playable.GetInputCount ();

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            ScriptPlayable<MidiEventBehaviour> inputPlayable = (ScriptPlayable<MidiEventBehaviour>)playable.GetInput(i);
            MidiEventBehaviour input = inputPlayable.GetBehaviour ();
            
            // Use the above variables to process each frame of this playable.
            
        }
    }
}
