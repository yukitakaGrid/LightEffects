using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using UnityEngine;
using SFB;
using Application = UnityEngine.Application;

public class Load : MonoBehaviour
{
    public SaveData[] LoadBuffer()
    {
        string path = LoadFileBrowser();
        
        string json = File.ReadAllText(path);
        Data loadData = JsonUtility.FromJson<Data>(json);
        Debug.Log(loadData.frameBuffer);

        int[] frameBuffer = ParseStringToIntArray(loadData.frameBuffer);

        int frameStepCount= (frameBuffer.Length + 1) / 125;
        SaveData[] data = new SaveData[frameStepCount];
        for (int i = 0; i < frameStepCount; i++)
        {
            data[i] = new SaveData();
            int[] buffer = new int[125];
            for(int j=0; j<125; j++)
            {
                buffer[j] = frameBuffer[i * 125 + j];
            }
            data[i].SetSaveData(buffer);
        }

        data[0].SetFrameRate(loadData.frameRate);
        data[0].SetPlayTime(loadData.playTime);
        
        return data;
    }

    public string LoadFileBrowser()
    {
        string path;
        #if UNITY_EDITOR
        path = UnityEditor.EditorUtility.OpenFilePanel("Title", "", "json");
        return path;
        #endif

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "json files (*.json)|*.json";
        openFileDialog.InitialDirectory = Application.dataPath;

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            return openFileDialog.FileName;
        }
        else
        {
            return "";
        }
        #endif
    }
    
    public int[] ParseStringToIntArray(string str)
    {
        string[] strArray = str.Split(',');
        int[] intArray = new int[strArray.Length];
        for (int i = 0; i < strArray.Length; i++)
        {
            intArray[i] = int.Parse(strArray[i]);
        }
        return intArray;
    }
}
