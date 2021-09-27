using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCtrlScript : MonoBehaviour
{
    public Transform spellSpawnLoc;
    public GameObject spellPrefab;
	public float spellSpd;
	public GameObject aoeRangeIndicator;
	public GameObject pieRangeIndicator;
	public GameObject targetIndicator;
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
		pieRangeIndicator.GetComponent<SpriteRenderer>().color = ariOgColor;
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
			pieRangeIndicator.SetActive(false);
			targetIndicator.SetActive(false);

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
			pieRangeIndicator.SetActive(false);
			targetIndicator.SetActive(false);

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
				StartCoroutine(ChangeToDefaultColor(aoeRangeIndicator));
			}
		}
		else if(currentCastType == CastType.pie)
        {
			aoeRangeIndicator.SetActive(false);
			pieRangeIndicator.SetActive(true);
			targetIndicator.SetActive(false);

            if (Input.GetMouseButtonDown(0))
            {
				pieRangeIndicator.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
				StartCoroutine(ChangeToDefaultColor(pieRangeIndicator));
            }
        }
		else if(currentCastType == CastType.target)
        {
			aoeRangeIndicator.SetActive(false);
			pieRangeIndicator.SetActive(false);

			//if selected enemy, then show the indicator
            if (MouseManager.me.slctedEnemy)
            {
				targetIndicator.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
					targetIndicator.GetComponent<Light>().color = new Color(0, 159, 179, 1);
                }
				if(Input.GetMouseButtonUp(0))
					targetIndicator.GetComponent<Light>().color = new Color(176, 0, 0, 1);
			}
            else
            {
				targetIndicator.SetActive(false);
            }
        }
	}

	private IEnumerator ChangeToDefaultColor(GameObject indicator)
	{
		yield return new WaitForSeconds(0.05f);
		indicator.GetComponent<SpriteRenderer>().color = ariOgColor;
	}

	private void SpawnSpell_proj()
	{
		GameObject spell = Instantiate(spellPrefab, spellSpawnLoc.position, spellSpawnLoc.rotation);
		spell.GetComponent<Rigidbody>().AddForce(spellSpawnLoc.transform.forward * spellSpd, ForceMode.Impulse);
		spell.GetComponent<MeshRenderer>().material = PlayerScript.me.currentMat.GetComponent<MatScript>().myMaterial;
	}
}
