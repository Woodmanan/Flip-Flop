using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMimic : MonoBehaviour
{
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Color nue = cam.backgroundColor;
        Color brandnue = new Color(1 - nue.r, 1 - nue.g, 1 - nue.b, 1);
        GetComponent<SpriteRenderer>().color = brandnue;
    }

    // Update is called once per frame
    void Update()
    {
        Color nue = cam.backgroundColor;
        Color brandnue = new Color(1 - nue.r, 1 - nue.g, 1 - nue.b, 1);
        GetComponent<SpriteRenderer>().color = brandnue;
    }
}
