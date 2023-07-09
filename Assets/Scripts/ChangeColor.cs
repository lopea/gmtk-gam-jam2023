using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ChangeColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TMP_Text _text;

    [SerializeField] private Color idle, hovered, pressed;
    
    // Awake is called before the first frame update
    void Awake()
    {
        _text = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void ChangeToScene(int index)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(index);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _text.color = hovered;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _text.color = idle;
    }
}
