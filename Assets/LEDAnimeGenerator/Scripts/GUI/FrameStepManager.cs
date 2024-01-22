using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameStepManager : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private AudioSource _audioSource;
    
    private object lockObject = new object();
    
    private int currentFrameStep = -1;
    private bool isProcessing = false;

    // Start is called before the first frame update
    void Start()
    {
        //子オブジェクトに各要素を与える
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<ButtonChanger>().SetNum(i);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    //もともとアクティブだったchildを非アクティブにする
    public void SetInactiveChild()
    {
        transform.GetChild(currentFrameStep).gameObject.GetComponent<FrameStepColorChanger>().SetButtonDown(false);
    }

    public void SetActiveChild()
    {
        transform.GetChild(currentFrameStep).gameObject.GetComponent<FrameStepColorChanger>().SetButtonDown(true);
    }
    
    public int GetCurrentFrameStep()
    {
        return currentFrameStep;
    }

    public void UpdateFrameStep(GameObject frameStepButton)
    {
        lock (lockObject)
        {
            if (isProcessing) return;

            //Start Processing
            isProcessing = true;
            
            if (currentFrameStep != -1)
            {
                SetInactiveChild();
            }

            currentFrameStep = frameStepButton.GetComponent<FrameStepColorChanger>().GetNum();
            Debug.Log(currentFrameStep);
            SetActiveChild();
            
            //_audioSource.PlayOneShot(_audioClip);

            //End Processing
            isProcessing = false;
        }
    }

    public void UpdateFrameStep(int setFrameStep)
    {
        lock (lockObject)
        {
            if (isProcessing) return;

            //Start Processing
            isProcessing = true;
            
            if (currentFrameStep != -1)
            {
                SetInactiveChild();
            }

            currentFrameStep = setFrameStep;
            Debug.Log(currentFrameStep);
            SetActiveChild();
            
            //_audioSource.PlayOneShot(_audioClip);

            //End Processing
            isProcessing = false;
        }
    }
}
