using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Reflection;
using UnityEditor;

public class OscReceiver : MonoBehaviour
{
    // Start is called before the first frame update
    public OSC osc;
    public string address;

    public bool active = false;
    private bool wasActive = false;

    public ColorChanger ColorCh;


    void Start()
    {
        osc.SetAddressHandler(address, messageReceive);
    }

    public void messageReceive(OscMessage msg)
    {
        //print(address + " said :" + msg.ToString());

        //ColorCh.UpdateColors(msg);
    }

    // Update is called once per frame
    void Update()
    {
        if (active && !wasActive)
        {
            OscMessage msg = new OscMessage();
            msg.address = address+"/led";
            osc.Send(msg);
        }
        wasActive = active;

    }
}
