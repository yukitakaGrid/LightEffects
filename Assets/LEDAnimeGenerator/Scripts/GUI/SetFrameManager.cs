using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFrameManager : MonoBehaviour
{
    private GameObject[] LEDButtonArray = new GameObject[125];

    [SerializeField] private GameObject _timeline;

    private TimelineManager _timelineData;
    // Start is called before the first frame update
    void Start()
    {
        _timelineData = _timeline.GetComponent<TimelineManager>();
        
        Debug.Log("SetFrameManager Start");
        //子オブジェクトの数だけループする
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).transform.Find("GridRow").gameObject == null)
            {
                Debug.Log("GridRow is not found");
                continue;
            }
            else
            {
                GameObject gridRow = transform.GetChild(i).transform.Find("GridRow").gameObject;
                
                for (int j = 0; j < gridRow.transform.childCount; j++)
                {
                    GameObject grandChild = gridRow.transform.GetChild(j).gameObject;
                    
                    for(int k=0; k<grandChild.transform.childCount; k++)
                    {
                        GameObject greatGrandChild = grandChild.transform.GetChild(k).gameObject;
                        //Debug.Log(k + j * 5 + i * 25 + "番目は" + greatGrandChild.name);
                        LEDButtonArray[k + j * 5 + i * 25] = greatGrandChild;
                    }
                }
            }
        }
        Debug.Log("SetFrameManager End");
    }

    public void ResetAllButton()
    {
        Debug.Log("ResetAllButton");
        for (int i = 0; i < LEDButtonArray.Length; i++)
        {
            LEDButtonArray[i].GetComponent<ButtonChanger>().SetButtonDown(false);
        }
    }

    public void ReflectButton()
    {
        int currentFrameStep = _timelineData.GetCurrentFrameStep();
        if (currentFrameStep != -1)
        {
            int[] _buffer = _timeline.transform.GetChild(currentFrameStep).GetComponent<FrameStep>().GetAllBuffer();
            for (int i = 0; i < LEDButtonArray.Length; i++)
            {
                bool active = (_buffer[i] == 0) ? false : true;
                LEDButtonArray[i].GetComponent<ButtonChanger>().SetButtonDown(active);
            }
        }
    }

    public GameObject[] GetLEDButtonArray()
    {
        return LEDButtonArray;
    }
}
