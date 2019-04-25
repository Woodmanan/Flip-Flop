using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [SerializeField] private float duration;
    Text tex;
    Color store;
    // Start is called before the first frame update
    void Start()
    {
        tex = GetComponent<Text>();
        store = tex.color;
        tex.color = Color.black;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad < duration)
        {
            tex.color = Color.Lerp(Color.black, store, Time.timeSinceLevelLoad / duration);
        }
    }
}
