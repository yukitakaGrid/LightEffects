using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimePlayer : MonoBehaviour
{
    [SerializeField] private GameObject Timeline;
    [SerializeField] private LEDCUBEBufferRenderer LEDCUBE;
    
    private bool activePlayer = false;
    
    private Coroutine playCoroutine;

    private int currentframeStep = 0;

    public bool GetActivePlayer()
    {
        return activePlayer;
    }
    
    //非同期処理を使って指定フレームごとに再生する
    public void PlayAnime()
    {
        if (!activePlayer)
        {
            activePlayer = true;
        
            Debug.Log("再生します。");
            playCoroutine = StartCoroutine(Play());
        }
    }
    
    public void StopAnime()
    {
        if (playCoroutine != null)
        {
            activePlayer = false;
            StopCoroutine(playCoroutine);
            Debug.Log("停止します。");
        }
    }
    
    public void ResetAnime()
    {
        if (playCoroutine != null)
        {
            activePlayer = false;
            StopCoroutine(playCoroutine);
            Debug.Log("リセットします。");
            currentframeStep = 0;
            LEDCUBE.ResetBuffer();
        }
        PlayAnime();
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator Play()
    {
        //フレームレートを取得
        int frameRate = Timeline.GetComponent<TimelineManager>().GetFrameRate();
        while (true)
        {
            Debug.Log("再生中 : 現在のフレーム" + currentframeStep * (60 / frameRate) + "F");
            //Timelineの子オブジェクトを取得
            GameObject frameStep = Timeline.transform.GetChild(currentframeStep).gameObject;
            //frameStepのbufferを取得
            int[] buffer = frameStep.GetComponent<FrameStep>().GetAllBuffer();
            Timeline.GetComponent<FrameStepManager>().UpdateFrameStep(currentframeStep);
            //bufferをセット
            LEDCUBE.SetAllBuffer(buffer);
            //次のフレームに進める
            currentframeStep++;
            
            if(currentframeStep>=Timeline.transform.childCount)
            {
                currentframeStep = 0;
            }
            yield return new WaitForSeconds(1.0f / frameRate);
        }
    }
}
