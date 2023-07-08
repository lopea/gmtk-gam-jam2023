using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodEvent : MonoBehaviour
{
    [SerializeField] private GameObject _rodEndPoint;

    private GameObject _baitTrap;

    private LineRenderer _line;

    private Transform _cameraTransform;

    private bool _eventStarted;
    // Start is called before the first frame update
    void Start()
    {
        _line = GetComponent<LineRenderer>();
        _eventStarted = false;
        _cameraTransform.position = new Vector3(0, 10, 0);
        _cameraTransform.forward = new Vector3(0, -1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartRodEvent(GameObject bait)
    {
        _baitTrap = bait;
        _eventStarted = true;
        Vector3 randomPos = Random.onUnitSphere * 10;
        randomPos.y = 7;
        transform.position = randomPos;
        transform.LookAt(new Vector3(0, 7, 0));
        // need to lerp to original and then to new cam
        //StartCoroutine(Camera.main.GetComponent<CameraControls>().CameraLerpToOriginal(_cameraTransform, 0.5f));
        _line.SetPosition(0, _rodEndPoint.transform.position);
        _line.SetPosition(1, bait.transform.position);
    }
}
