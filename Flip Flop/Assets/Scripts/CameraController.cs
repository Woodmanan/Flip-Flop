using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //These are the main variables in this thing.
    //Basespeed determines how fast the camera crawls forward with no extra input
    //When people speed up, the camera goes to 3xBasespeed and then 6xBasespeeed (which should equal player speed)
    //If you make a bigger camera and speed up the players, don't forget this!
    [SerializeField]
    float baseSpeed;
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
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Speed Modification
        float dist = -5;
        GameObject first = null;
        foreach (GameObject go in players)
        {
            if (go != null)
            {
                if (go.transform.position.x - transform.position.x > dist)
                {
                    dist = go.transform.position.x - transform.position.x;
                    first = go;
                }
            }
        }

        speed = baseSpeed / 6;

        //Updating speed, based upon farthest player position
        print("Dist is " + dist);
        if (dist < 0)
        {
            print("Smol");
            speed = baseSpeed / 6;
        }
        else if (dist < 3)
        {
            print("Med");
            speed = baseSpeed / 2;
        }
        else if (dist >= 3)
        {
            print("Fas");
            speed = baseSpeed;
        }
        
        
        transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;



        //Rotation Updating
        if (rotating)
        {
            rotation += rotationMod;
            if (rotationMod > 0)
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

            transform.rotation = Quaternion.Euler(0, 0, rotation);
            
        }
    }

    //Set the camera rotating to a new rotation
    //Just call an angle, and the camera will automatically rotate to it.
    //Camera will automatically choose the fastest way to achieve it's rotation
    //Will accept values between [-720, infinity) as valid rotation values, and resulting rotation
    //will be between [0, 360)
    public void setRotation(int newRotation)
    {
        //First, figure out which way is the faster rotation

        //Set rotation and newRotation into [0, 359]
        rotation = (rotation + 720) % 360;
        newRotation = (newRotation + 720) % 360;


        int smaller;
        int larger;

        if (rotation.CompareTo(newRotation) < 0)
        {
            smaller = rotation;
            larger = newRotation;
        }
        else
        {
            smaller = newRotation;
            larger = rotation;
        }


        //Some weird math that checks to see if rotating up or down gets us there faster
        int upDist = Mathf.Abs(larger - smaller);
        int downDist = Mathf.Abs((larger - 360) - smaller);



        //Based upon our previous math, choose which way to modify our 
        if (upDist <= downDist)
        {
            //The closest way between the two is without crossing 0

            if (smaller == rotation)
            {
                //We need to rotate up!
                rotationMod = 1;
                rotationGoal = newRotation;
            }
            else
            {
                //We need to rotate down!
                rotationMod = -1;
                rotationGoal = newRotation;
            }
            
        }
        else
        {
            if (smaller == rotation)
            {
                //We need to modify our goal
                rotationMod = -1;
                rotationGoal = newRotation;
                rotation = rotation + 360;
            }
            else
            {
                rotationMod = 1;
                rotationGoal = newRotation + 360;
            }
        }

        rotating = true;

        print("Rotation has been set.");
        print("Rotating goal is: " + rotationGoal);
        print("RotationMod is: " + rotationMod);
        

    }

    public int getRotation()
    {
        return rotation;
    }

    public void modRotation(int mod)
    {
        setRotation(rotation + mod);
    }
}
