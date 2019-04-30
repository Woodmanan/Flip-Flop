using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Maker : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject Hex;

    [SerializeField] int mod;
    int timecount = 0;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            timecount++;
        }
        if (timecount%mod == 1)
        {
            Vector3 position = transform.position + new Vector3(Random.Range(-9, 9),-5,1) ;

            GameObject newGameObject = Instantiate(Hex);

            newGameObject.transform.position = position;
        }
    }
}
