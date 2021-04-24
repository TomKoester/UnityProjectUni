using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Lightning : MonoBehaviour
{
    public int numberOfPoints = 32;
    public Transform startPoint;
    public Transform endPoint;

    LineRenderer lineRenderer;
    Vector3[] positions;
    Vector3 arcDirection;

    float timer = 100;

    public float resetTimeMin = 0.5f;
    public float resetTimeMax = 1.0f;
    float resetTime = 0.5f;

    public float arcingIntensity = 1.0f;
    public float sinFrequency = 2.0f;
    public float sinIntensity = 1.0f;
    public float randomness;
    float offset;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        resetTime = Random.Range(resetTimeMin, resetTimeMax);
    }


    // Update is called once per frame
    void Update()
    {
        if(positions == null || positions.Length != numberOfPoints)
        {
            positions = new Vector3[numberOfPoints];
        }


        for(int i = 0; i < numberOfPoints; ++i)
        {
            //calculate line points
            float t = 1.0f* i / (numberOfPoints-1);
            Vector3 p = Vector3.Lerp(startPoint.position + Vector3.up * 3.5f, endPoint.position, t);

            //arcing
            float arc = Mathf.Sin(t * Mathf.PI) * timer;
            p += arcDirection * arc * arcingIntensity;

            //more interesting arc
            p += arcDirection * Mathf.Sin(t * sinFrequency+ offset) * timer * sinIntensity * arc;

            //randomness
            p += new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * randomness;

            positions[i] = p;
            
        }

        lineRenderer.positionCount = numberOfPoints;
        lineRenderer.SetPositions(positions);

        timer += Time.deltaTime;
        if (timer > resetTime)
        {
            timer = 0;
            resetTime = Random.Range(resetTimeMin, resetTimeMax);
            offset = Random.Range(0, Mathf.PI * 2);
            Vector3 diff = startPoint.position - endPoint.position;
            arcDirection = Vector3.Cross(diff, new Vector3(Random.Range(-1f, 1f), Random.Range(-1f,1f), Random.Range(-1f, 1f)));
            arcDirection.Normalize();
            
      
            /*
            for (int i = 0; i < numberOfPoints; ++i)
            {
                //calculate line points
                float t = 1.0f * i / (numberOfPoints - 1);
                positions[i] = Vector3.Lerp(startPoint.position, endPoint.position, t);
            }
            */
        }
    }
}
