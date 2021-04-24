using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 diff;
    private Vector3 oldPosition;

    // Start is called before the first frame update
    void Start()
    {
        oldPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        diff = oldPosition - transform.position;
        oldPosition = transform.position;
    }
}
