using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorShifter : MonoBehaviour
{
    float timeStore;
    float passed;
    Color one;
    Color two;
    int count;

    [SerializeField] private Camera cam;
    [SerializeField] private float shiftTime;
    // Start is called before the first frame update
    void Start()
    {
        one = Color.black;
        two = Color.blue;
        count = 2;
        timeStore = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        passed = Time.time - timeStore;
        if (Time.time - timeStore < shiftTime)
        {
            cam.backgroundColor = Color.Lerp(one, two, passed / shiftTime);
        }
        else
        {
            timeStore = Time.time;
            //Time to switch!
            if (count == 0)
            {
                one = Color.green;
                two = Color.red;
                count++;
            }
            else if (count == 1)
            {
                one = Color.red;
                two = Color.blue;
                count++;
            }
            else
            {
                one = Color.blue;
                two = Color.green;
                count = 0;
            }
        }
    }
}
