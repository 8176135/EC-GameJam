using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cameraMovement : MonoBehaviour
{

	[SerializeField] float moveSpeed = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position += new Vector3(0, Time.deltaTime * moveSpeed, 0);
		if (Input.GetButtonDown("Fire3"))
		{
			SceneManager.LoadScene(0);
		}
	}
}
