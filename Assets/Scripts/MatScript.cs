using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatScript : MonoBehaviour
{
    public string namae;
    public SpellCtrlScript.CastType matCastType;
    public bool compound; // true - product of combination
    public Material myMaterial;
    public List<EffectStruct> myEffects;

    [Header("Projectile")]
    public float mass;
    public float spd;

    [Header("AOE")]
    public float aoe_distance;
    public float aoe_range;

}
