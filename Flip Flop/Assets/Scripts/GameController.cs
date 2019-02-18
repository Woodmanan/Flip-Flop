using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //This class watches the game to make sure that we end the game when we need to!

    GameObject[] players;
    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        int count = 0;
        GameObject winner = null;
        foreach (GameObject go in players)
        {
            if (go != null && go.activeSelf == true)
            {
                count++;
                winner = go;
            }
        }
        if (count == 1)
        {
            GameObject control = GameObject.FindGameObjectWithTag("MainManager");
            print(winner.name + " wins!");
            control.GetComponent<SceneSetup>().setWinner(winner.GetComponent<PlayerController>().getPlayerNum());
            Destroy(this.gameObject);
        }
        else if (count == 0)
        {
            GameObject control = GameObject.FindGameObjectWithTag("MainManager");
            print("No one wins!");
            control.GetComponent<SceneSetup>().setWinner(0);
            Destroy(this.gameObject);
        }
    }

    
}
