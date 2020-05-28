using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MidiFileDropdown : MonoBehaviour
{
    private const string DIRECTORY_MIDI = "MIDI/";
    private const string EXTENSION_MIDI = ".mid";

    [SerializeField] private Text midiFilePathText = default;
    [SerializeField] private Dropdown midiFileDropdown = default;
    [SerializeField] private UnityMidi.MidiPlayer midiPlayer = default;

    private string FilePath = "";

    private void Reset()
    {
        midiPlayer = GameObject.Find("MidiPlayer").GetComponent<UnityMidi.MidiPlayer>();
        midiFilePathText = GameObject.Find("MidiFilePathText").GetComponent<Text>();

        midiFileDropdown = GetComponent<Dropdown>();
        Init();
    }

    private void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        FilePath = Application.persistentDataPath + "/MIDI";
#else
        FilePath = Application.streamingAssetsPath + "/MIDI";
#endif
        midiFilePathText.text = FilePath;
        Init();
    }

    private void Init()
    {
        midiFileDropdown.ClearOptions();

        string[] files = Directory.GetFiles(FilePath, "*" + EXTENSION_MIDI, SearchOption.AllDirectories);
        List<string> options = new List<string>();

        for (int i = 0; i < files.Length; i++)
            options.Add(Path.GetFileNameWithoutExtension(files[i]));

        midiFileDropdown.AddOptions(options);
    }

    public void SelectedMidiFile(Dropdown input)
    {
        midiPlayer.midiSource.streamingAssetPath = DIRECTORY_MIDI + input.options[input.value].text + EXTENSION_MIDI;
        midiPlayer.ResetSynthesizer();
    }
}
