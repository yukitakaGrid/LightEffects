using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StringConverter : MonoBehaviour
{
    public string RemoveEvenNumber(string text)
    {
        string result = "";
        for (int i = 0; i < text.Length; i++)
        {
            if (i % 2 == 1)
            {
                result += text[i];
            }
        }

        return result;
    }

    /*public string ReverceArray(string text)
    {
        string updateText = "";
        for (int i = text.Length-1; i >= 0; i--)
        {
            updateText += text[i];
        }

        return updateText;
    }*/
}
