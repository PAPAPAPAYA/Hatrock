using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    public Transform targetObject;
    private Vector3 initialOffset;
    private Vector3 cameraPosition;
    public float smoothness;
    public CamConstrainStruct livingRoom;

    void Start()
    {
        initialOffset = transform.position - targetObject.position;
    }

    void Update()
    {
        cameraPosition = targetObject.position + initialOffset;
        // when in living room
        cameraPosition = new Vector3(Mathf.Clamp(cameraPosition.x, livingRoom.xMin, livingRoom.xMax),
            cameraPosition.y,
			Mathf.Clamp(cameraPosition.z, Mathf.Max(transform.position.x + livingRoom.z_accordingToX_min, livingRoom.zMin), Mathf.Min(transform.position.x + livingRoom.z_accordingToX_max, livingRoom.zMax)));
		transform.position = Vector3.Lerp(transform.position, cameraPosition, smoothness * Time.fixedDeltaTime);
    }
}
