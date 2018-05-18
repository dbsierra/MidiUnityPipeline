using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using SmfLite;

[Serializable]
public class MidiEventBehaviour : PlayableBehaviour
{
    public TextAsset MidiFile;
    public int Bpm;
    public PlayableDirector Director;

    #region private variables
    MidiFileContainer m_song;
    MidiTrackSequencer m_seq;
    bool m_FirstFrameHappened;
    MidiEventReceiver m_midiEventReceiver;
    double m_lastTime;
    PlayState m_previousState = PlayState.Paused;
    #endregion

    public override void OnPlayableCreate(Playable playable)
    {

    }

    public override void OnGraphStart(Playable playable)
    {
        Debug.Log("Init " + playable.GetTime());
       // m_previousState = Director.state;
        LoadSong();
        LoadSequencer();
    }

    public override void OnGraphStop(Playable playable)
    {
        Debug.Log("Deinit " + playable.GetTime());
        m_FirstFrameHappened = false;
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        Debug.Log("Play " + playable.GetTime());
        m_previousState = PlayState.Paused;
        m_lastTime = playable.GetTime();
    }
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        m_previousState = PlayState.Paused;
        m_lastTime = playable.GetTime();
    }

    private void LoadSong()
    {
        if (MidiFile != null)
        {
            m_song = MidiFileLoader.Load(MidiFile.bytes);
        }
    }

    private void LoadSequencer()
    {
        if (m_song.tracks != null)
        {
            m_seq = new MidiTrackSequencer(m_song.tracks[0], m_song.division, Bpm);
        }
    }

    private void Start(float time)
    {

    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        double time = playable.GetTime();

        MidiEventReceiver trackBinding = playerData as MidiEventReceiver;

        if (!trackBinding)
            return;

        m_midiEventReceiver = trackBinding;

        //  Debug.Log( playable.GetGraph().GetRootPlayable(0).GetPlayState() );

       // Debug.Log(m_previousState);

        if (Director.state == PlayState.Paused)
        {
            m_lastTime = playable.GetTime();
           // DispatchEvents(m_seq.Start((float)playable.GetTime()));
        }
        // On play
        if(Director.state == PlayState.Playing && m_previousState == PlayState.Paused)
        {
            if (m_seq != null)
            {
                Debug.Log("Start it " + m_lastTime);
                LoadSong();
                LoadSequencer();
                DispatchEvents(m_seq.Start((float)m_lastTime));
            }
        }
        m_previousState = Director.state;


        if (!m_FirstFrameHappened)
        { 
            m_FirstFrameHappened = true;
        }


        if (m_seq != null && m_seq.Playing)
        {
            // Update the sequencer and dispatch incoming events.

            DispatchEvents(m_seq.Advance(info.deltaTime));
        }
    }

    private void DispatchEvents(List<MidiEvent> events)
    {
        if (events != null)
        {
            List<byte> noteOns = new List<byte>();

            foreach (var e in events)
            {
                if ((e.status & 0xf0) == 0x90)
                {
                    noteOns.Add(e.data1);

                    if (e.data1 == 0x3C)
                    {
                       // Debug.Log("Kick");
                    }
                    if (e.data1 == 0x3E)
                    {
                       // Debug.Log("Snare");
                    }
                    if (e.data1 == 0x40)
                    {
                        // Hat.Play();
                    }
                }
            }

            if(m_midiEventReceiver != null)
            {
                m_midiEventReceiver.ReceiveNotes(noteOns);
            }

        }
    }

}
