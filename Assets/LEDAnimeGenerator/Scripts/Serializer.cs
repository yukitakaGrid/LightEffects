using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SFB;

[System.Serializable]
public class AnimationData
{
    public string AnimationBinaryData;
}

public class Serializer : MonoBehaviour
{
    public void Serialize(SerializeData data)
    {
        string path = SaveFileBrowser();
        AnimationData animationData = new AnimationData();
        animationData.AnimationBinaryData = data.GetAnimationBinaryData();
        using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
        {
            // StreamWriterを使用してファイルに書き込む
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(data.GetAnimationBinaryData());
            }
        }
    }
    
    public string SaveFileBrowser()
    {
        string path = StandaloneFileBrowser.SaveFilePanel("Title", "", "New AnimationData", "txt");
        if (path.Length > 0)
        {
            Debug.Log("保存先 : " + path);
            return path;
        }

        return null;
    }
}
