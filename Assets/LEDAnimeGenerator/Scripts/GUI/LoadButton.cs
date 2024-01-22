using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadButton : MonoBehaviour
{
    [SerializeField] private GameObject frameStepManager;
    [SerializeField] private LEDCUBEBufferRenderer bufferRenderer;
    [SerializeField] private TimelineManager _timelineManager;

    public void LoadBuffer()
    {
        Load load = new Load();
        SaveData[] data = load.LoadBuffer();
        
        //タイムラインを更新して、フレームを読み込む事前準備をする
        _timelineManager.UpdateFrameStep(data[0].GetFrameRate(),data[0].GetPlayTime());
        //フレームの読み込み
        for (int i = 0; i < data.Length; i++)
        {
            frameStepManager.transform.GetChild(i).GetComponent<FrameStep>().SetAllBuffer(data[i].GetSaveData());
        }
        
        int currentFrameStep = frameStepManager.GetComponent<FrameStepManager>().GetCurrentFrameStep();
        if(currentFrameStep != -1)
        {
            bufferRenderer.SetAllBuffer(frameStepManager.transform.GetChild(currentFrameStep).GetComponent<FrameStep>().GetAllBuffer());
        }
    }
}