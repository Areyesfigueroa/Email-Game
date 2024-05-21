using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EmailManager : MonoBehaviour
{
    public GameObject emailPrefab;
    private readonly int MAX_EMAILS_COUNT = 7; // Max number of emails that can be shown on screen.

    private readonly int instantiateTimerSec = 2;
    private int emailCounter = 0;
    public int EmailCounter { get { return emailCounter; } }

    private bool isCoroutineRunning = false;

    private void Update()
    {
        if (transform.childCount < MAX_EMAILS_COUNT && !isCoroutineRunning)
        {
            Debug.Log("Initiate Coroutine");
            StartCoroutine(InstantiateOverTime(instantiateTimerSec));
        }
    }

    private void InstantiateEmail()
    {
        GameObject emailObj = Instantiate(emailPrefab, transform.position, transform.rotation);
        emailObj.transform.SetParent(gameObject.transform, false);

        // Update email counter, testing purposes only
        emailCounter++;
    }

    private IEnumerator InstantiateOverTime(int seconds)
    {
        while (transform.childCount < MAX_EMAILS_COUNT)
        {
            isCoroutineRunning = true;

            InstantiateEmail();

            yield return new WaitForSeconds(seconds);
        }

        isCoroutineRunning = false;
        Debug.Log("Coroutine Ended");
    }
}
