using System.Collections;
using System.Collections.Generic;
using EZCameraShake;
using UnityEngine;
using UnityEngine.Audio;

public class ShieldActive : MonoBehaviour
{
    [SerializeField] float powerupDuration = 20;

    public float activeDuration = 0;

    SpriteRenderer sr;
//    PolygonCollider2D col;

    [SerializeField] GameObject explosion;
    [SerializeField] GameObject explosionGreen;

    [SerializeField] AudioMixerSnapshot defaultMusic;
    [SerializeField] AudioMixerSnapshot invinciMusic;

    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
//        col = GetComponent<PolygonCollider2D>();
    }

    bool lastState = false;
    // Update is called once per frame
    void Update()
    {
        activeDuration = Mathf.Max(0,activeDuration - Time.deltaTime);
        if (activeDuration > 0)
        {
            sr.color = Color.HSVToRGB(activeDuration % 1.0f, 1.0f, 1.0f);
            if (!lastState)
            {
                lastState = true;
                invinciMusic.TransitionTo(1f);
            }
        }
        else
        {
            if (lastState)
            {
                lastState = false;
                defaultMusic.TransitionTo(1.0f);
            }
            sr.color = Color.white;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shield"))
        {
            activeDuration += powerupDuration;
            Instantiate(explosionGreen, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }

        if (activeDuration <= 0)
        {
            return;
        }

        if (other.CompareTag("Rock"))
        {
            Instantiate(explosion, other.transform.position, Quaternion.identity);
            CameraShaker.Instance.ShakeOnce(22, 17, 0.1f, 0.25f);
            scoreManager.bonus += 25;
            Destroy(other.gameObject);
        }
    }
}