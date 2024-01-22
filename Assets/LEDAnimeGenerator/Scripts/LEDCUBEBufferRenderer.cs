using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEDCUBEBufferRenderer : MonoBehaviour
{
    //5x5x5の配列を用意する
    private int[] _buffer;

    [SerializeField] private GameObject[] LED = new GameObject[125];
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("buffer setup.");
        _buffer = new int[125];
    }

    void Update()
    {
        
    }

    public int[] GetAllBuffer()
    {
        int[] buffer = (int[])_buffer.Clone();
        return buffer;  
    }

    public void SetBuffer(int x, int y, int z)
    {
        //x,y,zの値を受け取って、bufferの値を変更する
        //bufferの値が0なら1に、1なら0にする
        int index = x + y * 25 + z * 5;
        if (_buffer[index] == 0)
        {
            _buffer[index] = 1;
        }
        else
        {
            _buffer[index] = 0;
        }
        //bufferの値に応じて、LEDの色を変更する
        if (_buffer[index] == 0)
        {
            //LEDのマテリアルのemissionをオフにする
            LED[index].GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }
        else
        {
            //LEDのマテリアルをcolor(0,3,191)のemissionをオンにする
            LED[index].GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
                
            Debug.Log(LED[index].GetComponent<Renderer>().material.name + "のemissionをonにしました。");
        }
    }

    public void SetAllBuffer(int[] buffer)
    {
        _buffer = (int[])buffer.Clone();
        for(int i=0; i<125; i++)
        {
            if (_buffer[i] == 0)
            {
                //LEDのマテリアルのemissionをオフにする
                LED[i].GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            }
            else
            {
                //LEDのマテリアルのemissionをオンにする
                LED[i].GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
                
                Debug.Log(LED[i].GetComponent<Renderer>().material.name + "のemissionをonにしました。");
            }
        }
    }
    
public void ResetBuffer()
    {
        for(int i=0; i<125; i++)
        {
            _buffer[i] = 0;
            //LEDのマテリアルのemissionをオフにする
            LED[i].GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }
    }
}
