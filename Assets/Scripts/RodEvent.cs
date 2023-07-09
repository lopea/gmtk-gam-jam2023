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

    private GameObject _player;

    private Rigidbody _playerRb;

    private Vector3 _forceDir;

    private float _forceMag;

    private float _forceInterval;

    private float _timer;

    private BaitSpawner _spawner;

    private GameStateManager _gsm;

    private float _grace;
    private float _graceTimer;

    private bool _graceEnded;
    // Start is called before the first frame update
    void Start()
    {
        _line = GetComponent<LineRenderer>();
        _cameraTransform = GameObject.Find("CamPos").GetComponent<Transform>();
        _player = GameObject.Find("Player");
        _playerRb = _player.GetComponent<Rigidbody>();
        _eventStarted = false;
        _forceMag = 50f;
        _forceInterval = 1f;
        _timer = _forceInterval;
        _spawner = GameObject.Find("BaitSpawner").GetComponent<BaitSpawner>();
        _gsm = GameObject.Find("StateManager").GetComponent<GameStateManager>();
        _grace = 1f;
        _graceTimer = _grace;
        _line.SetPosition(0, Vector3.zero);
        _line.SetPosition(1, Vector3.zero);
        //_cameraTransform.position = new Vector3(0, 10, 0);
        //_cameraTransform.forward = new Vector3(0, -1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (_eventStarted)
        {
            UpdateRodEvent();
        }
    }

    public void StartRodEvent(GameObject bait)
    {
        if (!_eventStarted)
        {
            _eventStarted = true;
            _baitTrap = bait;
            Vector3 randomPos = Random.onUnitSphere * 10;
            randomPos.y = 7;
            transform.position = randomPos;
            transform.LookAt(new Vector3(0, 7, 0));
            _cameraTransform.position = new Vector3(_player.transform.position.x, 10, _player.transform.position.z);
            _line.SetPosition(0, _rodEndPoint.transform.position);
            _line.SetPosition(1, bait.transform.position);
            Camera.main.transform.rotation = Quaternion.Euler(90, 0, 0);
            StartCoroutine(Camera.main.GetComponent<CameraControls>().CameraLerpToRodEvent(_cameraTransform, 0.5f));
            Vector3 dir = transform.position - _player.transform.position;
            dir.Normalize();
            dir.y = 0;
            _forceDir = dir;
            foreach (var obj in _spawner._bait)
            {
                obj.SetActive(false);
            }
        }
    }

    private void UpdateRodEvent()
    {
        _graceTimer -= Time.deltaTime;
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            _playerRb.AddForce(_forceDir * _forceMag, ForceMode.Impulse);
            _timer = _forceInterval;
        }

        if (_graceTimer <= 0f)
        {
            _graceEnded = true;
        }
        _baitTrap.transform.position = _player.transform.position;
        _line.SetPosition(1, _baitTrap.transform.position);

        Vector3 rodDist = new Vector3(transform.position.x, 0, transform.position.z);
        if (Vector3.Magnitude(_player.transform.position - rodDist) <= 0.5f && _graceEnded)
        {
            _gsm.HandleLose();
        }

        if (Vector3.Magnitude(_player.transform.position - rodDist) >= 7f)
        {
            RodEventEnd();
        }
    }

    private void RodEventEnd()
    {
        _eventStarted = false;
        Destroy(_baitTrap);
        StartCoroutine(Camera.main.GetComponent<CameraControls>().CameraLerpToOriginal(Camera.main.transform, 0.3f));
        _line.SetPosition(1, Vector3.zero);
        _line.SetPosition(0, Vector3.zero);
        foreach (var obj in _spawner._bait)
        {
            obj.SetActive(true);
        }
    }
}
