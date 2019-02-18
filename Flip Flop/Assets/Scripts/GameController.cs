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
            print(winner.name + " wins!");
            Invoke("toNext", 2.5f);
        }
        else if (count == 0)
        {
            print("No one wins!");
            Invoke("toNext", 1.5f);
        }
    }

    private void toNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
