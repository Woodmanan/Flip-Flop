using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.Rendering.PostProcessing;

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

    int nextChange;
    string nextName;

    //Some specific weird stuff vars
    [SerializeField]
    PhysicsMaterial2D bounceMaterial;

    private PostProcessVolume effect;
    private ChromaticAberration AbLayer = null;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = Time.time;
        changeText = "If you're seeing this, there's a bug.";
        pickNext();
        effect = GetComponent<PostProcessVolume>();
        effect.profile.TryGetSettings(out AbLayer);
    }

    // Update is called once per frame
    void Update()
    {
        if (dispChange)
        {
            updateField.text = changeText;
            updateField.fontSize = 40;
            if ((int) (2 *Time.time) % 2 == 0)
            {
                updateField.color = Color.yellow;
            }
            else
            {
                updateField.color = Color.red;
            }
        }
        else
        {
            updateField.text = nextName + " change in: " + (int) (timePerChange - (Time.time - currentTime));
            updateField.fontSize = 30;
            updateField.color = Color.yellow;
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
        int choice = nextChange;
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
            int swapNum = Random.Range(0, GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControlBeta>().getNumSwaps());
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
            {
                int choiceTwo = Random.Range(1, 3);
                changeText = go.GetComponent<PlayerControlBeta>().swapByInt(swapNum, choiceTwo);
            }
        }
        else if (choice == 2)
        {
            secondChoice = Random.Range(0, 3);
            switch (secondChoice)
            {
                case 0:
                    GameObject tiles = GameObject.FindGameObjectWithTag("Ground");
                    tiles.GetComponent<TilemapCollider2D>().sharedMaterial = bounceMaterial;
                    changeText = "Rubber tiles! All obstacles are now bouncy";
                    if (Random.Range(0, 2) == 0)
                    {
                        changeText = "Heavy slime! Bouncy tiles + Gravity up";
                        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
                        {
                            go.GetComponent<PlayerControlBeta>().doubleGravity();
                        }
                    }
                    break;
                case 1:
                    foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
                    {
                        go.GetComponent<PlayerControlBeta>().halfGravity();
                        go.GetComponent<PlayerControlBeta>().halfGravity();
                    }
                    changeText = "MOOOOOON";
                    break;
                case 2:
                    changeText = "Slippery Controls!";
                    foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
                    {
                        go.GetComponent<PlayerControlBeta>().modSlide(15);
                    }
                    break;
            }
        }
        else if (choice == 3)
        {
            secondChoice = Random.Range(0, 3);
            switch (secondChoice)
            {
                case 0:
                    changeText = "Surprise!";
                    Color[] toSwitch = getRandomColors();
                    int count = 0;
                    foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
                    {
                        go.GetComponent<PlayerControlBeta>().setColor(toSwitch[count]);
                        count++;
                    }
                    break;
                case 1:
                    changeText = "BBLLUURR";
                    AbLayer.intensity.value = 1f;
                    break;
                case 2:
                    changeText = "Bad Rendering!";
                    GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
                    cam.GetComponent<Camera>().clearFlags = CameraClearFlags.Nothing;
                    if (Random.Range(0, 2) == 0)
                    {
                        AbLayer.intensity.value = 1f;
                        changeText = "Literally Tripping Balls";
                    }
                    break;
            }
        }
        pickNext();
    }

    void pickNext()
    {
        nextChange = Random.Range(0, 4);
        switch (nextChange)
        {
            case 0:
                nextName = "Camera";
                break;
            case 1:
                nextName = "Controller";
                break;
            case 2:
                nextName = "Physics";
                break;
            case 3:
                nextName = "Miscellaneous";
                break;
        }
    }

    Color[] getRandomColors()
    {
        Color[] list = new Color[8];
        list[0] = Color.black;
        list[1] = Color.white;
        list[2] = Color.red;
        list[3] = Color.green;
        list[4] = Color.blue;
        list[5] = Color.cyan;
        list[6] = Color.yellow;
        list[7] = Color.magenta;

        for (int i = 0; i < list.Length; i++)
        {
            Color hold = list[i];
            int rand = Random.Range(0, list.Length);
            list[i] = list[rand];
            list[rand] = hold;
        }

        return list;
    }

    //Turns off the display option
    private void returnToTimer()
    {
        dispChange = false;
    }
}
