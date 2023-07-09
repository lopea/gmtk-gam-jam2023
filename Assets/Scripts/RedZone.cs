using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedZone : MonoBehaviour
{

    public RodEvent rodRef;
    private float surviveTime = 0.5f;
    private float surviveMax = 1.0f;
    private bool inZone = false;

 
    public  GameObject bar;

    // Awake is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
