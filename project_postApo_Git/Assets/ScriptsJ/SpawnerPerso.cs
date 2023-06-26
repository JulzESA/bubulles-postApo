/*
 Dans ce script, toutes les particules sont générées en une seule fois lorsque la touche "p" est enfoncée car la fonction "CreateParticle()" est appelée dans la fonction 
"Update()" sans aucune condition.

Pour faire en sorte que les particules ne se génèrent pas toutes à la fois, vous pouvez utiliser une méthode de temporisation pour espacer les créations de particules. 
Par exemple, vous pouvez utiliser une coroutine pour attendre un certain temps avant de créer chaque particule.

Voici un exemple de code qui utilise une coroutine pour espacer les créations de particules. 

Dans cet exemple, la coroutine "CreateParticles()" est appelée lorsque la touche "p" est enfoncée. Dans la coroutine, chaque particule est créée avec une attente de 
0.1 seconde entre chaque création, ce qui espacera les créations de particules.

Vous pouvez ajuster la durée de l'attente selon vos besoins en modifiant la valeur passée à la fonction "WaitForSeconds()".
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySteer2D.Behaviors;

public class SpawnerPerso : MonoBehaviour
{
    public Transform _startPos;

    public GameObject prefab;       // objet qui va être multiplié
    public int amount;              // nombre de particules
    public GameObject[] particles;  // crée un tableau avec chaque particule dans une ligne

    public Transform[] steps;
    public Transform pathDAlternatif;

    public GameObject[] Collider;

    public int currentStep;
    public int pAccount;

    public GameObject CameraQuad;

    public AudioSource StartingMusic;
    public AudioSource Abis;
    public AudioSource B;
    public AudioSource Bbis;
    public AudioSource D;
    public AudioSource C1;
    public AudioSource C2;
    public AudioSource C3;


    int s;

    private void Start()
    {
        _startPos = GameObject.Find("GrandmasterGameobj").transform;
        StartingMusic.Play();
        s = 0;
        Abis.Play();

    }

    public void ChangeParticleColor(Color new_c)
    {
        for (int p=0; p < particles.Length; p++)
        {
            if (particles[p] != null)
            {
                SpriteRenderer s_r = particles[p].GetComponent<SpriteRenderer>();
                s_r.color = new_c;
            }
        }

        Renderer ca_r = CameraQuad.GetComponent<Renderer>();
        Material ca_m = ca_r.material;
        ca_m.color = new_c;
    }


    public void RunAnimatorController()
    {
        if (pAccount == 0)
        {
            StartCoroutine(CreateParticles());
        }

        if (pAccount > 0)
        {
            NextStep();
        }

        NextMusic();
    }

    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            RunAnimatorController();
        }
    }


    IEnumerator CreateParticles()
    {
        particles = new GameObject[amount];

        for (var i = 0; i < amount; i++)
        {
            particles[i] = Instantiate(prefab, new Vector3(_startPos.position.x, _startPos.position.y, _startPos.position.z), Quaternion.identity); //crée un nb i de prefab, sur un axe x de i*2 sans changer d'orientation

            yield return null;
            //yield return new WaitForSeconds(0.1f);
        }

        pAccount = (pAccount + 1);

    }

    void NextStep()
    {
        currentStep = (currentStep + 1) % steps.Length;
        
        // pour chaque bubulle
        for (var i = 0; i < amount; i++)
        {
            // on lui indique un nouveau chemin
            particles[i].GetComponent<PathFollowingController2D>()._pathRoot = steps[currentStep];
            if(i<amount/2 && currentStep == 4)
            {
                particles[i].GetComponent<PathFollowingController2D>()._pathRoot = pathDAlternatif;
            }
            particles[i].GetComponent<PathFollowingController2D>().AssignPath();
        }

        if (currentStep == 1)
           {
            Collider[0].SetActive(false);
           }

        if (currentStep == 2)
        {
            Collider[1].SetActive(false);
        }

        if (currentStep == 3)
        {
            Collider[2].SetActive(false);
        }

        if (currentStep == 4)
        {
            Collider[3].SetActive(false);
            Collider[4].SetActive(false);
        }
    }

    void NextMusic()
    {

        s = s + 1;
        Debug.Log("Valeur de S" + s);
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

