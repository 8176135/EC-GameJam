using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cameraMovement : MonoBehaviour
{

	[SerializeField] float moveSpeed = 1;
	[SerializeField] float moveSpeedIncrease = 0.1f;
	public static bool move = false;

	[SerializeField] GameObject player;

	[SerializeField] GameObject wallOfDeath;
	[SerializeField] GameObject rockSpawner;


	RockSpawner spawner;
	// Use this for initialization
	void Start () {

		// set the desired aspect ratio (the values in this example are
		// hard-coded for 16:9, but you could make them into public
		// variables instead so you can set them at design time)
		float targetaspect = 5f / 6.0f;

		// determine the game window's current aspect ratio
		float windowaspect = (float)Screen.width / (float)Screen.height;

		// current viewport height should be scaled by this amount
		float scaleheight = windowaspect / targetaspect;

		// obtain camera component so we can modify its viewport
		Camera camera = Camera.main;

		// if scaled height is less than current height, add letterbox
		if (scaleheight < 1.0f)
		{
			Rect rect = camera.rect;

			rect.width = 1.0f;
			rect.height = scaleheight;
			rect.x = 0;
			rect.y = (1.0f - scaleheight) / 2.0f;

			camera.rect = rect;
		}
		else // add pillarbox
		{
			float scalewidth = 1.0f / scaleheight;

			Rect rect = camera.rect;

			rect.width = scalewidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scalewidth) / 2.0f;
			rect.y = 0;

			camera.rect = rect;
		}

//		Camera.main.orthographicSize = 80.22f / Screen.width * Screen.height;
//		transform.position = new Vector3(0, Camera.main.orthographicSize / 2, -10);
//		wallOfDeath.transform.localPosition = new Vector3(0,-Camera.main.orthographicSize + 3, 10);
//		rockSpawner.transform.localPosition = new Vector3(0,Camera.main.orthographicSize + 10, 10);

		rockSpawner = GetComponentInChildren<RockSpawner>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown("Fire3"))
		{
			move = false;
			SceneManager.LoadScene(0);
			return;

		}

		if (Input.GetButtonDown("FireGrapple"))
		{
			move = true;
		}

		if (!move)
		{
			return;
		}


		moveSpeed += moveSpeedIncrease * Time.deltaTime;
		transform.position += new Vector3(0, Time.deltaTime * moveSpeed, 0);

		if (player != null && player.transform.position.y - 18 > transform.position.y)
		{
			transform.position = new Vector3(transform.position.x, player.transform.position.y - 18, transform.position.z);
		}

	}
}
