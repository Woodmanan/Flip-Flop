using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeirdStuff : MonoBehaviour
{
    //This is the script that controls the gameplay changes!
    //Every 'timePerChange' seconds, the random function is called to make changes
    //To edit the time, editing the 'time' variable should be enough
    //To add more weird effects, edit the random function and change the counter
    [SerializeField]
    private float timePerChange;
    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - currentTime > timePerChange)
        {
            currentTime = Time.time;
            doRandomChange();
        }
    }

    //Generate a random number correlated with our change, then use it!
    //If you aren't seeing your change be implemented, it's probable that the random.range needs to be updated!
    private void doRandomChange()
    {
        //Gets random choice between 0 (inclusive) and x (exclusive)
        int choice = Random.Range(0, 4);
        if (choice == 0)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().setRotation(90);
        }
        else if (choice == 1)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().setRotation(270);
        }
        else if (choice == 2)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().setRotation(180);
        }
        else if (choice == 3)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().setRotation(45);
        }

    }
}
