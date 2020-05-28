using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// サンプリングレートの選択を制御する
/// </summary>
public class SamplingRateDropdown : MonoBehaviour
{
    /// <summary>
    /// レート選択ドロップダウン
    /// </summary>
    [SerializeField] private Dropdown rateDropdown = default;
    /// <summary>
    /// MIDIプレイヤ
    /// </summary>
    [SerializeField] private UnityMidi.MidiPlayer midiPlayer = default;

    /// <summary>
    /// レート配列
    /// </summary>
    private int[] rate = new int[] { 8000, 16000, 32000, 44100, 48000, 96000 };
    /// <summary>
    /// レート別の例配列
    /// </summary>
    private string[] annotation = new string[] { "公衆電話", "高音質IP電話", "高音質IP電話", "CD", "DVD", "Blu-ray Disc" };

    /// <summary>
    /// リセットイベント
    /// </summary>
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

    /// <summary>
    /// レート選択イベント
    /// </summary>
    /// <param name="input"></param>
    public void SelectedRateDropdown(Dropdown input)
    {
        midiPlayer.sampleRate = rate[input.value];
        midiPlayer.ResetSynthesizer();
    }
}
