using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	static public PlayerScript me;
	public float spd;
	public float hp;
	public GameObject currentMat;

	private void Awake()
	{
		me = this;
	}

	private void Update()
	{
		// simple movement for now
		if (Input.GetKey(KeyCode.W))
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + spd * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.S))
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - spd * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.A))
		{
			transform.position = new Vector3(transform.position.x - spd * Time.deltaTime, transform.position.y, transform.position.z);
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.position = new Vector3(transform.position.x + spd * Time.deltaTime, transform.position.y, transform.position.z);
		}

		// look at mouse pos
		transform.LookAt(MouseManager.me.mousePos);
	}
}