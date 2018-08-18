using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RockSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] rockToSpawn;


    // Use this for initialization
    void Start()
    {
    }

    [SerializeField] public float rockSpawnCooldown = 1;
    [SerializeField] public float rockSpawnRandom = 0.3f;

    float actualSpawn = 0;

    // Update is called once per frame
    void Update()
    {
        if (!cameraMovement.move)
        {
            return;
        }

        actualSpawn -= Time.deltaTime;
        if (actualSpawn < 0) {
            actualSpawn = rockSpawnCooldown + Random.Range(-rockSpawnRandom, rockSpawnRandom);
            var rock = Instantiate(rockToSpawn[Random.Range(0, rockToSpawn.Length)], new Vector3(Random.Range(-50.0f, 50.0f), transform.position.y, transform.position.z), Quaternion.Euler(0,0,Random.Range(0,360.0f)));
            rock.transform.localScale *= Random.Range(0.5f, 1.1f);
            var rb = rock.GetComponent<Rigidbody2D>();
            rb.angularVelocity = 0;
            rb.velocity = Vector2.zero;
            rb.Sleep();
            StartCoroutine(InvokeMethod(rock));
        }
    }

    void SleepIt(GameObject rock)
    {
        rock.GetComponent<Rigidbody2D>().Sleep();
    }

    IEnumerator InvokeMethod(GameObject rock)
    {
        yield return new WaitForSeconds(0.1f);
        SleepIt(rock);
    }
}