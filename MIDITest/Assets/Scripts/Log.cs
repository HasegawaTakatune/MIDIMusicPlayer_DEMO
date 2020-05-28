using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Log : MonoBehaviour
{
    [SerializeField] private Text logText = default;

    private void Reset()
    {
        logText = GameObject.Find("LogText").GetComponent<Text>();
    }

    public void ShowLog(string message)
    {
        logText.text = message;
        gameObject.SetActive(true);
    }

    public void OnClickOKButton()
    {
        gameObject.SetActive(false);
        logText.text = "";
    }
}
