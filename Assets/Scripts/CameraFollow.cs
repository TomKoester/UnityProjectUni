using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float hardness = 10f;
    public float rotationalSpeed = 50f;
    public float height = 10;
    public float radius = 10;
    public float angle = 0;

    // Update is called once per frame
    private void FixedUpdate()
    {
        float cameraX = target.position.x + (radius * Mathf.Cos(Mathf.Deg2Rad * angle));
        float cameraY = target.position.y + height;
        float cameraZ = target.position.z + (radius * Mathf.Sin(Mathf.Deg2Rad * angle));

        Vector3 wantPosition = new Vector3(cameraX, cameraY, cameraZ);

        if (Input.GetKey(KeyCode.E))
        {
            angle = angle - rotationalSpeed * Time.deltaTime;
        }//end of if
        else if (Input.GetKey(KeyCode.Q))
        {
            angle = angle + rotationalSpeed * Time.deltaTime;
        }//end of else if
        transform.position = Vector3.Lerp(transform.position, wantPosition, Mathf.Clamp01(Time.deltaTime * hardness));
        transform.LookAt(target.position);
    }
}
