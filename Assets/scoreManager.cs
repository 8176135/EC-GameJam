using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreManager : MonoBehaviour
{

	[SerializeField] Text scoreText;

	float offset;
	// Use this for initialization
	void Start ()
	{
		offset = transform.position.y;
	}
	
	// Update is called once per frame
	void Update ()
	{
		scoreText.text = (transform.position.y - offset).ToString("0.0");
	}
}
