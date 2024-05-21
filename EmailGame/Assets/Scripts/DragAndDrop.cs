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
    private float maxScreenBoundary = 0;
    private float minScreenBoundary = 0;

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
            selectedEmail = hit.collider.gameObject;

            if (maxScreenBoundary == 0) maxScreenBoundary = selectedEmail.transform.parent.position.x * 2;

            Debug.Log(maxScreenBoundary);

            //Debug.Log(selectedEmail.transform.parent.position);
            StartCoroutine(DragUpdate(hit.collider.gameObject));
        }
    }

    private IEnumerator DragUpdate(GameObject pressedObject)
    {
        // Calculate the inital touch so that it stays in place until dragged
        Vector2 prevTouchCoord = SwipeDetection.instance.Position;

        Vector2 initEmailPos = pressedObject.transform.position;
        float distance = Mathf.Abs(prevTouchCoord.x - initEmailPos.x);

        float destinationPosX = (prevTouchCoord.x > initEmailPos.x)
            ? Mathf.Abs(prevTouchCoord.x - distance)
            : Mathf.Abs(prevTouchCoord.x + distance);

        Vector2 destinationPos = new Vector2(destinationPosX, initEmailPos.y);


        while (SwipeDetection.instance.PressValue != 0 && pressedObject != null) {

            // Check if the email is more than halfway out of the screen view
            float currEmailPosX = pressedObject.transform.position.x;
            if (currEmailPosX > maxScreenBoundary || currEmailPosX < minScreenBoundary)
            {
                RemoveEmail();
            }

            // If we are pressing a new position on the screen
            if(prevTouchCoord.x != SwipeDetection.instance.Position.x)
            {
                // Calculate the distance between the prev touch and the new one.
                float touchDistance = Mathf.Abs(prevTouchCoord.x - SwipeDetection.instance.Position.x);

                // Update the new email position destination based on which side(left or right) a touch was pressed
                destinationPos = (prevTouchCoord.x > SwipeDetection.instance.Position.x)
                    ? new Vector2(destinationPos.x - touchDistance, destinationPos.y)
                    : new Vector2(destinationPos.x + touchDistance, destinationPos.y);

                // Update prevTouchCoord to the new one touch coordinates
                prevTouchCoord = SwipeDetection.instance.Position;
            }

            // Interpolate(Move) the email position to the calculated destination position
            Vector2 newPos = Vector2.SmoothDamp(pressedObject.transform.position, destinationPos, ref velocity, mouseDragSpeed);
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
        RemoveEmail();
    }

    private void RemoveEmail()
    {
        if (selectedEmail != null)
        {
            Debug.Log("Remove Email: " + selectedEmail);
            Destroy(selectedEmail);
            selectedEmail = null;
        }
    }
}
