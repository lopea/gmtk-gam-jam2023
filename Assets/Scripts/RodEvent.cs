using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodEvent : MonoBehaviour
{
    [SerializeField] private GameObject _rodEndPoint;

    public Vector3 ogRodPos;

    public RedZone RedZone;
    private RedZone Cur_RedZone;

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

    private FoodSystem _foodSystem;

    private float _grace;
    private float _graceTimer;

    private bool _graceEnded;
    // Start is called before the first frame update
    void Start()
    {
        ogRodPos = transform.position;
        _line = GetComponent<LineRenderer>();
        _cameraTransform = GameObject.Find("CamPos").GetComponent<Transform>();
        _player = GameObject.Find("Player");
        _playerRb = _player.GetComponent<Rigidbody>();
        _eventStarted = false;
        _forceMag = 3f; 
        _forceInterval = 1f;
        _timer = _forceInterval;
        _spawner = GameObject.Find("BaitSpawner").GetComponent<BaitSpawner>();
        _gsm = GameObject.Find("StateManager").GetComponent<GameStateManager>();
        _grace = 3f;
        _graceTimer = _grace;
        _line.SetPosition(0, Vector3.zero);
        _line.SetPosition(1, Vector3.zero);
        _foodSystem = _player.GetComponent<FoodSystem>();
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

    public void StartRodEvent()
    {
        if (!_eventStarted)
        {
            _eventStarted = true;
            Vector3 randomPos = new Vector3(Random.Range(0, 1f), 0, Random.Range(0, 1f));
            randomPos *= 4f;
            randomPos.y = 7;
            transform.position = _player.transform.position + randomPos;
            transform.LookAt(new Vector3(_player.transform.position.x, 7, _player.transform.position.z));
            _cameraTransform.position = new Vector3(_player.transform.position.x, 15, _player.transform.position.z);
            _line.SetPosition(0, _rodEndPoint.transform.position);
            _line.SetPosition(1, _player.transform.position);
            StartCoroutine(Camera.main.GetComponent<CameraControls>().CameraLerpToRodEvent(_cameraTransform, 0.5f));
            Vector3 dir = transform.position - _player.transform.position;
            dir.Normalize();
            dir.y = 0;
            _graceTimer = _grace;
            _timer = _forceInterval;
            _graceEnded = false;
            _forceDir = dir;
            foreach (var obj in _spawner._bait)
            {
                if(obj != null)
                    obj.SetActive(false);
            }

            Cur_RedZone = Instantiate(RedZone, _player.transform.position, Quaternion.identity, null);
            Cur_RedZone.rodRef = this;
        }
    }

    private void UpdateRodEvent()
    {
        _graceTimer -= Time.deltaTime;
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            Vector3 dir = Cur_RedZone.transform.position - _player.transform.position;
            dir.Normalize();
            dir.y = 0;
            _forceDir = dir;
            _playerRb.AddForce(_forceDir * _forceMag, ForceMode.Impulse);
            _timer = _forceInterval;
        }

        if (_graceTimer <= 0f)
        {
            _graceEnded = true;
        }
        _line.SetPosition(1, _player.transform.position);

        Vector3 rodDist = new Vector3(transform.position.x, 0, transform.position.z);
        
        /*if (Vector3.Magnitude(_player.transform.position - rodDist) <= 3f && _graceEnded)
        {
            _gsm.HandleLose();
        }

        if (Vector3.Magnitude(_player.transform.position - rodDist) >= 14f)
        {
            RodEventEnd();
        }*/
    }

    public void RodEventEnd()
    {
        transform.position = ogRodPos;
        _eventStarted = false;
        StartCoroutine(Camera.main.GetComponent<CameraControls>().CameraLerpToOriginal(Camera.main.transform, 0.3f));
        _line.SetPosition(1, Vector3.zero);
        _line.SetPosition(0, Vector3.zero);
        foreach (var obj in _spawner._bait)
        {
            if(obj != null)
                obj.SetActive(true);
        }

        _foodSystem.gracePeriod = true;
    }
}
