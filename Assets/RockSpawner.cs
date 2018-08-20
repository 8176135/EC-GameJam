using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RockSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] rockToSpawn;

    [SerializeField] GameObject shield;
    [SerializeField] float shieldChance = 1.0f;

    [SerializeField] GameObject shell;
    [SerializeField] float shellChance = 1.0f;

    [SerializeField] float powerUpChance = 0.05f;

    [SerializeField] float powerUpSpawnCooldown = 5;
    [SerializeField] float powerUpSpawnCooldownInst = 12;

    // Use this for initialization
    void Start()
    {
    }

    [SerializeField] public float rockSpawnCooldown = 1;
    [SerializeField] public float rockSpawnRandom = 0.3f;
    [SerializeField] public float spawnTries = 5;
    

    float actualSpawn = 0;

    // Update is called once per frame
    void Update()
    {
        if (!cameraMovement.move)
        {
            return;
        }

        actualSpawn -= Time.deltaTime;
        powerUpSpawnCooldownInst -= Time.deltaTime;
        if (actualSpawn < 0)
        {
            actualSpawn = rockSpawnCooldown + Random.Range(-rockSpawnRandom, rockSpawnRandom);
            var toSpawn = rockToSpawn[Random.Range(0, rockToSpawn.Length)];
            if (Random.Range(0.0f, 1.0f) < powerUpChance && powerUpSpawnCooldownInst < 0)
            {
                powerUpSpawnCooldownInst = powerUpSpawnCooldown;
                var range = Random.Range(0.0f, shellChance + shieldChance);
                range -= shieldChance;
                if (range < 0)
                {
                    toSpawn = shield;
                }
                else
                {
                    toSpawn = shell;
                }
            }
            var rScale = toSpawn.transform.localScale * Random.Range(0.5f, 1.1f);
            for (int i = 0; i < spawnTries; i++)
            {
                var spawnPos = new Vector3(Random.Range(-50.0f, 50.0f), transform.position.y, transform.position.z);

                //Physics2D.queriesStartInColliders = true;
                var cast = Physics2D.CircleCast(spawnPos, 1.25f * rScale.x, Vector2.down, 0.1f);
                if (cast.collider != null){continue;}

                var rock = Instantiate(toSpawn, spawnPos, Quaternion.Euler(0, 0, Random.Range(0, 360.0f)));
                rock.transform.localScale = rScale;
                var rb = rock.GetComponent<Rigidbody2D>();
                rb.angularVelocity = 0;
                rb.velocity = Vector2.zero;
                rb.Sleep();
                StartCoroutine(InvokeMethod(rock));
                break;
            }
        }
    }

    void SleepIt(GameObject rock)
    {
        if (rock)
        {
            rock.GetComponent<Rigidbody2D>().Sleep();
        }
    }

    IEnumerator InvokeMethod(GameObject rock)
    {
        yield return new WaitForSeconds(0.1f);
        SleepIt(rock);
    }
}