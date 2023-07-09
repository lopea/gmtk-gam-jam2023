using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FoodSystem : MonoBehaviour
{
    public float percentChanceBait = 20f;

    public float baitDelay = 4f;

    public GameObject baitTrap;

    [SerializeField] private float _drain = 15f;

    // please keep this 150 lol
    [SerializeField] private float _maxFood = 150f;

    private float _eatAmount = 30;

    [SerializeField] private RodEvent _rod;

    private Image _foodBar;

    public float _currFood;

    private float _timer;
    
    private float _destination;
    private BaitSpawner _spawner;
    public bool gracePeriod;
    private float _graceTimer;
    void Awake()
    {
        _foodBar = GameObject.Find("FoodMeter").GetComponent<Image>();
        _spawner = GameObject.Find("BaitSpawner").GetComponent<BaitSpawner>();
        _rod = GameObject.Find("Rod").GetComponent<RodEvent>();
        _currFood = _maxFood;
        _timer = 0f;
        _drain = 2.5f;
        _graceTimer = baitDelay;
    }
    
    void Update()
    {
        if (gracePeriod)
        {
            _graceTimer -= Time.deltaTime;
        }

        if (_graceTimer <= 0f)
        {
            gracePeriod = false;
            _graceTimer = baitDelay;
        }
        _timer += Time.deltaTime;
        if (_timer >= 1f)
        {
            _timer = 0f;
            //_destination = _foodBar.localPosition.x - _drain;
            _currFood -= _drain;
            //_foodBar.localPosition -= offset;
        }
        // float destination = _foodBar.localPosition.
        // float lerpedValue = Mathf.Lerp(_foodBar.localPosition.x,)
        //_foodBar.localPosition = new Vector3(Mathf.Lerp(_foodBar.localPosition.x, _destination, _timer),
        // _foodBar.localPosition.y, _foodBar.localPosition.z);

        _foodBar.fillAmount = _currFood / 150;

        
        // Vector3 offset = new Vector3(_drain, 0, 0);
        if (_currFood <= 0)
        {
            GameObject.Find("StateManager").GetComponent<GameStateManager>().HandleLose();
        }
    }

    void EatFood(GameObject food)
    {
        if (!BaitCheck(food) || gracePeriod)
        {
            Destroy(food);
            _currFood += _eatAmount;
            if (_currFood >= _maxFood)
            {
                _currFood = _maxFood;
            }
            else
            {
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
        ScoreManager.Instance.AddScore(15);
        return false;
    }
}
