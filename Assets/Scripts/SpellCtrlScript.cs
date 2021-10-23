using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCtrlScript : MonoBehaviour
{
	public PlayerScript ps;
	private Animator anim;

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

	[Header("Self")]
	public GameObject selfIndicator;
	
	public enum CastType
	{
		none,
		projectile,
		aoe,
		pie,
		target,
		self
	};
	public CastType currentCastType;

	private void Start()
	{
		aoeOgColor = aoeRangeIndicator.GetComponent<SpriteRenderer>().color;
		ps = gameObject.GetComponent<PlayerScript>();
		//pieRangeIndicator.GetComponent<SpriteRenderer>().color = aoeOgColor;
		anim = GetComponent<Animator>();
	}
	
	private void Update()
	{
		if (PlayerScript.me.currentMat != null && anim.GetCurrentAnimatorStateInfo(0).IsName("testIdle"))
		{
			currentCastType = PlayerScript.me.currentMat.GetComponent<MatScript>().matCastType;
			// if cast type projectile
			if (currentCastType == CastType.projectile)
			{
				aoeRangeIndicator.SetActive(false);
				pieRangeIndicator.SetActive(false);
				targetIndicator.SetActive(false);
				selfIndicator.SetActive(false);

				if (Input.GetMouseButtonDown(0))
				{
					anim.Play("testWindup");
				}
			}
			// if cast type aoe
			else if (currentCastType == CastType.aoe)
			{
				// show range
				aoeRangeIndicator.SetActive(true);
				pieRangeIndicator.SetActive(false);
				targetIndicator.SetActive(false);
				selfIndicator.SetActive(false);

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
			else if (currentCastType == CastType.pie)
			{
				aoeRangeIndicator.SetActive(false);
				pieRangeIndicator.SetActive(true);
				targetIndicator.SetActive(false);
				selfIndicator.SetActive(false);

				pieRangeIndicator.GetComponent<Image>().fillAmount = 1f / 360f * pieAngle;
				Quaternion targetAngle = Quaternion.Euler(0, 0, pieAngle / 2 - 180f);
				pieRangeIndicator.GetComponent<RectTransform>().localRotation = targetAngle;

				if (Input.GetMouseButtonDown(0))
				{
					SpawnSpell_pie();
				}
			}
			else if (currentCastType == CastType.target)
			{
				aoeRangeIndicator.SetActive(false);
				pieRangeIndicator.SetActive(false);
				selfIndicator.SetActive(false);

				//if selected enemy, then show the indicator
				if (MouseManager.me.enemySelected != null)
				{
					targetIndicator.SetActive(true);
					if (Input.GetMouseButtonDown(0))
					{
						targetIndicator.GetComponent<Light>().color = new Color(0, 159, 179, 1);
						//! insert effect codes here
						print("hit enemy with target");
						EffectManager.me.ProcessEffects(gameObject.GetComponent<PlayerScript>().currentMat, MouseManager.me.enemySelected);
					}
					if (Input.GetMouseButtonUp(0))
						targetIndicator.GetComponent<Light>().color = new Color(255, 255, 255, 1);
				}
				else
				{
					targetIndicator.SetActive(false);
				}
			}
			else if (currentCastType == CastType.self)
			{
				aoeRangeIndicator.SetActive(false);
				pieRangeIndicator.SetActive(false);
				targetIndicator.SetActive(false);
				selfIndicator.SetActive(true);

				if (Input.GetMouseButtonDown(0))
				{
					selfIndicator.GetComponent<Light>().color = new Color(0, 159, 179, 1);
				}
				//! effect goes here
				if (Input.GetMouseButtonUp(0))
				{
					EffectManager.me.ProcessEffects(ps.currentMat, ps.gameObject);
					selfIndicator.GetComponent<Light>().color = new Color(59, 190, 55, 1);
				}
			}
			else
			{
				Debug.Log("I am none");
				aoeRangeIndicator.SetActive(false);
				pieRangeIndicator.SetActive(false);
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
		aoeSpell.GetComponent<SpellAOEScript>().mat = gameObject.GetComponent<PlayerScript>().currentMat;
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
					EffectManager.me.ProcessEffects(gameObject.GetComponent<PlayerScript>().currentMat, collider.gameObject);
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
