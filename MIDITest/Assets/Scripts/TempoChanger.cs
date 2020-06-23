using UnityEngine;
using UnityEngine.UI;
using UnityMidi;

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
        //tempoText.text = TEMPO + sequencer.PlaySpeed.ToString();
        tempoText.text = TEMPO + midiPlayer.Sequencer.PlaySpeed.ToString();
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

        //double value = midiPlayer.Sequencer.BPM;

        //if (value < 300) value++;
        //midiPlayer.Sequencer.BPM = value;

        //tempoText.text = TEMPO + midiPlayer.Sequencer.BPM.ToString();
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

        //double value = midiPlayer.Sequencer.BPM;

        //if (30 < value) value--;
        //midiPlayer.Sequencer.BPM = value;

        //tempoText.text = TEMPO + midiPlayer.Sequencer.BPM.ToString();
    }
}
