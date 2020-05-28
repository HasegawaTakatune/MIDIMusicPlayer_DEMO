using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 再生/停止を制御する
/// </summary>
public class AudioButton : MonoBehaviour
{
    /// <summary>
    /// 再生
    /// </summary>
    private const string PLAY = "▶";
    /// <summary>
    /// 停止
    /// </summary>
    private const string STOP = "■";

    /// <summary>
    /// MIDIプレイヤ
    /// </summary>
    [SerializeField] private UnityMidi.MidiPlayer midiPlayer = default;
    /// <summary>
    /// ボタンテキスト
    /// </summary>
    [SerializeField] private Text text = default;

    /// <summary>
    /// 再生判定
    /// </summary>
    private bool isPlay = false;

    /// <summary>
    /// リセットイベント
    /// </summary>
    private void Reset()
    {
        midiPlayer = GameObject.Find("MidiPlayer").GetComponent<UnityMidi.MidiPlayer>();
        text = GetComponentInChildren<Text>();
    }

    /// <summary>
    /// オーディオボタンクリックイベント
    /// </summary>
    public void OnClickAudioButton()
    {
        isPlay = !isPlay;
        if (isPlay)
        {
            // 再生
            midiPlayer.Play();
            text.text = STOP;

            StartCoroutine(IsPlaying());
        }
        else
        {
            // 停止
            midiPlayer.Stop();
            text.text = PLAY;
        }
    }

    /// <summary>
    /// 再生中かを判定してボタン表示を変更する
    /// </summary>
    /// <returns></returns>
    public IEnumerator IsPlaying()
    {
        // MIDIプレイヤが停止するまで1フレームずつ待つ
        while (midiPlayer.IsPlaying)
        {
            yield return null;
        }

        // 既に停止していたら処理を抜ける
        if (!isPlay)
            yield break;

        // 停止処理
        isPlay = !isPlay;
        midiPlayer.Stop();
        text.text = PLAY;
    }
}
