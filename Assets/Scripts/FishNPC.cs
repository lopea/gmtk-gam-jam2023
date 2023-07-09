using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishNPC : MonoBehaviour
{
    [SerializeField] private GameObject _pond;

    private GameObject _player;

    // Bait will not spawn within this radius local to the player
    [SerializeField] private float _playerRadius = 3f;

    [SerializeField] private int maxBait = 5;

    private float _spawnX;
    private float _spawnZ;
    private int currBait;
    [SerializeField] public List<GameObject> _bait;

    private float idleTime = 1.0f;
    private bool busy = false;
    private Vector3 pos;

    private Rigidbody _rb;

    private bool dontChange;

    // Awake is called before the first frame update
    void Awake()
    {
        _player = GameObject.Find("Player");
        _spawnX = 30;
        _spawnZ = 17;
        currBait = 0;
        _bait.Capacity = 10;
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(idleTime > 0)
        {
            idleTime -= Time.deltaTime;
            Wander();
        }
        else
        {
            var position = _pond.transform.position;
            pos = new Vector3(position.x + Random.Range(-_spawnX, _spawnX),
                position.y + 0.6f, position.z + Random.Range(-_spawnZ, _spawnZ));
            
            idleTime = Random.Range(1, 7);
        }
    }

    void Wander()
    {
        if (Vector3.Distance(transform.position, pos) > 1.0f)
        {
            var dir = (dontChange ? _rb.velocity.normalized : Vector3.Normalize(pos - transform.position)) * Random.Range(2.0f, 5.0f);
            _rb.AddForce(dir, ForceMode.Acceleration);
            transform.rotation = Quaternion.LookRotation(_rb.velocity) * Quaternion.Euler(0, 90, 0);
        }
        else
        {
            currBait = 0;
        }
    }

    void Eat()
    {

    }


    IEnumerator UpdateDontChange()
    {
        dontChange = true;
        yield return new WaitForSeconds(1.5f);
        dontChange = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Barrier") && collision.contacts.Length >= 1)
        {
            _rb.velocity = Vector3.Reflect(_rb.velocity, collision.contacts[0].normal); 
            StartCoroutine(UpdateDontChange());
        }
    }
}
