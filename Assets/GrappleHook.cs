using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    BoxCollider2D col;

    [SerializeField] float fireForce = 50;
    [SerializeField] GameObject piece;
    [SerializeField] int ropeNumber = 5;

    GameObject[] ropePieces;

    public GrappleGun shooter;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();

        ropePieces = new GameObject[ropeNumber];
        {
            var ropeP = Instantiate(piece, Vector3.zero, Quaternion.identity);
            ropeP.transform.SetParent(transform);
            ropeP.transform.localScale = new Vector3(0.5f, 1f);
            ropeP.transform.position = transform.position + transform.up.normalized * -2.5f;
            ropeP.transform.rotation = transform.rotation;
            ropeP.GetComponent<HingeJoint2D>().connectedBody = rb;
            ropePieces[0] = ropeP;
        }

        for (int i = 1; i < ropePieces.Length; i++)
        {
            var ropePInner = Instantiate(piece, Vector3.zero, Quaternion.identity);
            ropePInner.transform.SetParent(transform);
            ropePInner.transform.localScale = new Vector3(0.5f, 1f);
            ropePInner.transform.position = ropePieces[i - 1].transform.position + transform.up.normalized * -0.25f * (i % 2 == 0 ? -1 : 1);
            ropePInner.transform.rotation = ropePieces[i - 1].transform.rotation;
            ropePInner.transform.Rotate(0, 0, 180);
            ropePInner.GetComponent<HingeJoint2D>().connectedBody = ropePieces[i - 1].GetComponent<Rigidbody2D>();
            ropePieces[i] = ropePInner;
        }

        //ropePieces[ropePieces.Length - 1].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        rb.AddForce(transform.up * fireForce, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.SetParent(other.transform);
        shooter.AttachHinge(gameObject);
    }
}