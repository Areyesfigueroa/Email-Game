using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EmailManager : MonoBehaviour
{
    public GameObject emailPrefab;
    private readonly int MAX_EMAILS_COUNT = 7; // Max number of emails that can be shown on screen.

    private int emailCounter = 0;
    public int EmailCounter { get { return emailCounter; } }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InstantiateOverTime(3));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Testo");
            InstantiateEmail();
        }
    }

    private void InstantiateEmail()
    {
        if (transform.childCount >= MAX_EMAILS_COUNT) return;

        GameObject emailObj = Instantiate(emailPrefab, transform.position, transform.rotation);
        emailObj.transform.SetParent(gameObject.transform, false);

        // Update email counter, testing purposes only
        emailCounter++;

    }

    private IEnumerator InstantiateOverTime(int seconds)
    {
        while (transform.childCount < MAX_EMAILS_COUNT)
        {
            InstantiateEmail();
            // Debug.Log(transform.childCount);
            yield return new WaitForSeconds(seconds);
        }
        Debug.Log("Coroutine Ended");
    }
}
