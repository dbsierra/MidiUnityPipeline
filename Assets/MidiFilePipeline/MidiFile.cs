using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmfLite;

public class MidiFile : MonoBehaviour
{

    [SerializeField]
    private int m_bpm;
    [SerializeField]
    private TextAsset m_midiFile;

    private MidiFileContainer m_song;
    public MidiFileContainer Song { get { return m_song; } }
    public int BPM { get { return m_bpm; } }

    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    public void Init()
    {
        m_song = MidiFileLoader.Load(m_midiFile.bytes);
    }
}
