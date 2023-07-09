using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BaitSpawner : MonoBehaviour
{

    [SerializeField] private GameObject _pond;

    [SerializeField] private GameObject _player;

    [SerializeField] private GameObject _baitPrefab;

    // Bait will not spawn within this radius local to the player
    [SerializeField] private float _playerRadius = 3f;

    [SerializeField] private int maxBait = 5;

    private float _spawnX;
    private float _spawnZ;
    private int currBait;
    [SerializeField] public List<GameObject> _bait;
    
    
    // Awake is called before the first frame update
    void Awake()
    {
        _player = GameObject.Find("Player");
        _spawnX = 25;
        _spawnZ = 16;
        currBait = 0;
        _bait.Capacity = 10;
        // Just serialize pond in editor for now, idk what the name will be
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = currBait; i < maxBait; i++)
        {
            // considering using random.insideunitsphere but pond is likely oval instead of circle
            Vector3 pos = new Vector3(_pond.transform.position.x + Random.Range(-_spawnX, _spawnX),
                _pond.transform.position.y + 0.6f, _pond.transform.position.z + Random.Range(-_spawnZ, _spawnZ));
            // fuck being efficient lol
            if (Vector3.Magnitude(_player.transform.position - pos) <= _playerRadius)
            {
                if (i > 0)
                {
                    --i;
                }
                continue;
            }
            ++currBait;
            GameObject bait = Instantiate(_baitPrefab, pos, Quaternion.identity);
            _bait.Add(bait);
        }
        
        //TESTING PURPOSES
        if (Input.GetKeyDown(KeyCode.X))
        {
            int rand = Random.Range(0, _bait.Count - 1);
            Destroy(_bait[rand]);
            _bait.RemoveAt(rand);
            --currBait;
        }
    }

    public void HandleEat(GameObject food)
    {
        _bait.Remove(food);
        Destroy(food);
        --currBait;
    }
}
