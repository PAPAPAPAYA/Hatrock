using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCtrlScript : MonoBehaviour
{
    public Transform spellSpawnLoc;
    public GameObject spellPrefab;
	public float spellSpd;
	public GameObject aoeRangeIndicator;
	public float aoeDistance;

	private Color ariOgColor;

	public enum CastType
	{
		none,
		projectile,
		aoe,
		pie,
		target
	};
	public CastType currentCastType;

	private void Start()
	{
		ariOgColor = aoeRangeIndicator.GetComponent<SpriteRenderer>().color;
	}

	private void Update()
	{
		// choose cast type
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			currentCastType = CastType.projectile;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			currentCastType = CastType.aoe;
		}

		currentCastType = PlayerScript.me.currentMat.GetComponent<MatScript>().matCastType;

		// if cast type projectile
		if (currentCastType == CastType.projectile)
		{
			aoeRangeIndicator.SetActive(false);
			if (Input.GetMouseButtonDown(0))
			{
				SpawnSpell_proj();
			}
		}
		// if cast type aoe
		else if (currentCastType == CastType.aoe)
		{
			// show range
			aoeRangeIndicator.SetActive(true);

			// restrict distance
			float dist = Vector3.Distance(MouseManager.me.mousePos, transform.position);
			if (dist > aoeDistance)
			{
				Vector3 fromOriginToObject = MouseManager.me.mousePos - transform.position;
				fromOriginToObject *= aoeDistance;
				fromOriginToObject /= dist;
				aoeRangeIndicator.transform.position = transform.position + fromOriginToObject;
				aoeRangeIndicator.transform.position = new Vector3(aoeRangeIndicator.transform.position.x, 0.1f, aoeRangeIndicator.transform.position.z);
			}
			else
			{
				aoeRangeIndicator.transform.position = new Vector3(MouseManager.me.mousePos.x, 0.1f, MouseManager.me.mousePos.z);
			}
			
			// cast the spell
			if (Input.GetMouseButtonDown(0))
			{
				aoeRangeIndicator.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
				StartCoroutine("ChangeToDefaultColor");
			}
		}
	}

	private IEnumerator ChangeToDefaultColor()
	{
		yield return new WaitForSeconds(0.05f);
		aoeRangeIndicator.GetComponent<SpriteRenderer>().color = ariOgColor;
	}

	private void SpawnSpell_proj()
	{
		GameObject spell = Instantiate(spellPrefab, spellSpawnLoc.position, spellSpawnLoc.rotation);
		spell.GetComponent<Rigidbody>().AddForce(spellSpawnLoc.transform.forward * spellSpd, ForceMode.Impulse);
		spell.GetComponent<MeshRenderer>().material = PlayerScript.me.currentMat.GetComponent<MatScript>().myMaterial;
	}
}
