using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
	public static EffectManager me;
	public GameObject dropped_mat_prefab;

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
		if (effect.damageAmount > 0)
		{
			print("dealt " + effect.damageAmount + " damage to " + target.name);
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
		if (effect.matProduce.Count > 0)
		{
			GameObject matDropped = effect.matProduce[Random.Range(0, effect.matProduce.Count)];
			if (effect.dropMat)
			{
				print(target.name + " dropped " + matDropped.name);
				Instantiate(matDropped, target.transform.position, Quaternion.identity);
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
}
