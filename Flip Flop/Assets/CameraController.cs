using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    float speed = .01f;

    //The rotation of our camera, along with the change we will increment it by.
    private int rotation;
    private int rotationGoal;
    private int rotationMod;
    private bool rotating;

    GameObject[] players;
    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        rotation = 0;
        rotationMod = 0;
        setRotation(90);
    }

    // Update is called once per frame
    void Update()
    {
        //Speed Modification
        float dist = 0;
        foreach (GameObject go in players)
        {
            if (go != null)
            {
                if (go.transform.position.x - transform.position.x > dist)
                {
                    dist = go.transform.position.x - transform.position.x;
                }
            }
        }

        //Updating speed, based upon farthest player position
        if (dist <= 0)
        {
            speed = .025f;
        }
        else if (dist < 3)
        {
            speed = .05f;
        }
        else if (dist >= 3)
        {
            speed = .1f;
        }
        transform.position += new Vector3(1, 0, 0) * speed;



        //Rotation Updating
        if (rotating)
        {
            print("We are rotating!");
            rotation += rotationMod;
            if (rotationMod < 0)
            {
                if (rotation > rotationGoal)
                {
                    rotation = rotationGoal;
                    rotating = false;
                }
            }
            else
            {
                if (rotation < rotationGoal)
                {
                    rotation = rotationGoal;
                    rotating = false;
                }
            }
            
        }
    }

    //Set the camera rotating to a new rotation
    public void setRotation(int newRotation)
    {
        /*
        //First, figure out which way is the faster rotation

        print("Initial Rotation: " + rotation + " + Initial new Rotation: " + newRotation);

        //Set rotation and newRotation into [0, 359]
        rotation = (rotation + 720) % 360;
        newRotation = (newRotation + 720) % 360;

        print("Modded up: " + rotation + ", " + newRotation);

        int smaller = Mathf.Min();

        //Some weird math that checks to see if rotating up or down gets us there faster
        int upDist = Mathf.Abs(rotation - newRotation);
        int downDist = Mathf.Abs(rotation - (newRotation - 360));


        //Based upon our previous math, choose which way to modify our 
        if (upDist >= downDist)
        {
            rotationMod = 1;
            rotationGoal = newRotation;
        }
        else
        {
            rotationMod = -1;
            rotationGoal = newRotation - 360;
        }

        rotating = true;

        print("Rotation has been set.");
        print("Rotating goal is: " + rotationGoal);
        print("RotationMod is: " + rotationMod);
        */

    }
}
