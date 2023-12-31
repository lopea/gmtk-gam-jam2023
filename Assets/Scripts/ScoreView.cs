using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    private TMP_Text _text;

    // Awake is called before the first frame update
    void Awake()
    {
        _text = GetComponentInChildren<TMP_Text>();
        _text.text = " Score 0";
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = $" Score {ScoreManager.Instance.Score}";
    }
}
