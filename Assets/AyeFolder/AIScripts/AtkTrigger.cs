using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkTrigger : MonoBehaviour
{
    public bool onAtkTrigger = false;
    public MeshRenderer myMR;

    private void Awake()
    {
        myMR = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log(other.gameObject.name);
            onAtkTrigger = true;
            myMR.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            onAtkTrigger = false;
            myMR.enabled = false;
        }
    }

}
