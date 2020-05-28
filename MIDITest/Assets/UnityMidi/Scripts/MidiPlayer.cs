using UnityEngine;
using System.IO;
using System.Collections;
using AudioSynthesis;
using AudioSynthesis.Bank;
using AudioSynthesis.Synthesis;
using AudioSynthesis.Sequencer;
using AudioSynthesis.Midi;
using System;

namespace UnityMidi
{
    [RequireComponent(typeof(AudioSource))]
    public class MidiPlayer : MonoBehaviour
    {
        [SerializeField] Log log = default;

        [SerializeField] private StreamingAssetResouce bankSource;
        [SerializeField] public StreamingAssetResouce midiSource;
        [SerializeField] private bool loadOnAwake = true;
        [SerializeField] private bool playOnAwake = true;
        [SerializeField] public int channel = 1;
        [SerializeField] public int sampleRate = 44100;
        [SerializeField] private int bufferSize = 1024;
        PatchBank bank;
        MidiFile midi;
        Synthesizer synthesizer;
        AudioSource audioSource;
        MidiFileSequencer sequencer;
        int bufferHead;
        float[] currentBuffer;

        public AudioSource AudioSource { get { return audioSource; } }

        public MidiFileSequencer Sequencer { get { return sequencer; } }

        public PatchBank Bank { get { return bank; } }

        public MidiFile MidiFile { get { return midi; } }

        public void Awake()
        {
            synthesizer = new Synthesizer(sampleRate, channel, bufferSize, 1);
            sequencer = new MidiFileSequencer(synthesizer);
            audioSource = GetComponent<AudioSource>();

            if (loadOnAwake)
            {
                try
                {
                    LoadBank(new PatchBank(bankSource));
                }
                catch (Exception e)
                {
                    log.ShowLog(e.ToString() + "\n\n" + e.Message);
                }
                LoadMidi(new MidiFile(midiSource));
            }

            if (playOnAwake)
            {
                Play();
            }
        }

        public void LoadBank(PatchBank bank)
        {
            this.bank = bank;
            synthesizer.UnloadBank();
            synthesizer.LoadBank(bank);
        }

        public void LoadMidi(MidiFile midi)
        {
            this.midi = midi;
            sequencer.Stop();
            sequencer.UnloadMidi();
            sequencer.LoadMidi(midi);
        }

        public void Play()
        {
            sequencer.Play();
            audioSource.Play();
        }

        public void Stop()
        {
            sequencer.Stop();
            audioSource.Stop();
        }

        public bool IsPlaying
        {
            get { return sequencer.IsPlaying && audioSource.isPlaying; }
        }

        void OnAudioFilterRead(float[] data, int channel)
        {
            Debug.Assert(this.channel == channel);
            int count = 0;
            while (count < data.Length)
            {
                if (currentBuffer == null || bufferHead >= currentBuffer.Length)
                {
                    sequencer.FillMidiEventQueue();
                    synthesizer.GetNext();
                    currentBuffer = synthesizer.WorkingBuffer;
                    bufferHead = 0;
                }
                var length = Mathf.Min(currentBuffer.Length - bufferHead, data.Length - count);
                System.Array.Copy(currentBuffer, bufferHead, data, count, length);
                bufferHead += length;
                count += length;
            }
        }

        public void ResetSynthesizer()
        {
            sequencer.Stop();
            synthesizer = new Synthesizer(sampleRate, channel, bufferSize, 1);
            sequencer = new MidiFileSequencer(synthesizer);
            LoadBank(new PatchBank(bankSource));
            LoadMidi(new MidiFile(midiSource));
        }
    }
}
