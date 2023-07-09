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

    // Start is called before the first frame update
    private void Awake()
    {
        phaseTwo = false;
        phaseThree = false;
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad > phaseTwoThreshold)
        {
            if (!phaseTwo)
            {
                transform.localScale += new Vector3(2.5f, 0, 2.5f);
            }
            phaseTwo = true;
        }

        if (Time.timeSinceLevelLoad > phaseThreeThreshold)
        {
            phaseThree = true;
        }
        if (inZone)
            surviveTime -= Time.deltaTime * 0.1f;
        else
            surviveTime += Time.deltaTime * 0.1f;

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
            Vector3 rand = new Vector3(Random.Range(-0.1f, 0.1f), 0, Random.Range(-0.1f, 0.1f));
            dir += rand;
            dir.Normalize();
            dir.y = 0;
            //dir *= 2f;
            GetComponent<Rigidbody>().AddForce(dir * Time.deltaTime, ForceMode.VelocityChange);
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
