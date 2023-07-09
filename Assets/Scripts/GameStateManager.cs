using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainUI;

    [SerializeField] private GameObject _loseUI;

    [SerializeField] private GameObject _winUI;
    // Awake is called before the first frame update
    void Awake()
    {
        _mainUI.SetActive(true);
        _loseUI.SetActive(false);
        _winUI.SetActive(false);
    }

    public void HandleLose()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        _loseUI.SetActive(true);
    }
    
    public void HandleWin()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        _winUI.SetActive(true);
    }

    public void ResetGame()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        _loseUI.SetActive(false);
        _winUI.SetActive(false);
        _mainUI.SetActive(true);
    }
}
