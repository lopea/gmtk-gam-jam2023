using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bait : MonoBehaviour
{
    private float quickTime = 1.5f;
    private char quickAnswer;

    public GameObject baitCam;
    public GameObject quickText;

    [SerializeField] private GameObject _rod;
    private bool _failed;
    private bool _passed = false;




    // Awake is called before the first frame update
    void Awake()
    {
        quickTime = 1.5f;
        _rod = GameObject.Find("Rod");
        //Slow down time
        Time.timeScale = 0.1f;

        StartCoroutine(Camera.main.GetComponent<CameraControls>().CameraLerpFromOriginal(baitCam.transform, 0.05f));
    }

    // Update is called once per frame
    void Update()
    {
        //Lerp Main camera over to bait camera

        //Awake timer for quicktime over
        if (quickTime > 0)
            quickTime -= Time.deltaTime * 10f;
        else if (quickTime <= 0f && !_failed)
        {
            Fail();
            _failed = true;
        }
        //If key is wrong or timer runs out, thing fails and move on to catch event.
    }

    /*void Pass()
    {
        _passed = true;
        Time.timeScale = 1.0f;
        StartCoroutine(Camera.main.GetComponent<CameraControls>().CameraLerpToOriginal(baitCam.transform, 0.3f));
        Destroy(gameObject, 0.3f);
    }*/

    void Fail()
    {
        Time.timeScale = 1.0f;
        _rod.GetComponent<RodEvent>().StartRodEvent();
        Destroy(gameObject, 0.1f);
    }
}
