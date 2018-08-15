using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("YOLO");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate()
	{
		float forwards = Input.GetAxis("Vertical");

		this.transform.position = new Vector3(this.transform.position.x , this.transform.position.y + forwards, this.transform.position.z);
	}
}
