using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using SFB;

[System.Serializable]
public class Data
{
    public string frameBuffer;
    public int frameRate;
    public int playTime;
}


public class Save : MonoBehaviour
{
    public void SaveBuffer(SaveData[] data,int frameRate,int playTime)
    {
        string path = SaveFileBrowser();

        Data saveData = new Data();
        string allFrameBuffer = "";
        for(int i = 0; i < data.Length; i++)
        {
            //allFrameBufferにGetSaveData()で返ってきたint[]をstringに変換して代入
            allFrameBuffer += string.Join(",", data[i].GetSaveData());
            if (data.Length - 1 == i)
            {
                Debug.Log(allFrameBuffer);
                continue;
            }
            allFrameBuffer += ",";
        }
        saveData.frameBuffer = allFrameBuffer;
        saveData.frameRate = frameRate;
        saveData.playTime = playTime;
        string json = JsonUtility.ToJson(saveData);
        
        Debug.Log(json);
        StreamWriter streamWriter = new StreamWriter(path);
        streamWriter.Write(json);
        streamWriter.Flush();
        streamWriter.Close();
    }

    public string SaveFileBrowser()
    {
        string path = StandaloneFileBrowser.SaveFilePanel("Title", "", "MySaveFile", "json");
        if (path.Length > 0)
        {
            Debug.Log("保存先 : " + path);
            return path;
        }

        return null;
    }
}
