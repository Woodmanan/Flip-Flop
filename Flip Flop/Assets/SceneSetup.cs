using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSetup : MonoBehaviour
{
    private bool[] playersOn;
    private int[] score;

    [SerializeField]
    private int maxScene;

    [SerializeField]
    private int gamesToWin;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
        playersOn = new bool[4];
        score = new int[4];
        if (GameObject.FindGameObjectsWithTag("MainManager").Length != 1)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Start1") > 0 && Input.GetAxis("Start2") > 0)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                moveToNew();
            }
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
            if (!playersOn[go.GetComponent<PlayerControlBeta>().getPlayerNum() - 1])
            {
                //Player is off!
                go.GetComponent<PlayerControlBeta>().getUISlot().SetActive(false);
                go.SetActive(false);
            }
        }
    }

    public void setWinner(int player)
    {
        if (player != 0)
        {
            player--;
            score[player]++;
            if (score[player] == gamesToWin)
            {
                print((player++) + " wins!");
                moveToEnd();
            }
        }
        //Time.timeScale = 0;
        Invoke("moveToNew", 3.0f);
    }

    public void disableScene()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            go.GetComponent<PlayerController>().enabled = false;
            Destroy(go.GetComponent<Rigidbody2D>());
        }
    }

    public void moveToNew()
    {
        SceneManager.LoadScene(5);
    }

    public void moveToEnd()
    {
        int finalScene = 1;
        SceneManager.LoadScene(finalScene);
    }

    public int getPlayerScore(int playerNum)
    {
        return score[playerNum - 1];
    }

    public int[] getPlayerScores()
    {
        return score;
    }

    public bool[] getPlayerStates()
    {
        return playersOn;
    }
}
