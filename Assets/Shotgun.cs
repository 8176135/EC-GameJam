using System.Collections;
using System.Collections.Generic;
using EZCameraShake;
using UnityEngine;
using UnityEngine.UI;

public class Shotgun : MonoBehaviour
{

	Rigidbody2D rb;
	[SerializeField] Text shellCountDisplay;
	[SerializeField] GameObject explosiveOrange;
	[SerializeField] GameObject bullet;

	[SerializeField] AudioSource shotSfx;

	[SerializeField] float recoilForce;

	[SerializeField] int numberOfBullets = 5;
	[SerializeField] float spreadDegrees = 20;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	[SerializeField] int shells = 1;

	bool lastFire = false;
	// Update is called once per frame
	void Update () {
		float firing = Input.GetAxis("Fire2");

		if (firing > 0)
		{
			if (!lastFire)
			{
				lastFire = true;
				if (shells > 0)
				{
					shells--;
					var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					mousePos.z = 0;
					var rot = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
					rb.AddForce((transform.position - mousePos).normalized * recoilForce, ForceMode2D.Impulse);
					for (int i = 0; i < numberOfBullets; i++)
					{
						Instantiate(bullet, transform.position + (mousePos - transform.position).normalized * 5f, rot * Quaternion.Euler(0,0, ((i * spreadDegrees) / numberOfBullets) - spreadDegrees / 2));
					}
					shotSfx.Play();
					CameraShaker.Instance.ShakeOnce(30, 20, 0.1f, 0.2f);

				}
			}
		}
		else
		{
			lastFire = false;
		}
		shellCountDisplay.text = shells.ToString();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Shell"))
		{
			shells++;
			Instantiate(explosiveOrange, other.transform.position, Quaternion.identity);
			Destroy(other.gameObject);
		}
	}
}
