using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorChanger : MonoBehaviour
{

    // private Renderer renderer;
    
    public Color[] emotions;// = { Ulysse, Marcher };
    public GameObject[] symboles;

    public GameObject ScriptCall;

    public SpawnerPerso SpawnerP;

    //SpriteRenderer rendererSpriteLiquid;
    //ScriptTrans1b TransitionsAnim;

    int c = 0;

    
    void Start()
    {
        /* TransitionsAnim = ScriptCall.GetComponent<ScriptTrans1b>();

         rendererSpriteLiquid = this.GetComponent<SpriteRenderer>();
        */
        for (int i = 0; i < symboles.Length; i++)
        {
            symboles[i].SetActive(false);
        }

       
    }


    
    public void UpdateColors(int color_id)//OscMessage msg)
    {
        /*
        string msg_all = msg.ToString();
        string msg_code = msg_all.Substring(msg_all.Length - 1);
        int test_id = msg.GetInt(1);
        Debug.Log("La valeur de test est " + test_id + " pour l'objet " + gameObject.name);

        int msg_id = int.Parse(msg_code);
        */

        if (color_id >= 0 && color_id < emotions.Length && color_id < symboles.Length)
        {

            SpawnerP.ChangeParticleColor(emotions[color_id]);

            List<GameObject> activeSym = new List<GameObject>();
            for (int i = 0; i < symboles.Length; i++)
            {
                if (symboles[i].activeSelf) activeSym.Add(symboles[i]);
            }

            while (activeSym.Count >= 5) {
                int randID = (int)Mathf.Floor(Random.value * activeSym.Count);
                activeSym[randID].SetActive(false);
                activeSym.RemoveAt(randID);
            }

            symboles[color_id].SetActive(true);
        }

        /*    if (test_id >= 0)
        {
            //Debug.Log("my msg_id is a problem ->   " + msg_id);

            if (emotions.Length >= test_id)
            {
                rendererSpriteLiquid = GetComponent<SpriteRenderer>();

                rendererSpriteLiquid.color = emotions[test_id];

                renderer.material.color = emotions[msg_id];
                symboles[test_id].SetActive(true);
                TransitionsAnim.ScoreIncreaser();
            }
        }*/
    }

    void Update()
    {
       /* symboles[c].SetActive(true);
    
        if (Input.GetKeyDown("c"))
        {
            c = c + 1;
            rendererSpriteLiquid.color = emotions[c];
        }*/
    }
}
