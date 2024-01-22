using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class EditText : MonoBehaviour
{
    private int initValue;
    public GameObject Text;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Slider>() != null)
        {
            initValue = (int)GetComponent<Slider>().value;
            Text.GetComponent<TextMeshProUGUI>().text = initValue.ToString();
        }
    }

    public void SetText(string text)
    {
        Debug.Log(text + "をセットしました");
        Text.GetComponent<TextMeshProUGUI>().text = text;
    }
}
