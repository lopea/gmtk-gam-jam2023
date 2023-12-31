using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RedZone : MonoBehaviour
{

    public RodEvent rodRef;
    private float surviveTime = 0.5f;
    private float surviveMax = 1.0f;
    private bool inZone = false;

    public int phaseTwoThreshold = 15;
    public int phaseThreeThreshold = 30;
    public bool phaseTwo;
    public bool phaseThree;
    


    public  GameObject bar;
    private GameObject _player;

    private float _zoneMultiplier;
    // Start is called before the first frame update
    private void Awake()
    {
        phaseTwo = false;
        phaseThree = false;
        _player = GameObject.Find("Player");
        _zoneMultiplier = 1;

        transform.localScale += new Vector3(Time.timeSinceLevelLoad / 45.0f, 0, Time.timeSinceLevelLoad / 45.0f);

        if (Time.timeSinceLevelLoad > phaseTwoThreshold)
        {
            if (!phaseTwo)
            {
                transform.localScale += new Vector3(2.5f, 0, 2.5f);
                _zoneMultiplier = 1.5f;
            }
            phaseTwo = true;
        }

        if (Time.timeSinceLevelLoad > phaseThreeThreshold)
        {
            _zoneMultiplier = 2f;
            phaseThree = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inZone)
            surviveTime -= Time.deltaTime * 0.1f * _zoneMultiplier;
        else
            surviveTime += Time.deltaTime * 0.2f;

        if (surviveTime > surviveMax)
        {
            rodRef.RodEventEnd();
            Destroy(gameObject);
        }
        else if (surviveTime < 0.0)
        {
            GameObject.Find("StateManager").GetComponent<GameStateManager>().HandleLose();
            Destroy(gameObject);
        }

        bar.transform.localScale = new Vector3(surviveTime, 1, 1);
        if (phaseThree)
        {
            Vector3 dir = _player.transform.position - transform.position;
            //Vector3 rand = new Vector3(Random.Range(-0.1f, 0.1f), 0, Random.Range(-0.1f, 0.1f));
            //dir += rand;
            dir.Normalize();
            dir.y = 0;
        //dir *= 2f;

            GetComponent<Rigidbody>().velocity = dir ;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            inZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            inZone = false;
        }
    }
}
