using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayScript : MonoBehaviour
{
    void Start()
    {
        Debug.Log("display connected to Unity: " + Display.displays.Length);

        for (int d = 1; d < Display.displays.Length; d++)
        {
            Debug.Log("display id: " + d + " - " + Display.displays[d].renderingWidth);
            Display.displays[d].Activate();
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
