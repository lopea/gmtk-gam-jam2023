using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMaker : MonoBehaviour
{
    [SerializeField] private ParticleSystem _prefab;

    private ParticleSystem _ps;
    private Rigidbody _rb;


    public float MaxSize = 1;

    public float MaxSpeed = 10;

    public float MaxLifetime = 10;

    public float MaxParticles = 100;
    // Start is called before the first frame update
    void Start()
    {
        _ps = Instantiate(_prefab, transform);
        _ps.transform.localPosition = Vector3.right * 2f;
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var speed = (_rb.velocity.magnitude/MaxLifetime);
        var main = _ps.main;
        main.maxParticles = (int)(speed * MaxParticles * 1000.0f);
        main.startLifetime = speed * MaxLifetime;
        main.startSize = speed * MaxSize;
        var emission = _ps.emission;
        emission.burstCount = (int)(speed * MaxParticles/10.0f);
        
    }
}
