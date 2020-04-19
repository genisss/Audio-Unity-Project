using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile2 : MonoBehaviour {

	public float speed;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Translate (Vector3.forward * Time.deltaTime * speed);
	}
	// Aquí se destruye el proyectil
	private void OnTriggerEnter(Collider other) {

		if (other.tag == "Enemy")
		{
			Destroy (gameObject);
		}
		else
		{
			Destroy (gameObject, 3f);
		}
	}
}
