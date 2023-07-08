using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class FoodSystem : MonoBehaviour
{
    public float percentChanceBait = 20f;

    public float baitDelay = 2f;

    [SerializeField] private float _drain = 5f;

    // please keep this 100 lol
    [SerializeField] private float _maxFood = 100f;

    [SerializeField] private float _eatAmount = 15;

    private RectTransform _foodBar;

    private float _currFood;

    private float _timer;
    
    private float _destination;
    private BaitSpawner _spawner;
    void Start()
    {
        _foodBar = GameObject.Find("FoodMeter").GetComponent<RectTransform>();
        _spawner = GameObject.Find("BaitSpawner").GetComponent<BaitSpawner>();
        _currFood = _maxFood;
        _timer = 0f;
        _drain = 5f;
    }
    
    void Update()
    {
        
        _timer += Time.deltaTime;
        if (_timer >= 1f)
        {
            _timer = 0f;
            _destination = _foodBar.localPosition.x - _drain;
            // _foodBar.localPosition -= offset;
        }
        // float destination = _foodBar.localPosition.
        // float lerpedValue = Mathf.Lerp(_foodBar.localPosition.x,)
        _foodBar.localPosition = new Vector3(Mathf.Lerp(_foodBar.localPosition.x, _destination, _timer),
            _foodBar.localPosition.y, _foodBar.localPosition.z);
        // Vector3 offset = new Vector3(_drain, 0, 0);
    }

    void EatFood(GameObject food)
    {
        int baitRoll = Random.Range(0, 100);
        if (baitRoll <= percentChanceBait)
        {
            // event for bait here
        }
        else
        {
            Destroy(food);
            _currFood += _eatAmount;
            Vector3 offset = new Vector3(_eatAmount, 0, 0);
            _foodBar.localPosition += offset;
            _timer = 0f;
            _destination = _foodBar.localPosition.x - _drain;
            _spawner.HandleEat(food);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            EatFood(collision.gameObject);
        }
    }
}
