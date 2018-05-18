using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class LightControlBehaviour : PlayableBehaviour
{
    public Color color = Color.white;
    public float intensity = 1f;
    public float bounceIntensity = 1f;
    public float range = 10f;

    public override void OnGraphStart(Playable playable)
    {
        base.OnGraphStart(playable);
      //  Debug.Log("Start");
    }
    public override void OnGraphStop(Playable playable)
    {
        base.OnGraphStart(playable);
     //   Debug.Log("Stop");
    }

}
