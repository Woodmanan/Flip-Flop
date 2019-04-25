using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour
{
    [SerializeField] private Text tex;

    private float start;
    private bool waiting;
    // Start is called before the first frame update
    void Start()
    {
        start = 0;
        waiting = false;
        tex.color = new Color(tex.color.r, tex.color.g, tex.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Quit") > 0 && !waiting)
        {
            waiting = true;
            start = Time.time;
        }
        else if (Input.GetAxis("Quit") > 0 && waiting)
        {
            tex.color = new Color(tex.color.r, tex.color.g, tex.color.b, (Time.time - start) / 3);
            if (Time.time - start > 2)
            {
                tex.text = "Exiting...";
            }
            else if (Time.time - start > 1)
            {
                tex.text = "Exiting..";
            }
            else
            {
                tex.text = "Exiting.";
            }

            if (Time.time - start > 3)
            {
                SceneManager.LoadScene(0);
            }
        }
        else if (Input.GetAxis("Quit") == 0 && waiting)
        {
            waiting = false;
            tex.color = new Color(tex.color.r, tex.color.g, tex.color.b, 0);
        }
    }
}
