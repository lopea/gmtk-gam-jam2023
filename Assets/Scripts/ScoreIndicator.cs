using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreIndicator : MonoBehaviour
{
    private Rigidbody _rb;

    private TextMeshProUGUI _text;
    
    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        transform.LookAt(Camera.main.transform);
        transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 2f);
        _rb.velocity = Vector3.up;
        _text.color -= new Color(0, 0, 0, 50);
    }

    public void SetupText(float score, Vector3 pos)
    {
        transform.position = pos;
        _text.text = $"{score}";
    }
}
