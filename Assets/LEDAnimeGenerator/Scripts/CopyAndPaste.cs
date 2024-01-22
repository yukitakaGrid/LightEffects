using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyAndPaste : MonoBehaviour
{
    private bool[] buffer;

    public void Copy(bool[] b)
    {
        buffer = b;
    }

    public bool[] Paste()
    {
        return (bool[])buffer.Clone();
    }
}
