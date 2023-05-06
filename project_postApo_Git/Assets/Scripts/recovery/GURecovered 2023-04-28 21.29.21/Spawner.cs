using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public int amount;

    public GameObject[] particles;

    void Start() {
        particles = new GameObject[amount];

        for (var i = 0; i < amount; i++) {
            particles[i] = Instantiate(prefab, new Vector3(i * 2.0f, 0, 0), Quaternion.identity);
        }
    }
}
