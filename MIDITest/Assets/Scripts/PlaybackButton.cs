using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityMidi;

/// <summary>
/// 再生/停止を制御する
/// </summary>
public class PlaybackButton : MonoBehaviour
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
    [SerializeField] private MidiPlayer midiPlayer = default;
    /// <summary>
    /// ボタンテキスト
    /// </summary>
    [SerializeField] private Text text = default;

    /// <summary>
    /// 再生判定
    /// </summary>
    private bool isPlay = false;

    /// <summary>
    /// 再生時間・倍速の表示
    /// </summary>
    [SerializeField] private bool debugPlaybackTimAndDoubleSpd = false;

    /// <summary>
    /// 曲再生時間のベース時間
    /// </summary>
    private float baseTime = 0;

    /// <summary>
    /// リセットイベント
    /// </summary>
    private void Reset()
    {
        midiPlayer = GameObject.Find("MidiPlayer").GetComponent<MidiPlayer>();
        text = GetComponentInChildren<Text>();
    }

    /// <summary>
    /// オーディオボタンクリックイベント
    /// </summary>
    public void OnClickPlaybackButton()
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
        float startTime = Time.time, endTime = 0, playbackTime = 0;
        if (debugPlaybackTimAndDoubleSpd)
        {
            Debug.Log("Start time [" + startTime + "]");
        }

        // MIDIプレイヤが停止するまで1フレームずつ待つ
        while (midiPlayer.IsPlaying) { yield return null; }

        if (debugPlaybackTimAndDoubleSpd)
        {
            endTime = Time.time;
            playbackTime = endTime - startTime;
            if (baseTime <= 0) baseTime = playbackTime;
            Debug.Log("End time[" + endTime + "]  Play time [" + playbackTime + "]  <" + (Mathf.Floor((baseTime / playbackTime) * 1000) * 0.001) + "倍速>");
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
