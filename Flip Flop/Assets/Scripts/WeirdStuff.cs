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
            updateField.fontSize = 40;
        }
        else
        {
            updateField.text = "Time till next change: " + (int) (timePerChange - (Time.time - currentTime));
            updateField.fontSize = 30;
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
        int choice = Random.Range(0, 3);
        int secondChoice = 0;
        if (choice == 0)
        {
            secondChoice = Random.Range(0, 4);
            switch (secondChoice)
            {
                case 0:
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().modRotation((Random.Range(0, 2) * -2 + 1) * 45);
                    changeText = "Rotating by 45 degrees";
                    break;
                case 1:
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().modRotation((Random.Range(0, 2) * -2 + 1) * 90);
                    changeText = "Rotating by 90 degrees";
                    break;
                case 2:
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().modRotation((Random.Range(0, 2) * -2 + 1) * 135);
                    changeText = "Rotating by 135 degrees";
                    break;
                case 3:
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().modRotation(180);
                    changeText = "Rotating by 180 degrees";
                    break;
            }
        }
        else if (choice == 1)
        {
            int swapNum = Random.Range(0, GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().getNumSwaps());
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
            {
                changeText = go.GetComponent<PlayerController>().swapByInt(swapNum);
            }
        }
        else if (choice == 2)
        {
            secondChoice = Random.Range(0, 4);
            switch (secondChoice)
            {
                case 0:
                    GameObject tiles = GameObject.FindGameObjectWithTag("Ground");
                    tiles.GetComponent<TilemapCollider2D>().sharedMaterial = bounceMaterial;
                    changeText = "Rubber tiles! All obstacles are now bouncy";
                    break;
                case 1:
                    foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
                    {
                        go.GetComponent<PlayerController>().halfGravity();
                        go.GetComponent<PlayerController>().halfGravity();
                    }
                    changeText = "MOOOOOON";
                    break;
                case 2:
                    foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
                    {
                        go.GetComponent<PlayerController>().doubleGravity();
                    }
                    changeText = "Double Gravity!";
                    break;
            }
        }
    }


    //Turns off the display option
    private void returnToTimer()
    {
        dispChange = false;
    }
}
