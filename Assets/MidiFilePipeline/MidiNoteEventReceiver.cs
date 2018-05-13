using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidiNoteEventReceiver : MonoBehaviour {


    public MidiPlayDebug Kick;
    public MidiPlayDebug Snare;
    public MidiPlayDebug Hat;

    // Use this for initialization
    void Awake () {
        Kick.Init();
        Snare.Init();
        Hat.Init();
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    public void ReceiveNotes(List<byte> list)
    {
        foreach (int i in list)
        {
            if (i == 60)
            {
                Kick.Play();
            }
            if (i == 0x3E)
            {
                Snare.Play();
            }
            if (i == 0x40)
            {
                Hat.Play();
            }
        }
    }


}
