using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuiteOnClick : MonoBehaviour
{
   public void Quite()
    {   
        UnityEditor.EditorApplication.isPlaying = false;

    }
}
