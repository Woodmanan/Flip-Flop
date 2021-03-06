﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float mod;
    private float rot;
    // Start is called before the first frame update
    void Start()
    {
        rot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (mod > 0)
        {
            if (rot < 45)
            {
                rot += mod;
            }
            else
            {
                mod = -1 * mod;
            }
        }
        else
        {
            if (rot > -45)
            {
                rot += mod;
            }
            else
            {
                mod = -1 * mod;
            }
        }

        transform.rotation = Quaternion.Euler(0, 0, rot);
        //float size = Mathf.PingPong(Time.time, 4f)/8 + .5f;
        //transform.localScale = new Vector3(size, size, size);
    }
}
