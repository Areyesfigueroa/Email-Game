using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeDetection : MonoBehaviour
{
    public static SwipeDetection instance;

    public delegate void Swipe(Vector2 direction, Vector2 currentPos);
    public event Swipe swipePerformed;

    public delegate void Press(Vector2 currentPos);
    public event Press pressPerformed;

    [SerializeField] private InputAction position, press;

    [SerializeField] private float swipeResistance = 100;

    public float PressValue { get { return press.ReadValue<float>(); } }
    public Vector2 Position { get { return position.ReadValue<Vector2>(); } }

    private Vector2 initialPos;

    private Vector2 currentPos => position.ReadValue<Vector2>();

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
    }

    private void OnEnable()
    {
        position.Enable();
        press.Enable();
        press.performed += _ => DetectPress();
        press.canceled += _ => DetectSwipe();
    }

    private void OnDisable()
    {
        press.performed -= _ => DetectPress();
        press.canceled -= _ => DetectSwipe();

        position.Disable();
        press.Disable();
    }

    private void DetectPress()
    {
        initialPos = currentPos;
        pressPerformed(currentPos);
    }

    private void DetectSwipe()
    {
        // Direction of our swipe
        Vector2 delta = currentPos - initialPos;

        Vector2 direction = Vector2.zero;

        // A swipe happened
        if(Mathf.Abs(delta.x) > swipeResistance)
        {
            direction.x = Mathf.Clamp(delta.x, -1, 1);

        }
        if (Mathf.Abs(delta.y) > swipeResistance)
        {
            direction.y = Mathf.Clamp(delta.y, -1, 1);
        }


        // If we detected a swipe
        if(direction != Vector2.zero && swipePerformed != null)
        {
            swipePerformed(direction, currentPos);
        }
    }
}
