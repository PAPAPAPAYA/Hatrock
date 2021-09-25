using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    static public MaterialManager me;

	public List<GameObject> matList;


    public enum CtrlTypes
    {
        none,
        stun,
        slow,
        fear,
        move
    };

	private void Awake()
	{
        me = this;
	}
}
