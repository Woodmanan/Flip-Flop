using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class WeirdStuff : MonoBehaviour
{
    //This is the script that controls the gameplay changes!
    //Every 'timePerChange' seconds, the random function is called to make changes
    //To edit the time, editing the 'time' variable should be enough
    //To add more weird effects, edit the random function and change the counter
    [SerializeField]
    private float timePerChange;
    private float currentTime;

    [SerializeField]
    Text updateField;
    private bool dispChange;
    private string changeText;

    //Some specific weird stuff vars
    [SerializeField]
    PhysicsMaterial2D bounceMaterial;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = Time.time;
        changeText = "If you're seeing this, there's a bug.";
    }

    // Update is called once per frame
    void Update()
    {
        if (dispChange)
        {
            updateField.text = changeText;
        }
        else
        {
            updateField.text = "Time till next change: " + (int) (timePerChange - (Time.time - currentTime));
        }

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
        dispChange = true;
        Invoke("returnToTimer", 4.0f);
        //Gets random choice between 0 (inclusive) and x (exclusive)
        int choice = Random.Range(0, 7);
        if (choice == 0)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().setRotation(90);
            changeText = "Camera rotation moving to " + 90;
        }
        else if (choice == 1)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().setRotation(270);
            changeText = "Camera rotation moving to " + 270;
        }
        else if (choice == 2)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().setRotation(180);
            changeText = "Camera rotation moving to " + 180;
        }
        else if (choice == 3)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().setRotation(45);
            changeText = "Camera rotation moving to " + 45;
        }
        else if ((choice == 4) || (choice == 5))
        {
            int swapNum = Random.Range(0, GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().getNumSwaps());
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
            {
                changeText = go.GetComponent<PlayerController>().swapByInt(swapNum);
            }
        }
        else if (choice == 6)
        {
            //Bouncy stuff!
            GameObject tiles = GameObject.FindGameObjectWithTag("Ground");
            tiles.GetComponent<TilemapCollider2D>().sharedMaterial = bounceMaterial;
            changeText = "Rubber tiles! All obstacles are now bouncy";
        }

    }


    //Turns off the display option
    private void returnToTimer()
    {
        dispChange = false;
    }
}
