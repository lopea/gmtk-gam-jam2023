using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitCheck : MonoBehaviour
{
    private FoodSystem _system;

    private void Awake()
    {
        _system = GameObject.Find("Player").GetComponent<FoodSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _system.BaitCheck(transform.parent.gameObject);
        }
    }
}
