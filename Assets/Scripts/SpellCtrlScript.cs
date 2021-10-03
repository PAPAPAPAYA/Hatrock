using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCtrlScript : MonoBehaviour
{
	[Header("PROJECTILE")]
    public Transform spellSpawnLoc;
    public GameObject spell_proj_prefab;
	public float spellSpd;

	[Header("AOE")]
	public GameObject spell_AOE_prefab;
	public GameObject aoeRangeIndicator;
	public float aoeDistance;
	public float aoeRadius;
	private Color aoeOgColor;

	[Header("PIE")]
	public GameObject pieRangeIndicator;
	public float pieRadius;
	public float pieAngle;

	[Header("TARGET")]
	public GameObject targetIndicator;
	
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
		aoeOgColor = aoeRangeIndicator.GetComponent<SpriteRenderer>().color;
		//pieRangeIndicator.GetComponent<SpriteRenderer>().color = aoeOgColor;
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

			// get aoe params from current material
			aoeRadius = PlayerScript.me.currentMat.GetComponent<MatScript>().aoe_range;
			aoeDistance = PlayerScript.me.currentMat.GetComponent<MatScript>().aoe_distance;

			// change indicator range according to spell range
			aoeRangeIndicator.transform.localScale = new Vector3(aoeRadius / 15, aoeRadius / 15, 1);
			//aoeRangeIndicator.GetComponent<Light>().spotAngle = aoeRadius;

			// restrict distance
			float dist = Vector3.Distance(MouseManager.me.mousePos, transform.position);
			if (dist > aoeDistance)
			{
				Vector3 fromOriginToObject = MouseManager.me.mousePos - transform.position;
				fromOriginToObject *= aoeDistance;
				fromOriginToObject /= dist;
				aoeRangeIndicator.transform.position = transform.position + fromOriginToObject;
				aoeRangeIndicator.transform.position = new Vector3(aoeRangeIndicator.transform.position.x, .1f, aoeRangeIndicator.transform.position.z);
			}
			else
			{
				aoeRangeIndicator.transform.position = new Vector3(MouseManager.me.mousePos.x, .1f, MouseManager.me.mousePos.z);
			}
			
			// cast the spell
			if (Input.GetMouseButtonDown(0))
			{
				//aoeRangeIndicator.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
				//StartCoroutine(ChangeToDefaultColor(aoeRangeIndicator));
				SpawnSpell_aoe();
			}
		}
		else if(currentCastType == CastType.pie)
        {
			aoeRangeIndicator.SetActive(false);
			pieRangeIndicator.SetActive(true);
			targetIndicator.SetActive(false);

			pieRangeIndicator.GetComponent<Image>().fillAmount = 1f / 360f * pieAngle;
			Quaternion targetAngle = Quaternion.Euler(0, 0, pieAngle / 2 - 180f);
			pieRangeIndicator.GetComponent<RectTransform>().localRotation = targetAngle;

            if (Input.GetMouseButtonDown(0))
            {
				SpawnSpell_pie();
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
					//! insert effect codes here
					print("hit enemy with target");
                }
				if(Input.GetMouseButtonUp(0))
					targetIndicator.GetComponent<Light>().color = new Color(255, 255, 255, 1);
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
		indicator.GetComponent<SpriteRenderer>().color = aoeOgColor;
	}

	private void SpawnSpell_proj() // send out the spell
	{
		GameObject spell = Instantiate(spell_proj_prefab, spellSpawnLoc.position, spellSpawnLoc.rotation);
		spell.GetComponent<MeshRenderer>().material = PlayerScript.me.currentMat.GetComponent<MatScript>().myMaterial;
		//spell.GetComponent<Rigidbody>().mass = PlayerScript.me.currentMat.GetComponent<MatScript>().mass;
		spell.GetComponent<Rigidbody>().mass = PlayerScript.me.currentMat.GetComponent<MatScript>().mass;
		spell.GetComponent<Rigidbody>().AddForce(spellSpawnLoc.transform.forward * spellSpd, ForceMode.Impulse);
		spell.GetComponent<SpellScript>().mat = gameObject.GetComponent<PlayerScript>().currentMat;
	}

	private void SpawnSpell_aoe() // spawn a collider
	{
		GameObject aoeSpell = Instantiate(spell_AOE_prefab);
		aoeSpell.transform.position = aoeRangeIndicator.transform.position;
		aoeSpell.transform.localScale = new Vector3(aoeRadius, 2f, aoeRadius);
	}

	private void SpawnSpell_pie() // check if enemy in pie
	{
		Collider[] targetsAround = Physics.OverlapSphere(transform.position, pieRadius);
		foreach (var collider in targetsAround)
		{
			if (collider.gameObject.CompareTag("Enemy"))
			{
				Vector3 tempV3 = new Vector3(collider.transform.position.x, 0, collider.transform.position.z);
				if (Vector3.Angle(transform.forward, tempV3 - transform.position) < pieAngle / 2)
				{
					//! insert effect here
					print("hit enemy with pie");
				}
				else
				{
					print(collider.gameObject.name + " not in pie, its angle: " + Vector3.Angle(transform.forward, tempV3 - transform.position));
				}
			}
		}
	}
}
