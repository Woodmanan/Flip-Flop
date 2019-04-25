using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMovingMyShit : MonoBehaviour
{
    private Transform start;
    // Start is called before the first frame update
    void Start()
    {
        start = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = start.position;
        transform.rotation = start.rotation;
    }
}
