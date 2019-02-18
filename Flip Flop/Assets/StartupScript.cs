using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("MainManager").GetComponent<SceneSetup>().updatePlayers();
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
