using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TriggerEndScene : MonoBehaviour
{
    public Text UISlot;


    public void OnTriggerEnter2D(Collider2D other)
    {
        //UISlot.transform.parent.gameObject.SetActive(true);
       // UISlot.text = "Game Over!\n"+" Player "+other.GetComponent<PlayerController>().getPlayerNum() + " won!";
        GameObject.FindGameObjectWithTag("MainManager").GetComponent<SceneSetup>().setWinner(other.GetComponent<PlayerController>().getPlayerNum());
        GetComponent<BoxCollider2D>().enabled = false;       
    }

    private void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // Start is called before the first frame update
    void Start()
    {
        //UISlot.transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
