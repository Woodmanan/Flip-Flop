using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Incrementer : MonoBehaviour
{
    [SerializeField] private int count;
    private Text tex;
    private bool cooldown;
    [SerializeField] private GameObject controller;
    // Start is called before the first frame update
    void Start()
    {
        cooldown = false;
        if (count <= 0)
        {
            count = 1;
        }
        tex = GetComponent<Text>();
        tex.text = "-" + count + "-";
        controller.GetComponent<SceneSetup>().gamesToWin = count;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0 && !cooldown)
        {
            count++;
            cooldown = true;
            if (count > 10)
            {
                count = 10;
            }
            Invoke("removeCooldown", .3f);
            tex.text = "-" + count + "-";
            controller.GetComponent<SceneSetup>().gamesToWin = count;
        }
        else if (Input.GetAxis("Horizontal") < 0 && !cooldown)
        {
            count--;
            if (count == 0)
            {
                count = 1;
            }
            cooldown = true;
            Invoke("removeCooldown", .3f);
            tex.text = "-" + count + "-";
            controller.GetComponent<SceneSetup>().gamesToWin = count;
        }
        
    }

    private void removeCooldown()
    {
        cooldown = false;
    }
}
