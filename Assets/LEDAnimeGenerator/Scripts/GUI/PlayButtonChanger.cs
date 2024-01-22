using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayButtonChanger : MonoBehaviour
{
    private bool _buttonDown = false;

    private Color _activeColor;
    private Color _defaultColor;
    private Image _image;
    private int _num;
    private bool stopActive = false;
    
    [SerializeField] private AnimePlayer _animePlayer;
    
    void Start()
    {
        _image = GetComponent<Image>();
        _activeColor = GetComponent<Button>().colors.pressedColor;
        _defaultColor = GetComponent<Button>().colors.normalColor;
    }
    
    public void SetNum(int num)
    {
        this._num = num;
    }

    public int GetNum()
    {
        return _num;
    }

    public void PlayColorChanger()
    {
        if (_animePlayer.GetActivePlayer()) return;
        
        if (_buttonDown) 
        {
            _buttonDown = false;
        }else 
        {
            _buttonDown = true;
        }
        Debug.Log(transform.name + " : " + _buttonDown);
        
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

    public void StopColorChanger()
    {
        if (!stopActive) return;
        if (!_animePlayer.GetActivePlayer()) return;
        
        if (_buttonDown) 
        {
            _buttonDown = false;
        }else 
        {
            _buttonDown = true;
        }
        Debug.Log(transform.name + " : " + _buttonDown);
        
        if(_buttonDown)
        {
            //UIのbuttonのカラーを押されているままにする
            _image.color = _activeColor;
            stopActive = true;
        }
        else
        {
            _image.color = _defaultColor;
            stopActive = false;
        }
    }
}
