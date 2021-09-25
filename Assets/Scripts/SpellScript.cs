using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{
    void Update()
    {
        // destroy it after 3 seconds
        Destroy(gameObject, 3);
    }
}
