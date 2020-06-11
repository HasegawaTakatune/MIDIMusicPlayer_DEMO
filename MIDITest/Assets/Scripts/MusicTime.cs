using UnityEngine;
using UnityEngine.UI;
using AudioSynthesis.Sequencer;
using UnityMidi;

/// <summary>
/// 再生中時間/終了時間の表示
/// </summary>
public class MusicTime : MonoBehaviour
{
    /// <summary>
    /// ミリ秒
    /// </summary>
    private const float ms = 0.00001f;

    /// <summary>
    /// MIDIプレイヤ
    /// </summary>
    [SerializeField] MidiPlayer MidiPlayer = default;
    /// <summary>
    /// 再生時間表示
    /// </summary>
    [SerializeField] Text PlayingTimeText = default;
    /// <summary>
    /// 終了時間表示
    /// </summary>
    [SerializeField] Text EndTimeText = default;

    /// <summary>
    /// シーケンサ
    /// </summary>
    MidiFileSequencer Sequencer = default;

    /// <summary>
    /// リセットイベント
    /// </summary>
    private void Reset()
    {
        MidiPlayer = GameObject.Find("MidiPlayer").GetComponent<MidiPlayer>();
        PlayingTimeText = transform.Find("PlayingTime").Find("PlayingTimeText").GetComponent<Text>();
        EndTimeText = transform.Find("EndTime").Find("EndTimeText").GetComponent<Text>();
    }

    /// <summary>
    /// 初期処理
    /// </summary>
    private void Start()
    {
        MidiPlayer.AddResetSynthesuzerCallback(ChangedMusic);
        ChangedMusic();
    }

    /// <summary>
    /// メインループ
    /// </summary>
    private void Update()
    {
        UpdatePlayingTime();
    }

    /// <summary>
    /// インスタンス破棄する際のイベント
    /// </summary>
    private void OnDestroy()
    {
        MidiPlayer.RemoveResetSynthesuzerCallback(ChangedMusic);
    }

    /// <summary>
    /// 曲変更時の処理
    /// </summary>
    public void ChangedMusic()
    {
        float ss;
        Sequencer = MidiPlayer.Sequencer;

        ss = Mathf.Floor(Sequencer.CurrentTime * ms);
        PlayingTimeText.text = Mathf.Floor(ss / 60).ToString("00") + ":" + Mathf.Floor(ss % 60).ToString("00");

        ss = Mathf.Floor(Sequencer.EndTime * ms);
        EndTimeText.text = Mathf.Floor(ss / 60).ToString("00") + ":" + Mathf.Floor(ss % 60).ToString("00");
    }

    /// <summary>
    /// 再生中時間の更新
    /// </summary>
    private void UpdatePlayingTime()
    {
        float ss = Mathf.Floor(Sequencer.CurrentTime * ms);
        PlayingTimeText.text = Mathf.Floor(ss / 60).ToString("00") + ":" + Mathf.Floor(ss % 60).ToString("00");
    }

}
