using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSelector : MonoBehaviour
{
    [SerializeField] private AudioClip One;
    [SerializeField] private AudioClip Two;

    // Start is called before the first frame update
    void Start()
    {
        if (Random.Range(0, 2) == 0)
        {
            GetComponent<AudioSource>().clip = One;
        }
        else
        {
            GetComponent<AudioSource>().clip = Two;
        }

        GetComponent<AudioSource>().enabled = false;
        GetComponent<AudioSource>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
