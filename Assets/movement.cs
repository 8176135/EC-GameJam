using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class movement : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    [SerializeField] float upwardForce = 50;
    [SerializeField] float increaseRate = 80;
    [SerializeField] float maximumHeat = 100;

    [SerializeField] float heatDropRate = 20;
    float currentHeat = 0;

    // Update is called once per frame
    void Update()
    {
        float upwards = Input.GetAxis("Vertical");


        currentHeat = Mathf.Clamp(currentHeat + (upwards * increaseRate - heatDropRate) * Time.deltaTime, 0, maximumHeat);
        sr.color = new Color(1, 1 - currentHeat / maximumHeat, 1 - currentHeat / maximumHeat);

        rb.AddForce(Vector2.up * currentHeat * upwardForce * Time.deltaTime);

    }

    void FixedUpdate()
    {
    }
}