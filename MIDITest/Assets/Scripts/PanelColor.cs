using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// パネルカラーの制御
/// </summary>
public class PanelColor : MonoBehaviour
{
    /// <summary>
    /// 最小値
    /// </summary>
    private const int min = 0;
    /// <summary>
    /// 最大値
    /// </summary>
    private const int max = 3;
    /// <summary>
    /// カラータイプ
    /// </summary>
    private enum ColorType { Red, Green, Blue, None };
    /// <summary>
    /// カラーアクション
    /// </summary>
    private enum ColorAction { Add, Sub, None };
    /// <summary>
    /// ステータス
    /// </summary>
    private struct State
    {
        /// <summary>
        /// カラー
        /// </summary>
        public Color color;
        /// <summary>
        /// タイプ
        /// </summary>
        public ColorType type;
        /// <summary>
        /// アクション
        /// </summary>
        public ColorAction action;
        /// <summary>
        /// 次アクションに移行
        /// </summary>
        public void NextAct()
        {
            if (action == ColorAction.Add)
                action = ColorAction.Sub;
            else
                action = ColorAction.Add;
        }
    }

    /// <summary>
    /// ステータス配列
    /// </summary>
    private State[] states = new State[3];

    /// <summary>
    /// イメージ
    /// </summary>
    [SerializeField] private Image image = default;

    /// <summary>
    /// リセットイベント
    /// </summary>
    private void Reset()
    {
        image = GetComponent<Image>();
    }

    /// <summary>
    /// 初期イベント
    /// </summary>
    private void Start()
    {
        float value = 0.005f;

        // 赤色
        states[0].type = ColorType.Red;
        states[0].action = ColorAction.Add;
        states[0].color = new Color(1 * value, 0, 0, 0);

        // 緑色
        states[1].type = ColorType.Green;
        states[1].action = ColorAction.Add;
        states[1].color = new Color(0, 1 * value, 0, 0);

        // 青色
        states[2].type = ColorType.Blue;
        states[2].action = ColorAction.Add;
        states[2].color = new Color(0, 0, 1 * value, 0);

        StartCoroutine(ChangeColor());
    }

    /// <summary>
    /// 色変更
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangeColor()
    {
        int index = Random.Range(min, max);
        bool isChange = false;
        State state = states[index];

        // ループで色変えし続ける
        while (true)
        {
            if (state.type == ColorType.None || state.action == ColorAction.None) break;

            yield return null;

            // 色変更
            UpdateColor(state);

            // 色を変え終わったか判定
            switch (state.type)
            {
                case ColorType.Red: isChange = Check(state.action, image.color.r); break;
                case ColorType.Green: isChange = Check(state.action, image.color.g); break;
                case ColorType.Blue: isChange = Check(state.action, image.color.b); break;
                default: image.color = Color.green; break;
            }

            // 次に変える色を決める
            if (isChange)
            {
                states[index].NextAct();
                IEnumerator coroutine = ChangeIndex(index);
                yield return coroutine;
                index = (int)coroutine.Current;
                state = states[index];
                isChange = false;
            }

        }

    }

    /// <summary>
    /// 色変更
    /// </summary>
    /// <param name="state"></param>
    private void UpdateColor(State state)
    {
        switch (state.action)
        {
            case ColorAction.Add:
                image.color += state.color;
                break;

            case ColorAction.Sub:
                image.color -= state.color;
                break;

            default:
                image.color = Color.green;
                break;
        }
    }

    /// <summary>
    /// 色を変え終わったかをチェックする
    /// </summary>
    /// <param name="act"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    private bool Check(ColorAction act, float value)
    {
        switch (act)
        {
            case ColorAction.Add: if (1 <= value) return true; break;
            case ColorAction.Sub: if (value <= 0) return true; break;
            default: act = ColorAction.None; break;
        }
        return false;
    }

    /// <summary>
    /// 次に変える色を決める
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private IEnumerator ChangeIndex(int index)
    {
        int next = Random.Range(min, max);
        while (true)
        {
            if (index != next) break;
            next = Random.Range(min, max);
            yield return null;
        }

        yield return next;

    }

}
