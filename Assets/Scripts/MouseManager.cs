using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    static public MouseManager me;
    public Vector3 mousePos;
    public LayerMask ignoreMe;

	private void Awake()
	{
        me = this;
	}

	private void Update()
	{
        // STEP 1: declare a ray, use mouse's screenspace pixel coordinate
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // STEP 2: declare mouse ray distance
        float mouseRayDist = 10000f;

        // STEP 2B: declare a blank RaycastHit variable
        RaycastHit rayHit = new RaycastHit();

        // STEP 3: debug draw the raycast
        Debug.DrawRay(mouseRay.origin, mouseRay.direction * mouseRayDist, Color.magenta);

        // STEP 4: shoot the raycast
        if (Physics.Raycast(mouseRay, out rayHit, mouseRayDist, ~ignoreMe))
        {
            mousePos = rayHit.point;
        }
    }
}
