using UnityEngine;
using System.Collections.Generic;
using System.IO;
using AudioSynthesis;

namespace UnityMidi
{
    /// <summary>
    /// アセットのセットアップ（Android版）
    /// </summary>
    public static class StreamingAssetsSetupByAndroid
    {
        /// <summary>
        /// 拡張子.bank
        /// </summary>
        private static readonly string Extension_Bank = ".bank";
        /// <summary>
        /// 拡張子.mid
        /// </summary>
        private static readonly string Extension_Midi = ".mid";
        /// <summary>
        /// Bankファイル名
        /// </summary>
        public static string bankFile = "GMBank";
        /// <summary>
        /// MIDIファイル名
        /// </summary>
        public static string[] midiFiles = new string[] { "AllSeasons", "FineDays" };

        /// <summary>
        /// セットアップ
        /// </summary>
        public static void setup()
        {
            // ファイルパスリスト
            List<string> filePaths = new List<string>() { };

            // 参照先に目的のファイルがなければファイルパスリストに記載
            if (!File.Exists(Application.persistentDataPath + "/" + bankFile + Extension_Bank))
                filePaths.Add(bankFile + Extension_Bank);

            for (int i = 0; i < midiFiles.Length; i++)
                if (!File.Exists(Application.persistentDataPath + "/" + midiFiles[i] + Extension_Midi))
                    filePaths.Add(midiFiles[i] + Extension_Midi);

            // ファイルパスリストに記載があればファイルのコピーを開始
            if (0 < filePaths.Count)
                FileCopyFromStreamingToPersistent(filePaths);

        }

        /// <summary>
        /// ファイルコピー
        /// </summary>
        /// <param name="filePaths"></param>
        public static void FileCopyFromStreamingToPersistent(List<string> filePaths)
        {
            // StreamingAssetsからPersistentDataにファイルをコピーする
            foreach (string path in filePaths)
            {
                WWW www = new WWW(Application.streamingAssetsPath + "/" + path);
                while (!www.isDone) { }
                File.WriteAllBytes(Application.persistentDataPath + "/" + path, www.bytes);
            }
        }
    }

    [System.Serializable]
    public class StreamingAssetResouce : IResource
    {
        [SerializeField] public string streamingAssetPath;

        public bool ReadAllowed()
        {
            return true;
        }

        public bool WriteAllowed()
        {
            return false;
        }

        public bool DeleteAllowed()
        {
            return false;
        }

        public string GetName()
        {
            return Path.GetFileName(streamingAssetPath);
        }

        public Stream OpenResourceForRead()
        {
            string streamingPath = string.Empty;

            if (Application.platform == RuntimePlatform.Android)
            {
                streamingPath = Application.persistentDataPath + "/" + streamingAssetPath;
                return File.OpenRead(streamingPath);
            }
            else
            {
                streamingPath = Path.Combine(Application.streamingAssetsPath, streamingAssetPath);
            }
            Debug.Log("Streaming path : " + streamingAssetPath);
            return File.OpenRead(streamingPath);
        }

        public Stream OpenResourceForWrite()
        {
            throw new System.NotImplementedException();
        }

        public void DeleteResource()
        {
            throw new System.NotImplementedException();
        }
    }
}
