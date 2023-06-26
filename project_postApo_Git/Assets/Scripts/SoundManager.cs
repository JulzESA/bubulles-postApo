using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //public AudioSource [] MySounds;
    public AudioSource StartingMusic;
    public AudioSource Abis;
    public AudioSource B;
    public AudioSource Bbis;
    public AudioSource D;
    public AudioSource C1;
    public AudioSource C2;
    public AudioSource C3;


    int s; 

    void Start()
    {
        StartingMusic.Play();
        s = 0;
        Abis.Play();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("space"))
        {
            print("space key was pressed");
            NextMusic();
        }
    }

    void NextMusic() {

        s = s + 1;
        if (s == 1)
        {
            StartingMusic.Stop();
            B.Play();
        }
        if (s == 2)
        {
            B.Stop();
            Bbis.Play();
            C1.Play();
        }
        if (s == 3)
        {
            Bbis.Play();
            C2.Play();
        }
        if (s == 4)
        {
            Bbis.Play();
            C3.Play();
        }
        if (s == 5)
        {
            Bbis.Stop();
            D.Play();
        }
    }

}
