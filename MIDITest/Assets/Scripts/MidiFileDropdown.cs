using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityMidi;

/// <summary>
/// MIDIファイルの選択を制御する
/// </summary>
public class MidiFileDropdown : MonoBehaviour
{
    /// <summary>
    /// MIDIディレクトリ
    /// </summary>
    //private const string DIRECTORY_MIDI = "MIDI/";

    /// <summary>
    /// MIDI拡張子
    /// </summary>
    private const string EXTENSION_MIDI = ".mid";

    /// <summary>
    /// MIDIフォルダのパス表示テキスト
    /// </summary>
    [SerializeField] private Text midiFolderPathText = default;
    /// <summary>
    /// MIDI選択ドロップダウン
    /// </summary>
    [SerializeField] private Dropdown midiFileDropdown = default;
    /// <summary>
    /// MIDIプレイヤ
    /// </summary>
    [SerializeField] private MidiPlayer midiPlayer = default;

    /// <summary>
    /// フォルダパス
    /// </summary>
    private string FolderPath = "";

    /// <summary>
    /// リセットイベント
    /// </summary>
    private void Reset()
    {
        midiPlayer = GameObject.Find("MidiPlayer").GetComponent<MidiPlayer>();
        midiFolderPathText = GameObject.Find("MidiFolderPathText").GetComponent<Text>();

        midiFileDropdown = GetComponent<Dropdown>();
        Init();
    }

    /// <summary>
    /// スタートイベント
    /// </summary>
    private void Start()
    {
        // プラットフォーム別にフォルダパスを設定する
        if (Application.platform == RuntimePlatform.Android)
        {
            FolderPath = Application.persistentDataPath;
        }
        else
        {
            FolderPath = Application.streamingAssetsPath;
        }
        midiFolderPathText.text = FolderPath;
        Init();
    }

    /// <summary>
    /// 初期化
    /// </summary>
    private void Init()
    {
        midiFileDropdown.ClearOptions();

        string[] files = Directory.GetFiles(FolderPath, "*" + EXTENSION_MIDI, SearchOption.AllDirectories);
        List<string> options = new List<string>();

        for (int i = 0; i < files.Length; i++)
            options.Add(Path.GetFileNameWithoutExtension(files[i]));

        midiFileDropdown.AddOptions(options);
    }

    /// <summary>
    /// MIDIファイルの選択イベント
    /// </summary>
    /// <param name="input"></param>
    public void SelectedMidiFile(Dropdown input)
    {
        midiPlayer.midiSource.streamingAssetPath = input.options[input.value].text + EXTENSION_MIDI;
        midiPlayer.ResetSynthesizer();
    }
}
