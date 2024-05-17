using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    public float mouseDragSpeed = .1f;
    private Vector2 velocity = Vector2.zero;

    private Camera mainCamera;
    private GameObject selectedEmail;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        SwipeDetection.instance.swipePerformed += OnEmailSwipe;
        SwipeDetection.instance.pressPerformed += OnEmailPress;
    }

    private void Update()
    {
        if(Input.GetKeyDown("space"))
        {

            // StopAllCoroutines();
        }
    }

    private void OnEmailPress(Vector2 currentPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(currentPos, Vector2.zero);

        if (hit.collider != null) {
            // Debug.Log("HIT: " + hit.collider.gameObject.name + ":" + gameObject.name);
            selectedEmail = hit.collider.gameObject;
            StartCoroutine(DragUpdate(hit.collider.gameObject));
        }
    }

    private IEnumerator DragUpdate(GameObject pressedObject)
    {
        while (SwipeDetection.instance.PressValue != 0) {
            // Debug.Log("Update Position: " + pressedObject.name + "-" + SwipeDetection.instance.PressValue);
            Vector2 newPos = Vector2.SmoothDamp(pressedObject.transform.position, SwipeDetection.instance.Position, ref velocity, mouseDragSpeed);
            newPos.y = pressedObject.transform.position.y;
            pressedObject.transform.position = newPos;
            yield return null;
        }
        selectedEmail = null;
    }

    private void OnEmailSwipe(Vector2 direction, Vector2 currentPos)
    {
        Debug.Log("Swiping");
        // Debug.Log("Direction: " + direction.x);
        // Debug.Log("Position: " + currentPos.x);
        //Debug.Log(Mouse.current.position.ReadValue());
        Debug.Log("Remove Email: " + selectedEmail);
        if (selectedEmail != null)
        {
            Destroy(selectedEmail);
        }
    }
}
