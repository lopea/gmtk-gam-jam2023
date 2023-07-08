using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainUI;

    [SerializeField] private GameObject _loseUI;

    [SerializeField] private GameObject _winUI;
    // Start is called before the first frame update
    void Start()
    {
        _mainUI.SetActive(true);
        _loseUI.SetActive(false);
        _winUI.SetActive(false);
    }

    public void HandleLose()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0f;
        _loseUI.SetActive(true);
    }
    
    public void HandleWin()
    {
        Cursor.lockState = CursorLockMode.Confined;
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