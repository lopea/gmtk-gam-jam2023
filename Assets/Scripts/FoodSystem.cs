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

    public GameObject baitTrap;

    [SerializeField] private float _drain = 5f;

    // please keep this 150 lol
    [SerializeField] private float _maxFood = 150f;

    [SerializeField] private float _eatAmount = 15;

    [SerializeField] private RodEvent _rod;

    private RectTransform _foodBar;

    public float _currFood;

    private float _timer;
    
    private float _destination;
    private BaitSpawner _spawner;
    void Start()
    {
        _foodBar = GameObject.Find("FoodMeter").GetComponent<RectTransform>();
        _spawner = GameObject.Find("BaitSpawner").GetComponent<BaitSpawner>();
        _rod = GameObject.Find("Rod").GetComponent<RodEvent>();
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
            _currFood -= _drain;
            // _foodBar.localPosition -= offset;
        }
        // float destination = _foodBar.localPosition.
        // float lerpedValue = Mathf.Lerp(_foodBar.localPosition.x,)
        _foodBar.localPosition = new Vector3(Mathf.Lerp(_foodBar.localPosition.x, _destination, _timer),
            _foodBar.localPosition.y, _foodBar.localPosition.z);
        // Vector3 offset = new Vector3(_drain, 0, 0);
        if (_currFood <= 0)
        {
            GameObject.Find("StateManager").GetComponent<GameStateManager>().HandleLose();
        }
    }

    void EatFood(GameObject food)
    {
        if (!BaitCheck(food))
        {
            Destroy(food);
            _currFood += _eatAmount;
            if (_currFood >= _maxFood)
            {
                _currFood = _maxFood;
                _foodBar.position = new Vector3(75, _foodBar.position.y, _foodBar.position.z);
            }
            else
            {
                Vector3 offset = new Vector3(_eatAmount, 0, 0);
                _foodBar.localPosition += offset;
            }
        }
        _spawner.HandleEat(food);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            EatFood(other.gameObject);
        }
    }

    public bool BaitCheck(GameObject food)
    {
        int check = Random.Range(0, 100);
        if (check <= percentChanceBait)
        {
            Destroy(food);
            GameObject trap = Instantiate(baitTrap, food.transform.position, Quaternion.Euler(0,0, 0), null);
            //_rod.StartRodEvent(trap);
            return true;
        }

        return false;
    }
}
