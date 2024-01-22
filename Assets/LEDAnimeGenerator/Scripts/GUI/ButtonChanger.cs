using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonChanger : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
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

        _ledState = GetComponent<LEDButtonState>();
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
    
    // Update is called once per frame
    /*void Update()
    {
        if(_buttonDown)
        {
            //UIのbuttonのカラーを押されているままにする
            _image.color = _activeColor;
        }
        else
        {
            _image.color = _defaultColor;
        }
    }*/
    
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
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        //マウスの左クリックが押されているかどうかの処理をかいて
        if (Input.GetMouseButton(0))
        {
            if (_buttonDown)
            {
                _buttonDown = false;
            }
            else
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
}
