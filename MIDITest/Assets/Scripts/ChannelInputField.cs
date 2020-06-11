using UnityEngine;
using UnityEngine.UI;
using UnityMidi;

/// <summary>
/// チャネル設定を制御する
/// </summary>
public class ChannelInputField : MonoBehaviour
{
    /// <summary>
    /// チャネル最大値
    /// </summary>
    private const int MAX_CHANNEL = 15;
    /// <summary>
    /// チャネル最小値
    /// </summary>
    private const int MIN_CHANNEL = 0;

    /// <summary>
    /// チャネル入力項目
    /// </summary>
    [SerializeField] private InputField channelInputField = default;
    /// <summary>
    /// MIDIプレイヤ
    /// </summary>
    [SerializeField] private MidiPlayer midiPlayer = default;

    /// <summary>
    /// リセットイベント
    /// </summary>
    private void Reset()
    {
        midiPlayer = GameObject.Find("MidiPlayer").GetComponent<MidiPlayer>();
        channelInputField = GetComponent<InputField>();
    }

    /// <summary>
    /// チャネル変更イベント
    /// </summary>
    /// <param name="input"></param>
    public void OnClickChannelChanged(InputField input)
    {
        int value = System.Convert.ToInt32(input.text);

        // 最大値判定
        if (MAX_CHANNEL < value)
        {
            value = MAX_CHANNEL;
            input.text = MAX_CHANNEL.ToString();
        }

        // 最小値判定
        if (value < MIN_CHANNEL)
        {
            value = MIN_CHANNEL;
            input.text = MIN_CHANNEL.ToString();
        }

        midiPlayer.channel = value;
    }
}
