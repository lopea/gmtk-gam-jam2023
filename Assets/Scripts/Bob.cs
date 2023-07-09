using System;
using UnityEngine;


public class Bob : MonoBehaviour
{
    public float freq = 10, amplitude = 1;
    public Vector3 direction = Vector3.up;
    public Vector3 pos;

    private void Awake()
    {
        pos = transform.position;
    }

    private void Update()
    {
        transform.position = pos + direction * (Mathf.Sin(2.0f * Mathf.PI * freq) * amplitude);
    }
}