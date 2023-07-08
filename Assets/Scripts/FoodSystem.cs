using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSystem : MonoBehaviour
{
    public float percentChanceBait = 20f;

    public float baitDelay = 2f;

    [SerializeField] private float _drain = 5f;

    // please keep this 100 lol
    [SerializeField] private float _maxFood = 100f;

    [SerializeField] private float _eatAmount = 15f;

    private RectTransform _foodBar;

    private float _currFood;

    private float _timer;
    
    private float _destination;
    void Start()
    {
        _foodBar = GameObject.Find("FoodMeter").GetComponent<RectTransform>();
        _currFood = _maxFood;
        _timer = 0f;
        _drain = 10f;
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
}
