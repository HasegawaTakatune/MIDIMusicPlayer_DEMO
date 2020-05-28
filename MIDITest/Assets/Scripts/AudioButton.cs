using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    private const string PLAY = "▶";
    private const string STOP = "■";

    [SerializeField] private UnityMidi.MidiPlayer midiPlayer = default;
    [SerializeField] private Text text = default;

    private bool isPlay = false;

    private void Reset()
    {
        midiPlayer = GameObject.Find("MidiPlayer").GetComponent<UnityMidi.MidiPlayer>();
        text = GetComponentInChildren<Text>();
    }

    public void OnClickPlayButton()
    {
        isPlay = !isPlay;
        if (isPlay)
        {
            midiPlayer.Play();
            text.text = STOP;

            StartCoroutine(IsPlaying());
        }
        else
        {
            midiPlayer.Stop();
            text.text = PLAY;
        }
    }

    public IEnumerator IsPlaying()
    {
        while (midiPlayer.IsPlaying)
        {
            yield return 0;
        }

        isPlay = !isPlay;
        midiPlayer.Stop();
        text.text = PLAY;
    }
}
