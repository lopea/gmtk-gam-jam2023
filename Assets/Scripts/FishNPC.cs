using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        _spawnX = (_pond.transform.localScale.x / 2) - 0.5f;
        _spawnZ = (_pond.transform.localScale.z / 2) - 0.5f;
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
            pos = new Vector3(_pond.transform.position.x + Random.Range(-_spawnX, _spawnX),
                _pond.transform.position.y + 0.6f, _pond.transform.position.z + Random.Range(-_spawnZ, _spawnZ));

            idleTime = Random.Range(1, 7);
        }
    }

    void Wander()
    {
        if (Vector3.Distance(transform.position, pos) > 1.0f)
        {
            _rb.AddForce(Vector3.Normalize(pos - transform.position) * 0.1f, ForceMode.Acceleration);
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
}
