using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class LEDRotate : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject LEDCUBE;

    private Vector2 mouseSpeed;
    private bool isEnter = false;
    private Vector2 lastMousePosition;
    private Vector2 mousePosition;
    private Quaternion angleBuffer;
    private bool onResetButton = false;
    [SerializeField] private float rotationSpeed = 1.0f;
    [SerializeField] private float returnSpeed = 1.0f;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        isEnter = true;
        Debug.Log(transform.name + "に入りました");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isEnter = false;
        Debug.Log(transform.name + "を抜け出しました");
    }

    public void OnResetButton()
    {
        onResetButton = true;
    }

    public void ResetRotate(Quaternion from,Quaternion to)
    {
        LEDCUBE.transform.rotation = Quaternion.Slerp(from, to, returnSpeed * Time.deltaTime);
    }

    void Update()
    {
        bool leftMousePushed = Input.GetMouseButton(0);
        if (isEnter && leftMousePushed)
        {
            mousePosition = Input.mousePosition;
            mouseSpeed = (mousePosition - lastMousePosition) / Time.deltaTime;
            Vector3 aulerAngle = new Vector3(-mouseSpeed.y * rotationSpeed, -mouseSpeed.x * rotationSpeed, 0);
            Quaternion deltaRotation = Quaternion.Euler(aulerAngle * Time.deltaTime);
            LEDCUBE.transform.rotation = deltaRotation * LEDCUBE.transform.rotation;
            
            lastMousePosition = mousePosition;
        }
        else
        {
            Vector3 aulerAngle = new Vector3(-mouseSpeed.y * rotationSpeed, -mouseSpeed.x * rotationSpeed, 0);
            Quaternion deltaRotation = Quaternion.Euler(aulerAngle * Time.deltaTime);
            LEDCUBE.transform.rotation = deltaRotation * LEDCUBE.transform.rotation;
            
            lastMousePosition = Input.mousePosition;
        }

        if (onResetButton)
        {
            mouseSpeed = Vector3.zero;
            Quaternion to = Quaternion.Euler(0,0,0);
            ResetRotate(LEDCUBE.transform.rotation, to);
            
            if (Quaternion.Angle(LEDCUBE.transform.rotation, to) < 0.01f)
                onResetButton = false;
        }
    }
}
