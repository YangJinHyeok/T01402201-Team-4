using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    private bool paused = false;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 0;
            paused = true;
            pauseUI.SetActive(true);
        }
    }
    public void resumeGame()
    {
        Debug.Log("clicked");
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void replayGame()
    {
        Debug.Log("replay");
    }
    
    public void EndGame()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void OptionControl()
    {
        Debug.Log("option panel on");
    }
    
}
