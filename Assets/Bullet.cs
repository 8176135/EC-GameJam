using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;

    GameObject renderer;

    [SerializeField] float startingVelocity = 100;

    [SerializeField] float lifetime = 5;

    [SerializeField] GameObject explosive;

    [SerializeField] GameObject explosiveGreen;
    [SerializeField] GameObject explosiveOrange;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        renderer = transform.GetChild(0).gameObject;
        rb.velocity = transform.up * startingVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0 || !renderer)
        {
            Destroy(gameObject);
            return;
        }

        renderer.transform.rotation = Quaternion.LookRotation(rb.velocity.normalized, Vector3.down) * Quaternion.Euler(0f, -90f, 90f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other || !other.gameObject || !other.gameObject.activeInHierarchy)
        {
            return;
        }
        if (other.CompareTag("Rock"))
        {
            Instantiate(explosive, other.transform.position, Quaternion.identity);
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Shield"))
        {
            Instantiate(explosiveGreen, other.transform.position, Quaternion.identity);
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Shell"))
        {
            // Chain explosion?
            Instantiate(explosiveOrange, other.transform.position, Quaternion.identity);
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}