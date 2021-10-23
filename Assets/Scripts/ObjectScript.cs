using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
	Rigidbody rb;
	public float flyAmount;
	public GameObject enemy;
	public float sinkSpd;
	public bool sink;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (sink)
		{
			Sinking();
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		Vector3 dir = transform.position - enemy.transform.position;
		if (collision.gameObject.CompareTag("Enemy"))
		{
			rb.AddForce(dir.normalized * flyAmount, ForceMode.Impulse);
			StartCoroutine(StartSink());
		}
	}

	IEnumerator StartSink()
	{
		yield return new WaitForSeconds(2);
		sink = true;
		GetComponent<Rigidbody>().isKinematic = true;
		GetComponent<BoxCollider>().enabled = false;
	}

	private void Sinking()
	{
		float y;
		y = transform.position.y - sinkSpd * Time.deltaTime;
		transform.position = new Vector3(transform.position.x, y, transform.position.z);
	}
}
