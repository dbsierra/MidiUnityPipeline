using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MidiPlayDebug : MonoBehaviour
{

    Animator AC;

    public Color color;

    public Light pointLight;

    public void Init()
    {
        AC = GetComponent<Animator>();
    }

    public void Update()
    {
        pointLight.color = color;
        GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", color);
    }

    public void Play()
    {
        if(AC != null)
        {
            AC.SetTrigger("Pulse");
        }
    }

}
