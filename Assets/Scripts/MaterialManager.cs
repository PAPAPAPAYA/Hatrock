using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    static public MaterialManager me;

	public List<GameObject> matList;


    

	private void Awake()
	{
        me = this;
	}
}
