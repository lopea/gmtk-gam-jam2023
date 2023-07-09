using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bait : MonoBehaviour
{
    private float quickTime = 1.5f;
    private char quickAnswer;

    public GameObject baitCam;
    public GameObject baitText;
    public GameObject quickText;

    [SerializeField] private GameObject _rod;
    private bool _failed;


    void Awake()
    {
        quickTime = 2.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        quickTime = 1.5f;
        _rod = GameObject.Find("Rod");
        //Slow down time
        Time.timeScale = 0.1f;

        StartCoroutine(Camera.main.GetComponent<CameraControls>().CameraLerpFromOriginal(baitCam.transform, 0.05f));


        //Display the Quicktime Key (Either Q or E)
        int quickRand = Random.Range(0, 100);

        if (quickRand > 50)
        {
            quickText.GetComponent<TextMeshProUGUI>().text = "[Q]";
            quickAnswer = 'Q';
        }
        else
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
            quickTime -= Time.deltaTime * 10f;

        //If key is correct, end event and have player "dodge" the bait trap
        if(quickAnswer == 'Q' && Input.GetKeyDown(KeyCode.Q))
        {
            Pass();
        }
        else if(quickAnswer == 'E' && Input.GetKeyDown(KeyCode.E))
        {
            Pass();
        }
        if (quickTime <= 0f && !_failed)
        {
            Fail();
            _failed = true;
        }
        //If key is wrong or timer runs out, thing fails and move on to catch event.
    }

    void Pass()
    {
        Time.timeScale = 1.0f;
        StartCoroutine(Camera.main.GetComponent<CameraControls>().CameraLerpToOriginal(baitCam.transform, 0.3f));
        Destroy(gameObject, 0.3f);
    }

    void Fail()
    {
        Time.timeScale = 1.0f;
        _rod.GetComponent<RodEvent>().StartRodEvent(gameObject);
    }
}
