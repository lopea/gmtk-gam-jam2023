using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class Bob : MonoBehaviour
{
    public float freq = 10, amplitude = 1;
    public Vector3 direction = Vector3.up;
    Vector3 pos;
    private float RandomOffset;
    private void Awake()
    {
        RandomOffset = Random.Range(0.0f, 2.0f* Mathf.PI);
        pos = transform.position;
    }

    private void Update()
    {
        transform.position = pos + direction * (Mathf.Sin(2.0f * Mathf.PI * freq  * Time.time) * amplitude + RandomOffset);
    }
}