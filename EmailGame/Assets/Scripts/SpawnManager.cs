using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] emailPrefab; //Allows us to assign a object for respawn
    private Vector3 spawnPos = new Vector3(-5, 1, 0); //Assigns new starting position for the email

    private float startDelay = 1;
    private float repeatRate = 1;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEmails", startDelay, repeatRate); // Having the emails start and continue on a set rate
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEmails ()
    {
        int emailIndex = Random.Range(0, emailPrefab.Length); //This is picking randomly which Prefab to spawn
        Instantiate(emailPrefab[emailIndex], spawnPos, emailPrefab[emailIndex].transform.rotation); //They will spawn with the same settings over and over again
    }
}
