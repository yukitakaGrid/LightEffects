using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EditText))]
public class FrameStep : MonoBehaviour
{
    [SerializeField] private EditText frameText;
    public int[] _buffer;

    private int frameStepNum;
    // Start is called before the first frame update
    void Start()
    {
        _buffer = new int[125];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //bufferにx,y,zに応じた値をセットする
    public void SetBuffer(int x, int y, int z)
    {
        int index = x + y * 25 + z * 5;
        if (_buffer[index] == 0)
        {
            _buffer[index] = 1;
        }
        else
        {
            _buffer[index] = 0;
        }
    }
    
    public void SetAllBuffer(int[] buffer)
    {
        _buffer = (int[])buffer.Clone();
    }

    public void SetFrameStepNum(int num)
    {
        frameStepNum = num;
        //SetTextにframeStepNum.ToString() + "F"と表示させたい
        if ((float)frameStepNum % 60 == 0)
        {
            frameText.SetText(frameStepNum/60 + "秒");
        }
        else
        {
            frameText.SetText(frameStepNum + "F");
        }
    }

    public int[] GetAllBuffer()
    {
        int[] buffer = (int[])_buffer.Clone();
        return buffer;
    }
}
