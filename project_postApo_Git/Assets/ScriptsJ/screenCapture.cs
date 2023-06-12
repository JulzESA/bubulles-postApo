using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenCapture : MonoBehaviour

{     
    int PCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            ScreenCapture.CaptureScreenshot("screenshot" + PCounter + ".png", 4);
            Debug.Log("A screenshot was taken!") ;
            PCounter++;

        }
    }
}
