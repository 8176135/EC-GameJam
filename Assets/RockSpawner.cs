using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RockSpawner : MonoBehaviour
{
    [SerializeField] GameObject rockToSpawn;


    // Use this for initialization
    void Start()
    {
    }

    [SerializeField] float rockSpawnCooldown = 1;
    [SerializeField] float rockSpawnRandom = 0.3f;

    float actualSpawn = 0;

    // Update is called once per frame
    void Update()
    {
        actualSpawn -= Time.deltaTime;
        if (actualSpawn < 0)
        {
            actualSpawn = rockSpawnCooldown + Random.Range(-rockSpawnRandom, rockSpawnRandom);
            Instantiate(rockToSpawn, new Vector3(Random.Range(-15.0f, 15.0f), transform.position.y, transform.position.z), Quaternion.identity);
        }
    }
}