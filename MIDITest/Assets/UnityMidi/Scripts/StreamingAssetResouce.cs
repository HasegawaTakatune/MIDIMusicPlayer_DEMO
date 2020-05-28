using UnityEngine;
using System.IO;
using AudioSynthesis;

namespace UnityMidi
{
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

            //streamingPath = Application.streamingAssetsPath + streamingAssetPath;
            //WWW www = new WWW(streamingPath);

            //while (!www.isDone) { }
            //AssetBundle bundle = www.assetBundle;

            //streamingPath = Application.persistentDataPath + "/files/" + streamingAssetPath;
            //using (BinaryWriter writer = new BinaryWriter(new FileStream(streamingPath, FileMode.Create)))
            //{
            //    writer.Write(www.assetBundle);

            //    writer.Flush();
            //}
            //return File.OpenRead(streamingPath);

#if UNITY_ANDROID && !UNITY_EDITOR
            
            streamingPath = Application.persistentDataPath + "/" + streamingAssetPath;
            //streamingPath = Application.streamingAssetsPath + "/" + streamingAssetPath;
            return File.OpenRead(streamingPath);

            //streamingPath = "jar:file://" + Application.dataPath + "!/assets/" + streamingAssetPath;
            //streamingPath = Path.Combine(Application.streamingAssetsPath, streamingAssetPath);
            //streamingPath = "file://" + Path.Combine(Application.persistentDataPath, streamingAssetPath);
#else
            streamingPath = Path.Combine(Application.streamingAssetsPath, streamingAssetPath);
#endif
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
