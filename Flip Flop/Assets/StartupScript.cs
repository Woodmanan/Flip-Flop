using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartupScript : MonoBehaviour
{
    public GameObject panel;
    public Text UIslot;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("MainManager").GetComponent<SceneSetup>().updatePlayers(panel, UIslot);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
