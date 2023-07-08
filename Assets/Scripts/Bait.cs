using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bait : MonoBehaviour
{
    private float quickTime = 1.0f;
    private char quickAnswer;

    public GameObject baitCam;
    public GameObject baitText;
    public GameObject quickText;

    

    // Start is called before the first frame update
    void Start()
    {
        //Slow down time
        Time.timeScale = 0.1f;

        StartCoroutine(Camera.main.GetComponent<CameraControls>().CameraLerpFromOriginal(baitCam.transform, 0.1f));


        //Display the Quicktime Key (Either Q or E)
        int quickRand = Random.Range(0, 1);

        if (quickRand == 0)
        {
            quickText.GetComponent<TextMeshProUGUI>().text = "[Q]";
            quickAnswer = 'Q';
        }
        if (quickRand == 1)
        {
            quickText.GetComponent<TextMeshProUGUI>().text = "[E]";
            quickAnswer = 'E';
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Lerp Main camera over to bait camera

        //Start timer for quicktime over
        if (quickTime > 0)
            quickTime -= Time.deltaTime;

        //If key is correct, end event and have player "dodge" the bait trap
        if(quickAnswer == 'Q' && Input.GetKeyDown(KeyCode.Q))
        {
            Pass();
        }
        else if(quickAnswer == 'E' && Input.GetKeyDown(KeyCode.E))
        {
            Pass();
        }

        //If key is wrong or timer runs out, thing fails and move on to catch event.
    }

    void Pass()
    {
        Time.timeScale = 1.0f;
        StartCoroutine(Camera.main.GetComponent<CameraControls>().CameraLerpToOriginal(baitCam.transform, 0.3f));
        Destroy(gameObject, 0.3f);
    }
}