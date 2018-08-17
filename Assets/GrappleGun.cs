using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleGun : MonoBehaviour
{
    [SerializeField] GameObject grappleHook;

    GameObject currentHook;

    Coroutine currentCoroutine;

    DistanceJoint2D joint;
    Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        joint = GetComponent<DistanceJoint2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    float cooldown = 0;
    bool lastFireDown = false;

    // Update is called once per frame
    void Update()
    {
        float firing = Input.GetAxis("FireGrapple");
        cooldown -= Time.deltaTime;
        if (firing > 0)
        {
            if (!lastFireDown)
            {
                lastFireDown = true;
                if (currentHook != null)
                {
                    Destroy(currentHook);
                    joint.enabled = false;
                    if (currentCoroutine != null)
                    {
                        StopCoroutine(currentCoroutine);
                        currentCoroutine = null;
                    }
                }
                else if (cooldown <= 0)
                {
                    cooldown = 2;
                    var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePos.z = 0;
                    var rot = Quaternion.LookRotation(Vector3.forward, mousePos -transform.position);
                    Debug.Log(rot.eulerAngles);
                    grappleHook.GetComponent<GrappleHook>().shooter = this;

                    if (currentCoroutine != null)
                    {

                        StopCoroutine(currentCoroutine);
                        currentCoroutine = null;
                    }

                    currentHook = Instantiate(grappleHook, transform.position + ( mousePos - transform.position).normalized * 3.5f, rot);
                    currentHook.transform.SetParent(transform);
                }
            }
        }
        else
        {
            if (lastFireDown)
            {
                lastFireDown = false;
            }
        }
    }

    void PullInRope()
    {
        joint.distance = (currentHook.transform.position - transform.position).magnitude;
        rb.AddForce((currentHook.transform.position- transform.position).normalized * 2, ForceMode2D.Impulse);
    }

    IEnumerator InvokeMethod(Action method, float interval, int invokeCount)
    {
        for (int i = 0; i < invokeCount; i++)
        {
            method();

            yield return new WaitForSeconds(interval);
        }

    }

    public void AttachHinge(GameObject hook)
    {
        if (hook != currentHook)
        {
            Debug.LogError("PANIC, hooks don't equal");
        }
        joint.connectedBody = hook.GetComponent<Rigidbody2D>();
        joint.distance = (hook.transform.position - transform.position).magnitude;
        joint.enabled = true;
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(InvokeMethod(PullInRope, 0.1f, 40));
    }
}