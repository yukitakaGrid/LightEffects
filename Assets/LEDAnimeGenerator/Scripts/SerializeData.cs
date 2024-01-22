using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializeData : MonoBehaviour
{
    private string animationBynaryData = "";
    private string mode = "";
    public void SetIntToBinaryString(int[][] b)
    {
        mode += "BINARYMODE";
        
        StringConverter stringConverter = new StringConverter();
        for (int i = 0; i < b.Length; i++)
        {
            int[] buffer = b[i];
            for (int y = 4; y >= 0; y--)
            {
                string binaryData = "";
                for (int z = 0; z<5; z++)
                {
                    for (int x = 0; x < 5; x++)
                    {
                        binaryData += buffer[y*25 + z*5 + x].ToString();
                        if(x==4 && y==0 && z==4 && i==b.Length-1)
                            continue;
                        binaryData += ",";
                    }
                }

                
                //文字列結合
                animationBynaryData += binaryData;
            }
        }
    }

    public void SetIntToArrayString(int[][] b)
    {
        mode += "ARRAYMODE";
        
        animationBynaryData += "uint32_t pattern_2d[][5] = {\n";

        StringConverter stringConverter = new StringConverter();
        for (int i = 0; i < b.Length; i++)
        {
            int[] buffer = b[i];
            animationBynaryData += "  {";
            for (int y = 4; y >= 0; y--)
            {
                string binaryData = "";
                for (int index = 24; index >= 0; index--)
                {
                    //binaryDataにbufferのindex目のint型データをstring型データに変換し、追加する
                    binaryData += buffer[y * 25 + index].ToString();
                }

                binaryData += "0000000";
                byte[] byteData = new byte[8];
                for (int j = 0; j < 8; j++)
                {
                    //単位バイトずつ受け取った文字列を生成
                    string oneByte = "";
                    for (int k = 0; k < 4; k++)
                    {
                        oneByte += binaryData[j * 4 + k];
                    }
                    byteData[j] = Convert.ToByte(oneByte,2);
                }
                //1フレーム分32bit(8Byte)分の2進数を16進数に文字列として変換
                string hexString = BitConverter.ToString(byteData).Replace("-", "");
                hexString = stringConverter.RemoveEvenNumber(hexString);
                //文字列結合
                animationBynaryData += "0x" + hexString;
                
                if(y==0)continue;
                animationBynaryData += ",";
            }
            if(i==b.Length-1)continue;
            animationBynaryData += "},\n";
        }

        animationBynaryData += "}\n" +
                               "};";
    }

    public string GetAnimationBinaryData()
    {
        return animationBynaryData;
    }
}
