using UnityEngine;
using UnityEngine.UI;

public class ChannelInputField : MonoBehaviour
{
    private const int MAX_CHANNEL = 15;
    private const int MIN_CHANNEL = 0;

    [SerializeField] private InputField channelInputField = default;
    [SerializeField] private UnityMidi.MidiPlayer midiPlayer = default;

    private void Reset()
    {
        midiPlayer = GameObject.Find("MidiPlayer").GetComponent<UnityMidi.MidiPlayer>();
        channelInputField = GetComponent<InputField>();
    }

    public void OnClickChannelChanged(InputField input)
    {
        int value = System.Convert.ToInt32(input.text);

        if (value < MIN_CHANNEL)
        {
            value = MIN_CHANNEL;
            input.text = MIN_CHANNEL.ToString();
        }

        if (MAX_CHANNEL < value)
        {
            value = MAX_CHANNEL;
            input.text = MAX_CHANNEL.ToString();
        }

        midiPlayer.channel = value;
    }
}
