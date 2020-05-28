using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ログ表示を制御する
/// </summary>
public class Log : MonoBehaviour
{
    /// <summary>
    /// ログ表示するテキスト
    /// </summary>
    [SerializeField] private Text logText = default;

    /// <summary>
    /// リセットイベント
    /// </summary>
    private void Reset()
    {
        logText = GameObject.Find("LogText").GetComponent<Text>();
    }

    /// <summary>
    /// ログ表示
    /// </summary>
    /// <param name="message"></param>
    public void ShowLog(string message)
    {
        logText.text = message;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// OKボタンクリックイベント
    /// </summary>
    public void OnClickOKButton()
    {
        gameObject.SetActive(false);
        logText.text = "";
    }
}
