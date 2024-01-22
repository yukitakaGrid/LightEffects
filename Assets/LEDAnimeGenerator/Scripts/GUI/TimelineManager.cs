using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimelineManager : MonoBehaviour
{
    [SerializeField] private GameObject LEDCUBE;
    [SerializeField] private Slider frameRateSlider;
    [SerializeField] private Slider playTimeSlider;
    [SerializeField] private GameObject frameStepPrefab;
    [SerializeField] private EditText frameRateText;
    [SerializeField] private EditText playTimeText;
    [SerializeField] private Scrollbar _scrollbar;
 
    private int frameRate;
    private int playTime;
    private bool pushed;
    private float dashStartTime;
    private float stepRate;
    private float scrollbarSpeed;

    private FrameStepManager _fsm;
    private LEDCUBEBufferRenderer _ledBR;
    [SerializeField] private SetFrameManager _setFrameManager;
    private IEnumerator rightCoroutine = null;
    private IEnumerator leftCoroutine = null;

    private int currentFrameStep = -1;
    // Start is called before the first frame update
    void Start()
    {
        pushed = false;
        dashStartTime = 1.0f;
        stepRate = 0.05f;
        scrollbarSpeed = 0.0006f;
        
        _fsm = GetComponent<FrameStepManager>();
        _ledBR = LEDCUBE.GetComponent<LEDCUBEBufferRenderer>();

        frameRate = (int)frameRateSlider.value;
        playTime = (int)playTimeSlider.value;

        Debug.Log("フレームレートをテキストに反映する処理");
        for(int i=0; i<transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<FrameStep>().SetFrameStepNum(60/frameRate*(i));
        }
        
        UpdateFrameRate();
        UpdatePlayTime();
    }

    private void Update()
    {
        if(currentFrameStep!=-1)
        if (Input.GetKey(KeyCode.D) && currentFrameStep <= transform.childCount && !pushed)
        {
            rightCoroutine = RightCoroutine();
            StartCoroutine(rightCoroutine);
        }
        else if (Input.GetKey(KeyCode.A) && currentFrameStep >= 0 && !pushed)
        {
            leftCoroutine = LeftCoroutine();
            StartCoroutine(leftCoroutine);
        }

        if (!(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)))
        {
            if(!Input.GetKey(KeyCode.D) && rightCoroutine!=null)
            {
                StopCoroutine(rightCoroutine);
                rightCoroutine = null;
            }
            if (!Input.GetKey(KeyCode.A) && leftCoroutine!=null)
            {
                StopCoroutine(leftCoroutine);
                leftCoroutine = null;
            }

            pushed = false;
        }
    }

    public int GetFrameRate()
    {
        return frameRate;
    }
    
    public int GetPlayTime()
    {
        return playTime;
    }

    public void SetFrameRate(int frameRate)
    {
        this.frameRate = frameRate;
    }

    public void SetPlayTime(int playtime)
    {
        this.playTime = playTime;
    }
    
    public void SetCurrentFrameStep()
    {
        currentFrameStep = _fsm.GetCurrentFrameStep();
    }

    public int GetCurrentFrameStep()
    {
        return currentFrameStep;
    }
    
    public void SaveCurrentFrameStep()
    {
        Debug.Log("Saving Now...");
        int[] b = _ledBR.GetAllBuffer();
        if (currentFrameStep != -1)
        {
            FrameStep child = transform.GetChild(currentFrameStep).GetComponent<FrameStep>();
            child.SetAllBuffer(b);
        }
    }
    
    public void LoadCurrentFrameStep()
    {
        Debug.Log("Loading Now...");
       
        SetCurrentFrameStep();
        FrameStep child = transform.GetChild(currentFrameStep).GetComponent<FrameStep>();
        _ledBR.SetAllBuffer(child.GetAllBuffer());
    }
    
    public void UpdateFrameRate()
    {
        var frameStepSum = transform.childCount;
        frameRate = (int)frameRateSlider.value;
        int diff = (frameRate * playTime) - frameStepSum;
        
        for(int i=0; i<transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<FrameStep>().SetFrameStepNum(60/frameRate*(i));
        }

        if (diff > 0)
        {
            //diff分だけFrameStepをframeStepPrehabを基に増やす
            for (int i = 0; i < diff; i++)
            {
                GameObject frameStep = Instantiate(frameStepPrefab, transform);
                frameStep.GetComponent<FrameStep>().SetAllBuffer(new int[125]);
                frameStep.GetComponent<FrameStep>().SetFrameStepNum(60/frameRate * (frameStepSum + i));
                frameStep.GetComponent<FrameStepColorChanger>().SetNum(frameStepSum + i);
            }
        }
        
        else if (diff < 0)
        {
            //diff分だけFrameStepを削除する
            for (int i = 0; i < -diff; i++)
            {
                //このループが終わりスクリプトを抜けるまで、transformは更新されない？
                Destroy(transform.GetChild(transform.childCount - (1+i)).gameObject);
            }
        }

        frameStepSum = frameRate * playTime;
        frameRateText.SetText(frameRate.ToString() + "fps");
        
        Debug.Log("現在のフレームレートは" + frameRate + "です");
        Debug.Log("現在のプレイタイムは" + playTime + "です");
        Debug.Log("現在のframeStepの数は" + frameStepSum + "です");
    }

    public void UpdatePlayTime()
    {
        var frameStepSum = transform.childCount;
        playTime = (int)playTimeSlider.value;
        int diff = (frameRate * playTime) - frameStepSum;
        
        if (diff > 0)
        {
            //diff分だけFrameStepをframeStepPrehabを基に増やす
            for (int i = 0; i < diff; i++)
            {
                GameObject frameStep = Instantiate(frameStepPrefab, transform);
                frameStep.GetComponent<FrameStep>().SetAllBuffer(new int[125]);
                frameStep.GetComponent<FrameStep>().SetFrameStepNum(60/frameRate * (frameStepSum + i));
                frameStep.GetComponent<FrameStepColorChanger>().SetNum(frameStepSum + i);
            }
        }
        
        else if (diff < 0)
        {
            //diff分だけFrameStepを削除する
            for (int i = 0; i < -diff; i++)
            {
                Destroy(transform.GetChild(transform.childCount - (1+i)).gameObject);
            }
        }
        
        frameStepSum = frameRate * playTime;
        int framePlayTime = playTime * frameRate;
        playTimeText.SetText(framePlayTime.ToString() + "F");
        
        Debug.Log("現在のフレームレートは" + frameRate + "です");
        Debug.Log("現在のプレイタイムは" + playTime + "です");
        Debug.Log("現在のframeStepの数は" + frameStepSum + "です");
    }

    public void UpdateFrameStep(int frameRate,int playTime)
    {
        var frameStepSum = transform.childCount;
        this.frameRate = frameRate;
        this.playTime = playTime;
        int diff = (frameRate * playTime) - frameStepSum;

        if (diff == 0) return;
        
        for(int i=0; i<transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<FrameStep>().SetFrameStepNum(60/frameRate*(i+1));
        }

        if (diff > 0)
        {
            //diff分だけFrameStepをframeStepPrehabを基に増やす
            for (int i = 0; i < diff; i++)
            {
                GameObject frameStep = Instantiate(frameStepPrefab, transform);
                frameStep.GetComponent<FrameStep>().SetAllBuffer(new int[125]);
                frameStep.GetComponent<FrameStep>().SetFrameStepNum(60/frameRate * (frameStepSum + i));
                frameStep.GetComponent<FrameStepColorChanger>().SetNum(frameStepSum + i);
            }
        }
        
        else if (diff < 0)
        {
            //diff分だけFrameStepを削除する
            for (int i = 0; i < -diff; i++)
            {
                //このループが終わりスクリプトを抜けるまで、transformは更新されない？
                Destroy(transform.GetChild(transform.childCount - (1+i)).gameObject);
            }
        }

        frameStepSum = frameRate * playTime;
        frameRateText.SetText(frameRate.ToString());
        playTimeText.SetText(playTime.ToString());
        
        Debug.Log("現在のフレームレートは" + frameRate + "です");
        Debug.Log("現在のプレイタイムは" + playTime + "です");
        Debug.Log("現在のframeStepの数は" + frameStepSum + "です");
    }

    public void MoveTimelineButton(string dir)
    {
        Debug.Log("FrameStepがkeyによって動きました。");
        int step;
        if (dir == "right") step = 1;
        else if (dir == "left") step = -1;
        else
        {
            step = 0;
            Debug.Log("MoveTimelineButtonの引数の指定が間違っています。" );
        }

        _fsm.UpdateFrameStep(currentFrameStep + step);
        SaveCurrentFrameStep();
        LoadCurrentFrameStep();
        _setFrameManager.ResetAllButton();
        _setFrameManager.ReflectButton();
    }
    
    IEnumerator RightCoroutine(string direction = "right")
    {
        while (true)
        {
            if (currentFrameStep < transform.childCount-1)
            {
                if (!pushed)
                {
                    MoveTimelineButton(direction);
                    pushed = true;
                    _scrollbar.value += scrollbarSpeed;
                    yield return new WaitForSeconds(dashStartTime);
                }
                else
                {
                    MoveTimelineButton(direction);
                    _scrollbar.value += scrollbarSpeed;
                    yield return new WaitForSeconds(stepRate);
                }
            }
            else yield return new WaitForSeconds(1);
        }
    }
    
    IEnumerator LeftCoroutine(string direction = "left")
    {
        while (true)
        {
            if (currentFrameStep>0)
            {
                if (!pushed)
                {
                    MoveTimelineButton(direction);
                    pushed = true;
                    _scrollbar.value -= scrollbarSpeed;
                    yield return new WaitForSeconds(dashStartTime);
                }
                else
                {
                    MoveTimelineButton(direction);
                    _scrollbar.value -= scrollbarSpeed;
                    yield return new WaitForSeconds(stepRate);
                }
            }
            else yield return new WaitForSeconds(1);
        }
    }
}
