using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAround : MonoBehaviour
{
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = (Random.Range(3, 7) * 5) * (Random.Range(0, 2) * -2 + 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
        if (transform.position.x > 75 || transform.position.x < -75)
        {
            speed = -speed;
        }
    }
}
