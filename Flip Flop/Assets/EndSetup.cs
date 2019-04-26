using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndSetup : MonoBehaviour
{
    [SerializeField] private Text winField;
    [SerializeField] private Text others;
    GameObject controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("MainManager");
        int winner = controller.GetComponent<SceneSetup>().getWinner();
        winField.text = "Player " + winner + " Wins!";
        others.text = controller.GetComponent<SceneSetup>().dump();
        Destroy(controller);
        Invoke("moveToStart", 7);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void moveToStart()
    {
        SceneManager.LoadScene(0);
    }
}
