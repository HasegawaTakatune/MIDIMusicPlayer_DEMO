using UnityEngine;
using UnityEngine.UI;
using UnityMidi;
using AudioSynthesis.Sequencer;

/// <summary>
/// テンポ変更を制御する
/// </summary>
public class TempoChanger : MonoBehaviour
{
    /// <summary>
    /// テンポ（テキスト）
    /// </summary>
    private const string TEMPO = "Tempo\n";

    /// <summary>
    /// MIDIプレイヤ
    /// </summary>
    [SerializeField] MidiPlayer midiPlayer = default;
    /// <summary>
    /// テンポテキスト
    /// </summary>
    [SerializeField] Text tempoText = default;
    /// <summary>
    /// シーケンサ
    /// </summary>
    private MidiFileSequencer sequencer;

    /// <summary>
    /// リセットイベント
    /// </summary>
    private void Reset()
    {
        midiPlayer = GameObject.Find("MidiPlayer").GetComponent<MidiPlayer>();
        tempoText = transform.Find("Tempo").Find("TempoText").GetComponent<Text>();
    }

    /// <summary>
    /// 初期イベント
    /// </summary>
    private void Start()
    {
        sequencer = midiPlayer.Sequencer;

        tempoText.text = TEMPO + sequencer.PlaySpeed.ToString();
    }

    /// <summary>
    /// アップテンポボタン
    /// </summary>
    public void OnClickUpTempo()
    {
        double value = midiPlayer.Sequencer.PlaySpeed;

        if (value < 1) value = 1;
        else value += 0.1d;
        midiPlayer.Sequencer.PlaySpeed = value;

        tempoText.text = TEMPO + midiPlayer.Sequencer.PlaySpeed.ToString();
    }

    /// <summary>
    /// ダウンテンポボタン
    /// </summary>
    public void OnClickDownTempo()
    {
        double value = midiPlayer.Sequencer.PlaySpeed;

        value -= 0.1d;
        midiPlayer.Sequencer.PlaySpeed = value;

        tempoText.text = TEMPO + midiPlayer.Sequencer.PlaySpeed.ToString();
    }
}
