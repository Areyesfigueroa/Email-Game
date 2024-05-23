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
        new EmailData("Me", "Boss", "I need to speak with you", "This is an email body"),
        new EmailData("Me", "Wife", "Im in love with .......", "This is an email body"),
        new EmailData("Me", "Prince Charles", "Save me by paying my ransom", "This is an email body"),
        new EmailData("Me", "The Home Depot", "Here is your receipt", "This is an email body"),
        new EmailData("Me", "Trek", "Your bike is due for its tire rotation", "This is an email body"),
        new EmailData("Me", "Cousin Vinny", "I need a good lawyer", "This is an email body"),
        new EmailData("Me", "Lover", "I LOVE YOU AND I DONT CARE WHO KNOWS IT", "This is an email body"),
        new EmailData("Me", "Professor Rachel", "We need to talk about your daughters grades", "This is an email body"),
        new EmailData("Me", "Matt Damon", "I won an Oscar and here's how you can too", "This is an email body"),
    };

    public EmailData[] EmailPool { get { return emails; } }


    // Start is called before the first frame update
    void Start()
    {
        int randomNum = UnityEngine.Random.Range(0, emails.Length - 1); // This will take from the list we created and pick a random message to appear
        Debug.Log(randomNum);

        // Temp, this is for showcase purposes. Illustrates how to dynamically change the email data.
        fromText.text = emails[randomNum].Sender; //+ counter;
        subjectText.text = emails[randomNum].Subject; //+ counter;
        bodyText.text = emails[randomNum].Body; //+ counter;
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