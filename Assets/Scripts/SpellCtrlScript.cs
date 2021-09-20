using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCtrlScript : MonoBehaviour
{
    public Transform spellSpawnLoc;
    public GameObject spellPrefab;
	public float spellSpd;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject spell = Instantiate(spellPrefab, spellSpawnLoc.position, spellSpawnLoc.rotation);
			spell.GetComponent<Rigidbody>().AddForce(spellSpawnLoc.transform.forward * spellSpd, ForceMode.Impulse);
			print(spellSpawnLoc.transform.forward);
		}
	}
}
