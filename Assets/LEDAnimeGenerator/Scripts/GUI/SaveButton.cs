using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFB;

public class SaveButton : MonoBehaviour
{
    [SerializeField] private GameObject frameStepManager;
    [SerializeField] private TimelineManager _timelineManager;
    
    public SaveData[] GetFrameSequenceData()
    {
        var frameStepCount = frameStepManager.transform.childCount;
        SaveData[] data = new SaveData[frameStepCount];
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = new SaveData();
            data[i].SetSaveData(frameStepManager.transform.GetChild(i).GetComponent<FrameStep>().GetAllBuffer());
        }
        return data;
    }
    
    public void SaveBuffer()
    {
        Save save = new Save();
        SaveData[] data = GetFrameSequenceData();

        int frameRate = _timelineManager.GetFrameRate();
        int playTime = _timelineManager.GetPlayTime();
        save.SaveBuffer(data,frameRate,playTime);
    }
}
