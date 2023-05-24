/*
 Dans ce script, toutes les particules sont g�n�r�es en une seule fois lorsque la touche "p" est enfonc�e car la fonction "CreateParticle()" est appel�e dans la fonction 
"Update()" sans aucune condition.

Pour faire en sorte que les particules ne se g�n�rent pas toutes � la fois, vous pouvez utiliser une m�thode de temporisation pour espacer les cr�ations de particules. 
Par exemple, vous pouvez utiliser une coroutine pour attendre un certain temps avant de cr�er chaque particule.

Voici un exemple de code qui utilise une coroutine pour espacer les cr�ations de particules. 

Dans cet exemple, la coroutine "CreateParticles()" est appel�e lorsque la touche "p" est enfonc�e. Dans la coroutine, chaque particule est cr��e avec une attente de 
0.1 seconde entre chaque cr�ation, ce qui espacera les cr�ations de particules.

Vous pouvez ajuster la dur�e de l'attente selon vos besoins en modifiant la valeur pass�e � la fonction "WaitForSeconds()".
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySteer2D.Behaviors;

public class SpawnerPerso : MonoBehaviour
{
    public Transform _startPos;

    public GameObject prefab;       // objet qui va �tre multipli�
    public int amount;              // nombre de particules
    public GameObject[] particles;  // cr�e un tableau avec chaque particule dans une ligne

    public Transform[] steps;
    public Transform pathDAlternatif;

    public GameObject[] Collider;

    public int currentStep;
    public int pAccount;

    private void Start()
    {
        _startPos = GameObject.Find("GrandmasterGameobj").transform;
    }


    void Update()
    {
        if (Input.GetKeyDown("p") && pAccount == 0)
        {
            StartCoroutine(CreateParticles());
        }

        if (Input.GetKeyDown("p") && pAccount>0)
        {
            NextStep();
        }
    }


    IEnumerator CreateParticles()
    {
        particles = new GameObject[amount];

        for (var i = 0; i < amount; i++)
        {
            particles[i] = Instantiate(prefab, new Vector3(_startPos.position.x, _startPos.position.y, _startPos.position.z), Quaternion.identity); //cr�e un nb i de prefab, sur un axe x de i*2 sans changer d'orientation

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
}

