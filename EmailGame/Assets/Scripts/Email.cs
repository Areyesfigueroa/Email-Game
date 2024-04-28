using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;

// Temporary Enum, will be handled in different script emailDataPool.
public enum EmailType
{
    Reply,
    Delete
}

public class Email : MonoBehaviour
{
    public GameObject sender;
    public GameObject subject;
    public GameObject body;
    public GameObject profileIcon;
    public Sprite personIcon2;

    // Temporary, this will be a random number generator to choose an email from the emailDataPool
    public int emailIndex = 0;

    // This is for example purposes only, will be part of a different script, the emailDataPool
    private readonly EmailData[] emails = {
        new EmailData("Me1", "Sender1", "This is a new subject email1", "This is an email body1", EmailType.Reply),
        new EmailData("Me2", "Sender2", "This is a new subject email2", "This is an email body2", EmailType.Delete)
    };

    // Start is called before the first frame update
    void Start()
    {
        // Temp, this is for showcase purposes. Illustrates how to dynamically change the email data.
        sender.GetComponentInChildren<TMP_Text>().text = emails[emailIndex].Sender;
        subject.GetComponentInChildren<TMP_Text>().text = emails[emailIndex].Subject;
        body.GetComponentInChildren<TMP_Text>().text = emails[emailIndex].Body;
        profileIcon.GetComponent<SpriteRenderer>().sprite = personIcon2;
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
    public EmailType Type { get; }

    public EmailData(string recipient, string sender, string subject, string body, EmailType type) {
        Recipient = recipient;
        Sender = sender;
        Subject = subject;
        Body = body;
        Type = type;
    }
}