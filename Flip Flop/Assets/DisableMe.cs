using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMe : MonoBehaviour
{
    //I'm so sorry. This is such a hack. Never do this at home, kids.


    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.SetActive(false);
        Destroy(this.GetComponent<DisableMe>());
    }
}
