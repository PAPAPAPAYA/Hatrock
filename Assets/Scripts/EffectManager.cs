using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
	public static EffectManager me;
	public float droppedMat_flyAmount;

	public enum CtrlTypes
	{
		none,
		cantWalk,
		cantAttack,
		forceMove
	};

	private void Awake()
	{
		me = this;
	}

	public void ProcessEffects(GameObject mat, GameObject target)
	{
		foreach (var effect in mat.GetComponent<MatScript>().myEffects)
		{
			DoDamage(target, effect);
			DoCtrl(target, effect);
			Heal(target, effect);
			DropMat(target, effect);
		}
	}

	public void DoDamage(GameObject target, EffectStruct effect)
	{
		if (target.tag == "Enemy" && effect.damageAmount > 0)
		{
			print("dealt " + effect.damageAmount + " damage to " + target.name);
		}

        if (effect.DOT)
        {
			StartCoroutine(DoDOT(effect, target));
		}
	}

	public void DoCtrl(GameObject target, EffectStruct effect)
	{
		if (effect.myCtrlType != CtrlTypes.none)
		{
			print(target.name + " will be in " + effect.myCtrlType.ToString() + " state for " + effect.ctrl_duration + "s");
			if (effect.myCtrlType == CtrlTypes.forceMove)
			{
				// knock back based on amount
				KnockBack(effect.knockback_amount, PlayerScript.me.gameObject, target);
			}
			if (effect.myCtrlType == CtrlTypes.cantAttack)
			{
				if (target.GetComponent<Enemy>() != null)
				{
					target.GetComponent<Enemy>().attackable = false;
					StartCoroutine(ResetAttackability(effect.ctrl_duration, target));
				}
			}
			if (effect.myCtrlType == CtrlTypes.cantWalk)
			{
				if (target.GetComponent<Enemy>() != null)
				{
					target.GetComponent<Enemy>().walkable = false;
					StartCoroutine(ResetMoveability(effect.ctrl_duration, target));
				}
			}
		}
	}

	public void DropMat(GameObject target, EffectStruct effect)
	{
		if (target.GetComponent<Enemy>() != null)
		{
			Enemy eS = target.GetComponent<Enemy>();
			if (effect.matProduce.Count > 0)
			{
				if (effect.dropMat)
				{
					eS.dropMeter += effect.dropMatAmount;
					if (eS.dropMeter >= eS.dropMeterMax)
					{
						eS.dropMeter = 0;
						SpawnMat(target, effect);
					}
				}
			}
		}
	}

	public void Heal(GameObject target, EffectStruct effect)
	{
		if (target.GetComponent<PlayerScript>() != null && effect.healAmount > 0)
		{
			print("healed " + target.name + " " + effect.healAmount);
			target.GetComponent<PlayerScript>().hp += effect.healAmount;
		}

        if (effect.HOT)
        {
			StartCoroutine(DoHOT(effect, target));
        }
	}

	public void KnockBack(float amount, GameObject er, GameObject ee)
	{
		Vector3 dir = ee.transform.position - er.transform.position;
		ee.GetComponent<Rigidbody>().AddForce(dir.normalized * amount, ForceMode.Impulse);
	}

	IEnumerator ResetAttackability(float duration, GameObject target)
	{
		float timer = 0f;
		while (timer < duration)
		{
			print(timer);
			timer += Time.deltaTime;
			yield return null;
		}
		if (target.GetComponent<Enemy>() != null &&
			timer >= duration)
		{
			target.GetComponent<Enemy>().attackable = true;
		}
	}

	IEnumerator ResetMoveability(float duration, GameObject target)
	{
		float timer = 0f;
		while (timer < duration)
		{
			print(timer);
			timer += Time.deltaTime;
			yield return null;
		}
		if (target.GetComponent<Enemy>() != null &&
			timer >= duration)
		{
			target.GetComponent<Enemy>().walkable = true;
		}
	}

	IEnumerator DoDOT(EffectStruct effect, GameObject target)
    {
		int timer = 1;
		yield return new WaitForSeconds(1f);
		while(timer <= effect.DOT_duration)
        {
			timer += 1;
			print("dealt " + effect.DOT_interval + " DOT damage to " + target.name);
			yield return new WaitForSeconds(1f);
		}
		if(timer > effect.DOT_duration)
        {
			StopCoroutine(DoDOT(effect, target));
        }
    }

	IEnumerator DoHOT(EffectStruct effect, GameObject target)
	{
		int timer = 1;
		yield return new WaitForSeconds(1f);
		print(target.name);
		//if (target.GetComponent<PlayerScript>() != null)
		{
			
			while (timer <= effect.HOT_duration)
			{
				timer += 1;

				print("healed " + effect.HOT_interval + " HOT HP to " + target.name);
				target.GetComponent<PlayerScript>().hp += effect.HOT_interval;
				yield return new WaitForSeconds(1f);
			}
			if (timer > effect.HOT_duration)
			{
				StopCoroutine(DoHOT(effect, target));
			}
		}
	}

	public void SpawnMat(GameObject target, EffectStruct effect)
	{
		GameObject matDropped = effect.matProduce[Random.Range(0, effect.matProduce.Count)];
		print(target.name + " dropped " + matDropped.name);
		Vector3 spawnPos = new Vector3(target.transform.position.x, target.transform.position.y + 0.7f, target.transform.position.z);
		GameObject droppedMat = Instantiate(matDropped, spawnPos, Quaternion.identity);
		droppedMat.GetComponent<Rigidbody>().AddForce(
			new Vector3(Random.Range(-droppedMat_flyAmount, droppedMat_flyAmount),
			3,
			Random.Range(-droppedMat_flyAmount, droppedMat_flyAmount)),
			ForceMode.Impulse);
	}
}