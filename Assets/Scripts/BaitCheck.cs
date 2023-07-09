using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitCheck : MonoBehaviour
{
    private FoodSystem _system;
    private BaitSoundMaker _sound;

    private void Awake()
    {
        _system = GameObject.Find("Player").GetComponent<FoodSystem>();
        _sound = GetComponent<BaitSoundMaker>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            _sound.Play(_system.BaitCheck(transform.parent.gameObject));
        }
    }
}
