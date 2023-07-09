using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreIndicator : MonoBehaviour
{
    private Rigidbody _rb;

    private TMP_Text _text;
    
    // Start is called before the first frame update
    void Awake()
    {
        _text = GetComponentInChildren<TMP_Text>();
        _rb = GetComponent<Rigidbody>();
        transform.LookAt(_text.transform);
        transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 2f);
        _rb.velocity = Vector3.up;
        _text.transform.LookAt(Camera.main.transform);
        _text.transform.rotation = _text.transform.rotation * Quaternion.Euler(0, 180, 0);
        _text.color -= new Color(0, 0, 0, Time.deltaTime * .5f);
    }

    public void SetupText(float score, Vector3 pos)
    {
        transform.position = pos;
        _text.text = $"{score}";
    }
}
