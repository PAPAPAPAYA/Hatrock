using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAOEScript : MonoBehaviour
{
	public List<GameObject> targetsInAoe;

	void Update()
	{
		// destroy it after 3 seconds
		Destroy(gameObject, 3);
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			// !effect here
			print("enemy in aoe");
		}
	}
}
