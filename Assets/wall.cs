using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour
{
    SpriteRenderer sr;

    [SerializeField] GameObject[] wallList;

    [SerializeField] int numberOfWalls;

    [SerializeField] int wallHeight = 60;

    [SerializeField] bool flip = false;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < numberOfWalls; i++)
        {
            var wall = Instantiate(wallList[Random.Range(0, wallList.Length)],transform.position + Vector3.up * wallHeight * i, flip ? Quaternion.Euler(0,0,180) : Quaternion.identity);
            wall.transform.SetParent(transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}