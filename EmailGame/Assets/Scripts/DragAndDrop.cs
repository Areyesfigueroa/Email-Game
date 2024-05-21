using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    [Tooltip("How fast the email catches up to the touch press")]
    public float mouseDragSpeed = .1f;
    [Tooltip("How far the email should go offscreen before deleting. 0 means halfway of the screen by default.")]
    public float screenBoundaryOffset = 0;

    // Used to simulate physics when dragging an email
    private Vector2 velocity = Vector2.zero;

    // Email selected for drag and drop
    private GameObject selectedEmail;

    // Edges of the screen coordinates on the x axis. It is used to determine if an email should be destroyed.
    private float maxScreenBoundary = 0;
    private float minScreenBoundary = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Subscribed functions to the player input actions
        SwipeDetection.instance.swipePerformed += OnEmailSwipe;
        SwipeDetection.instance.pressPerformed += OnEmailPress;
    }

    private void OnEmailPress(Vector2 currentPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(currentPos, Vector2.zero);

        if (hit.collider != null) {
            selectedEmail = hit.collider.gameObject;

            if (maxScreenBoundary == 0)
            {
                maxScreenBoundary = selectedEmail.transform.parent.position.x * 2;
            }

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

            // Check if the email is more than halfway out of the screen view from the right side
            float currEmailPosX = pressedObject.transform.position.x;
            if (currEmailPosX > (maxScreenBoundary - screenBoundaryOffset))
            {
                RemoveEmail();
            }

            // Check if email is more than halfway out of the screen view from the left side
            if(currEmailPosX < (minScreenBoundary + screenBoundaryOffset))
            {
                Debug.Log("Reply action TBD");
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
            pressedObject.transform.position = newPos;
            yield return null;
        }

        // Email is not longer selected since the press value is 0
        selectedEmail = null;

        // Reset email position when letting go
        while (SwipeDetection.instance.PressValue == 0 && pressedObject != null && Mathf.Round(pressedObject.transform.position.x) != initEmailPos.x)
        {
            Vector2 newPos = Vector2.SmoothDamp(pressedObject.transform.position, initEmailPos, ref velocity, mouseDragSpeed);
            pressedObject.transform.position = newPos;
            yield return null;
        }

        Debug.Log("Drag and Drop Coroutine ended");
    }

    private void OnEmailSwipe(Vector2 direction, Vector2 currentPos)
    {
        Debug.Log("Swipe Detected");
        // Debug.Log("Direction: " + direction.x);
        // Debug.Log("Position: " + currentPos.x);
        //Debug.Log(Mouse.current.position.ReadValue());
        // RemoveEmail();
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
