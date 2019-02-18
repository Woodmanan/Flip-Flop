using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSetup : MonoBehaviour
{
    private bool[] playersOn;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
        playersOn = new bool[4];
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("MainManager"))
        {
            if (!this.gameObject.Equals(go))
            {
                Destroy(go);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("P1Start") > 0 || Input.GetAxis("P2Start") > 0 || Input.GetAxis("P3Start") > 0 || Input.GetAxis("P4Start") > 0)
        {
            moveToNew();
        }
    }

    public void setState(int player, bool state)
    {
        print("Player " + (player - 1) + " is set to be " + state);
        playersOn[player - 1] = state;
    }

    public void updatePlayers()
    {
        print("Scene setup is turning the players on / off");
        for (int i = 0; i < 4; i++)
        {
            print("Player " + (i + 1) + " is set to be " + playersOn[i]);
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (!playersOn[go.GetComponent<PlayerController>().getPlayerNum() - 1])
            {
                //Player is off!
                go.SetActive(false);
            }
        }
    }

    public void moveToNew()
    {
        int maxScene = 4;
        maxScene++;
        SceneManager.LoadScene(Random.Range(2, maxScene));
    }
}
