using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FrameStepColorChanger : MonoBehaviour
{
    private bool _buttonDown = false;

    private Color _activeColor;
    private Color _defaultColor;
    private Image _image;
    private int _num;

    private LEDButtonState _ledState;

    void Start()
    {
        _image = GetComponent<Image>();
        _activeColor = GetComponent<Button>().colors.pressedColor;
        _defaultColor = _image.color;
    }
    
    public void SetNum(int num)
    {
        this._num = num;
    }

    public int GetNum()
    {
        return _num;
    }

    public bool GetButtonDown()
    {
        return _buttonDown;
    }

    public void SetButtonDown(bool buttonDown)
    {
        this._buttonDown = buttonDown;
        
        if(_buttonDown)
        {
            //UIのbuttonのカラーを押されているままにする
            _image.color = _activeColor;
        }
        else
        {
            _image.color = _defaultColor;
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_buttonDown) 
        {
            _buttonDown = false;
        }else 
        {
            _buttonDown = true;
        }
        Debug.Log(transform.name + " : " + _buttonDown);
        
        if(_ledState!=null)
            _ledState.OnEnter();
        if(_buttonDown)
        {
            //UIのbuttonのカラーを押されているままにする
            _image.color = _activeColor;
        }
        else
        {
            _image.color = _defaultColor;
        }
    }
}
