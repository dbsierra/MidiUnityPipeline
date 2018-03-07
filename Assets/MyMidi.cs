using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using SmfLite;

public class MyMidi : MonoBehaviour {

    public int bpm = 95;

    // Source MIDI file asset.
    public TextAsset sourceFile;

    MidiFileContainer song;
    MidiTrackSequencer seq;

    IEnumerator Start()
    {
        // Load the MIDI song.
        song = MidiFileLoader.Load(sourceFile.bytes);

        // Wait for one second to avoid stuttering.
        yield return new WaitForSeconds(1.0f);

        // Start sequencing.
        ResetAndPlay(0);
    }

    // Reset and start sequecing.
    void ResetAndPlay(float startTime)
    {
        // Start the sequencer and dispatch events at the beginning of the track.
        seq = new MidiTrackSequencer(song.tracks[0], song.division, bpm);
        DispatchEvents(seq.Start(startTime));
    }

    // Update function (MonoBehaviour).
    void Update()
    {
        if (seq != null && seq.Playing)
        {
            // Update the sequencer and dispatch incoming events.
            DispatchEvents(seq.Advance(Time.deltaTime));
        }
    }

    /*
      C1 = 0x24 36
      
      
     */
    // Dispatch incoming MIDI events. 
    void DispatchEvents(List<MidiEvent> events)
    {
        if (events != null)
        {
            foreach (var e in events)
            {

                // Note on?
                if ((e.status & 0xf0) == 0x90)
                {
                    Debug.Log(e.data1 == (byte)36);
                }
                /*
                if ((e.status & 0xf0) == 0x90) {
                    // C2
                    if (e.data1 == 0x24) {
                        GameObject.Find ("Kick").SendMessage ("OnNoteOn");
                    } else if (e.data1 == 0x2a) {
                        GameObject.Find ("Hat").SendMessage ("OnNoteOn");
                    } else if (e.data1 == 0x2e) {
                        GameObject.Find ("OHat").SendMessage ("OnNoteOn");
                    } else if (e.data1 == 0x26 || e.data1 == 0x27 || e.data1 == 0x28) {
                        GameObject.Find ("Snare").SendMessage ("OnNoteOn");
                    }
                }
                */
            }
        }
    }
}
