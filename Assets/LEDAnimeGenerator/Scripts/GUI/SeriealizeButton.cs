using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeriealizeButton : MonoBehaviour
{
    [SerializeField] private GameObject _timeline;

    public void SeriearizeArrayBuffer()
    {
        SerializeData data = new SerializeData();
        Serializer serializer = new Serializer();

        int[][] buffer = new int[_timeline.transform.childCount][];
        for (int i = 0; i < buffer.Length; i++)
        {
            buffer[i] = _timeline.transform.GetChild(i).GetComponent<FrameStep>().GetAllBuffer();
        }

        data.SetIntToArrayString(buffer);
        serializer.Serialize(data);
    }
    
    public void SeriearizeBinaryBuffer()
    {
        SerializeData data = new SerializeData();
        Serializer serializer = new Serializer();

        int[][] buffer = new int[_timeline.transform.childCount][];
        for (int i = 0; i < buffer.Length; i++)
        {
            buffer[i] = _timeline.transform.GetChild(i).GetComponent<FrameStep>().GetAllBuffer();
        }

        data.SetIntToBinaryString(buffer);
        serializer.Serialize(data);
    }
}
