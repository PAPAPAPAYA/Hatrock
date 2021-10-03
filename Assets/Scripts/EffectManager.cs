using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
	public static EffectManager me;

	private void Awake()
	{
		me = this;
	}

	public void ProcessEffects(GameObject mat, GameObject target)
	{
		foreach (var effect in mat.GetComponent<MatScript>().myEffects)
		{
			DoDamage(target, effect);
		}
	}

	public void DoDamage(GameObject target, EffectStruct effect)
	{
		print("dealt " + effect.damageAmount + " to " + target.name);
	}
}
