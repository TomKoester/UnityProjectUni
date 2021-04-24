using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float shake;
    public float decreaseFactor = 1f;
    public float shakeAmount = 30f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shake > 0)
        {
            Camera.main.transform.localPosition = Random.insideUnitSphere * shakeAmount;
            shake -= Time.deltaTime * decreaseFactor;

        }
        else
        {
            Camera.main.transform.localPosition = Vector3.zero;
            shake = 0.0f;
        }
    }
}
