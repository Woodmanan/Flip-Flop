using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyUpController : MonoBehaviour {

    [SerializeField]
    private string playerNum;

    [SerializeField]
    private bool ready;


    private bool readyCoolDown;
    private string jump;
    // Use this for initialization
    void Start ()
    {
        readyCoolDown = false;
        jump = "P" + playerNum + "Jump";
        print("ON STARTUP:");
        print("Jump is: " + jump);
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetAxis(jump) > 0 && !ready && !readyCoolDown)
        {
            ready = true;
            Invoke("removeReadyCoolDown", .4f);
            readyCoolDown = true;
            GetComponent<Text>().text = "Player " + playerNum + ": Ready";
            GetComponent<Text>().color = new Color(0, 255, 0);
            GetComponent<Text>().fontSize = GetComponent<Text>().fontSize * 2;
            GameObject.FindGameObjectWithTag("MainManager").GetComponent<SceneSetup>().setState(int.Parse(playerNum), true);
        }
        else if (Input.GetAxis(jump) > 0 && ready && !readyCoolDown)
        {
            ready = false;
            readyCoolDown = true;
            Invoke("removeReadyCoolDown", .4f);
            GetComponent<Text>().text = "Player " + playerNum + ": Not Ready";
            GetComponent<Text>().color = new Color(0, 0, 0);
            GetComponent<Text>().fontSize = GetComponent<Text>().fontSize / 2;
            GameObject.FindGameObjectWithTag("MainManager").GetComponent<SceneSetup>().setState(int.Parse(playerNum), false);
        }
    }

    void removeReadyCoolDown()
    {
        readyCoolDown = false;
    }
}
