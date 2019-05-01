using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure; // Required in C#

public class ReadyUpController : MonoBehaviour {
    //So long, and thanks for all the fish!
    [SerializeField]
    private string playerNum;
    [SerializeField] private PlayerIndex ControllerNum;
    private GamePadState inp;

    [SerializeField]
    private bool ready;

    [SerializeField] private GameObject box;
    [SerializeField] private float speed;
    private float rot;

    [SerializeField] private ParticleSystem fun;

    private float timeStore;
    

    private bool readyCoolDown;
    private string jump;
    // Use this for initialization
    void Start ()
    {
        readyCoolDown = false;
        jump = "P" + playerNum + "Jump";
        print("ON STARTUP:");
        print("Jump is: " + jump);
        rot = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        inp = GamePad.GetState(ControllerNum);
		if((inp.Buttons.Start == ButtonState.Pressed) && !ready && !readyCoolDown)
        {
            ready = true;
            Invoke("removeReadyCoolDown", .4f);
            readyCoolDown = true;
            GetComponent<Text>().text = "Player " + playerNum + ": Ready";
            GetComponent<Text>().color = new Color(0, 255, 0);
            GetComponent<Text>().fontSize = GetComponent<Text>().fontSize * 2;
            GameObject.FindGameObjectWithTag("MainManager").GetComponent<SceneSetup>().setState(int.Parse(playerNum), true);
            GetComponent<AudioSource>().enabled = false;
            GetComponent<AudioSource>().enabled = true;
            fun.Play();

            timeStore = Time.time;

            GetComponent<FadeIn>().enabled = false;

            print("Hey this worked");
            speed = ((Random.Range(0, 2) * -2) + 1) * speed;
        }
        else if ((inp.Buttons.Start == ButtonState.Pressed) && ready && !readyCoolDown)
        {
            ready = false;
            readyCoolDown = true;
            Invoke("removeReadyCoolDown", .4f);
            GetComponent<Text>().text = "Player " + playerNum + ": Not Ready";
            GetComponent<Text>().color = Color.white;
            GetComponent<Text>().fontSize = GetComponent<Text>().fontSize / 2;
            GameObject.FindGameObjectWithTag("MainManager").GetComponent<SceneSetup>().setState(int.Parse(playerNum), false);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (ready)
        {
            rot += speed;
            box.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, rot);
            transform.rotation = Quaternion.Euler(0, 0, 25 * Mathf.PingPong(Time.time, 2) - 25);
            float size = (Mathf.PingPong(Time.time, 1)/2) + .5f;
            transform.localScale = new Vector3(size, size, 1);
        }
    }

    void removeReadyCoolDown()
    {
        readyCoolDown = false;
    }
}
