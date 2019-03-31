using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TriggerEndScene : MonoBehaviour
{
    public Text UISlot;
    public GameObject levelOverPanel;
    private SceneSetup _sceneSetUp;


    void Start()
    {
        _sceneSetUp = GameObject.FindGameObjectWithTag("MainManager").GetComponent<SceneSetup>();
        levelOverPanel.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.tag.Equals("Player"))
        {
            return;
        }

        Debug.Log("hit hit hit!!");
        GetComponent<BoxCollider2D>().enabled = false;

        int winnerNum = other.gameObject.GetComponent<PlayerControlBeta>().getPlayerNum();

        print("Setting player " + winnerNum + " as athe winner!");
        _sceneSetUp.setWinner(winnerNum);




    }



}

