using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;
using UnityEngine.UI;

public class Email : MonoBehaviour
{
    public Image profileImg; // TODO: Swap email from the assets folder
    public TMP_Text fromText;
    public TMP_Text subjectText;
    public TMP_Text bodyText;

    // This is for example purposes only, will be part of a different script, the emailDataPool
    private readonly EmailData[] emails = {
        new EmailData("Me", "Sender", "This is a new subject email", "This is an email body"),
    };


    // Start is called before the first frame update
    void Start()
    {

        int counter = transform.parent.GetComponent<EmailManager>().EmailCounter;
        // Temp, this is for showcase purposes. Illustrates how to dynamically change the email data.
        fromText.text = emails[0].Sender + counter;
        subjectText.text = emails[0].Subject + counter;
        bodyText.text = emails[0].Body + counter;
        // profileIcon.GetComponent<SpriteRenderer>().sprite = personIcon2;
    }

    // Update is called once per frame
    void Update()
    {
    }
}



// This is only for showcase purposes. This logic will be handled in the emailData pool script.
public class EmailData
{
    public string Recipient { get; }
    public string Sender { get; }
    public string Subject { get; }
    public string Body { get; }

    public EmailData(string recipient, string sender, string subject, string body) {
        Recipient = recipient;
        Sender = sender;
        Subject = subject;
        Body = body;
    }
}