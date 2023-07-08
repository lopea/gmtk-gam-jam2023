using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitCheck : MonoBehaviour
{
    private FoodSystem _system;

    private void Start()
    {
        _system = GameObject.Find("Player").GetComponent<FoodSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            _system.BaitCheck(transform.parent.gameObject);
        }
    }
}
