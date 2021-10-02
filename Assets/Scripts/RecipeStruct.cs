using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Recipe
{
    public string namae;
    public List<GameObject> Materials;
    public List<int> RequiredAmout;
    public GameObject Outcome;
}
