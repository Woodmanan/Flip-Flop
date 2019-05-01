using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    float timeSave;
    [SerializeField] GameObject panel;
    bool paused;
    bool cooldown;

    // Start is called before the first frame update
    void Start()
    {
        timeSave = 1;
        paused = false;
        cooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Pause") != 0 && !cooldown)
        {
            cooldown = true;
            Invoke("removeCooldown", .3f);
            if (paused)
            {
                unpause();
            }
            else
            {
                pause();
            }
        }
    }

    public void pause()
    {
        paused = true;
        timeSave = Time.timeScale;
        Time.timeScale = 0;
        panel.SetActive(true);
    }

    public void unpause()
    {
        paused = false;
        Time.timeScale = timeSave;
        panel.SetActive(false);
    }

    private void removeCooldown()
    {
        cooldown = false;
    }
}
