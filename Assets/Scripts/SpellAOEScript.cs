using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAOEScript : MonoBehaviour
{
	void Update()
	{
		// destroy it after 3 seconds
		Destroy(gameObject, 3);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			// effect here
			print("hit enemy with an aoe");
		}
	}
}
