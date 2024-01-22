using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyAndPasteButton : MonoBehaviour
{
    [SerializeField] private LEDCUBEBufferRenderer _LEDCUBE;
    [SerializeField] private SetFrameManager _setFrameManager;
    private GameObject[] LEDButtonArray;

    private CopyAndPaste copyPaste = new CopyAndPaste();
    
    public void Copy()
    {
        LEDButtonArray = _setFrameManager.GetLEDButtonArray();
        bool[] buttonActiveList = new bool[LEDButtonArray.Length];
        for(int i = 0; i < LEDButtonArray.Length; i++)
        {

            buttonActiveList[i] = LEDButtonArray[i].GetComponent<ButtonChanger>().GetButtonDown();
        }
        copyPaste.Copy(buttonActiveList);
    }

    public void Paste()
    {
        if (copyPaste.Paste() != null)
        {
            int frameStepNum = copyPaste.Paste().Length;
            bool[] buttonActiveList = copyPaste.Paste();
            int[] buffer = new int[frameStepNum];

            for (int i = 0; i < frameStepNum; i++)
            {
                //LEDButtonにコピーデータを反映
                LEDButtonArray[i].GetComponent<ButtonChanger>().SetButtonDown(buttonActiveList[i]);
            
                buffer[i] = ((buttonActiveList[i] == true) ? 1 : 0);
            }
            //LEDCUBEにコピーデータを反映
            _LEDCUBE.SetAllBuffer(buffer);
        }
    }
}
