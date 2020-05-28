using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SamplingRateDropdown : MonoBehaviour
{
    [SerializeField] private Dropdown rateDropdown = default;
    [SerializeField] private UnityMidi.MidiPlayer midiPlayer = default;

    private int[] rate = new int[] { 8000, 16000, 32000, 44100, 48000, 96000 };
    private string[] annotation = new string[] { "公衆電話", "高音質IP電話", "高音質IP電話", "CD", "DVD", "Blu-ray Disc" };

    private void Reset()
    {
        midiPlayer = GameObject.Find("MidiPlayer").GetComponent<UnityMidi.MidiPlayer>();

        rateDropdown = GetComponent<Dropdown>();
        rateDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < rate.Length; i++)
            options.Add(rate[i].ToString().PadRight(7, ' ') + annotation[i].ToString());

        rateDropdown.AddOptions(options);
    }

    public void SelectedRateDropdown(Dropdown input)
    {
        midiPlayer.sampleRate = rate[input.value];
        midiPlayer.ResetSynthesizer();
    }
}
