using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEDButtonState : MonoBehaviour
{
    [SerializeField] int x;
    [SerializeField] int y;
    [SerializeField] int z;

    [SerializeField] GameObject LEDCUBE;
    
    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }
    public int Z { get => z; set => z = value; }
    
    public void OnEnter()
    {
        Debug.Log("x:" + x + " y:" + y + " z:" + z + " is Enter.");
        LEDCUBE.GetComponent<LEDCUBEBufferRenderer>().SetBuffer(x, y, z);
    }
}
