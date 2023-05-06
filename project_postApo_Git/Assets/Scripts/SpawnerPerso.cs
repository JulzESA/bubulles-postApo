/*
 Dans ce script, toutes les particules sont générées en une seule fois lorsque la touche "p" est enfoncée car la fonction "CreateParticle()" est appelée dans la fonction "Update()" sans aucune condition.

Pour faire en sorte que les particules ne se génèrent pas toutes à la fois, vous pouvez utiliser une méthode de temporisation pour espacer les créations de particules. Par exemple, vous pouvez utiliser une coroutine pour attendre un certain temps avant de créer chaque particule.

Voici un exemple de code qui utilise une coroutine pour espacer les créations de particules. 

Dans cet exemple, la coroutine "CreateParticles()" est appelée lorsque la touche "p" est enfoncée. Dans la coroutine, chaque particule est créée avec une attente de 0.1 seconde entre chaque création, ce qui espacera les créations de particules.

Vous pouvez ajuster la durée de l'attente selon vos besoins en modifiant la valeur passée à la fonction "WaitForSeconds()".
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySteer2D.Behaviors;

public class SpawnerPerso : MonoBehaviour
{
    public GameObject prefab;       // objet qui va être multiplié
    public int amount;              // nombre de particules
    public GameObject[] particles;  // crée un tableau avec chaque particule dans une ligne

    int PCounter;                   // compte le nombre de particules

    public Transform[] steps;

    public int currentStep;

    private void Start()
    {
        StartCoroutine(CreateParticles());
    }


    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            NextStep();
        }
    }


    IEnumerator CreateParticles()
    {
        particles = new GameObject[amount];

        for (var i = 0; i < amount; i++)
        {
            particles[i] = Instantiate(prefab, new Vector3(-1.46f, 18.9f, 0), Quaternion.identity); //crée un nb i de prefab, sur un axe x de i*2 sans changer d'orientation
            //Debug.Log(particles[i].GetComponent<SteerForCohesion2D>().enabled);
            //particles[i].GetComponent<PathFollowingController2D>()._pathRoot = steps[0];

            
            //particles[i].GetComponent<PathFollowingController2D>().AssignPath();

            PCounter++;
            yield return null;
            //yield return new WaitForSeconds(0.1f); // attendre 0.5 seconde avant de créer la particule suivante
        }


    }

    void NextStep()
    {
        currentStep = (currentStep + 1) % steps.Length;

        // pour chaque bubulle
        for (var i = 0; i < amount; i++)
        {
            // on lui indique un nouveau chemin
            particles[i].GetComponent<PathFollowingController2D>()._pathRoot = steps[currentStep];
            particles[i].GetComponent<PathFollowingController2D>().AssignPath();
        }
    }
}

