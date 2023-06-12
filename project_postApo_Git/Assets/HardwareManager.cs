using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardwareManager : MonoBehaviour
{

    public OSC osc;
    const int TAG_ACTION = 1;
    const int BTN_ACTION = 2;
    const int POTAR_ACTION = 3;
    const int BEAM_ACTION = 4;
    const int GET_POTAR_ACTION = 5;
    const int SET_POTAR_ACTION = 6;
    const int VALUE_NO_TAG = -2;

    public List<string> slotName = new List<string>();
    public List<int> currentTags = new List<int>();
    public List<int> potarValues = new List<int>();

    private bool wasComplete = false;

    public ColorChanger ColorCh;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i< slotName.Count; i++)
        {
            currentTags.Add(VALUE_NO_TAG);
            potarValues.Add(0);
        }
        osc.SetAllMessageHandler(recieve);
    }
    public void recieve(OscMessage msg) {
        int action = msg.GetInt(0);
        int value = msg.GetInt(1);
        print(msg.ToString());
        Debug.Log("value of the msg is " + value);
        switch (action){
            case TAG_ACTION:

                ColorCh.UpdateColors(value);
                
                {
                    OscMessage msgOut = new OscMessage();
                    msgOut.address = msg.address;
                    msgOut.values.Add(POTAR_ACTION);
                    msgOut.values.Add(value == VALUE_NO_TAG ? 0 : 1);
                    osc.Send(msgOut);
                    print(msgOut.ToString());
                }

                {
                    int id = slotName.FindIndex(d => ("/" + d) == msg.address);
                    if (id >= 0 && id < currentTags.Count)
                    {
                        currentTags[id] = value;
                    }
                }

                bool isComplete = true;
                for (int i = 0; i <  currentTags.Count; i ++) {
                    if (currentTags[i] == VALUE_NO_TAG) isComplete = false;
                }

                if (isComplete) {
                    // SEND TURN BTN ON
                    OscMessage msgOut = new OscMessage();
                    msgOut.address = "/Norbert";
                    msgOut.values.Add(BTN_ACTION);
                    msgOut.values.Add(1);
                    osc.Send(msgOut);
                    print(msgOut.ToString());
                }

                if (wasComplete && !isComplete) {
                    // SEND TURN BTN OFF
                    OscMessage msgOut = new OscMessage();
                    msgOut.address = "/Norbert";
                    msgOut.values.Add(BTN_ACTION);
                    msgOut.values.Add(0);
                    osc.Send(msgOut);
                    print(msgOut.ToString());
                }
                wasComplete = isComplete;

            break;

            case BEAM_ACTION:
                foreach(string name in slotName)
                {
                    OscMessage msgOut = new OscMessage();
                    msgOut.address = "/"+ name;
                    msgOut.values.Add(GET_POTAR_ACTION);
                    msgOut.values.Add(0);
                    osc.Send(msgOut);
                    print(msgOut.ToString());
                }
                break;
            case SET_POTAR_ACTION :
                {
                    int id = slotName.FindIndex(d => ("/" + d) == msg.address);
                    if (id >= 0 && id < potarValues.Count)
                    {
                        potarValues[id] = value;
                    }
                }
                break;
            default:

                break;
        }

    }
}
