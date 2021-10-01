using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{

	private void Update()
	{
		
	}
	private void OnCollisionEnter(Collision collision)
	{
        if (collision.gameObject.CompareTag("Enemy"))
		{
			// effect here
			print("hit enemy with a projectile");
		}
		if (!collision.gameObject.CompareTag("Player"))
		{
			Destroy(gameObject);
		}
	}
}
