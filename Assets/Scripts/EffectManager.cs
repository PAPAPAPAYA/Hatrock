using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
	public static EffectManager me;
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
}
