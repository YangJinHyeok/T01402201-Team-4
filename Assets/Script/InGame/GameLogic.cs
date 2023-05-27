using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    
    /*
     * 0~9 : Init sequence
     * 10 : Now playing
     * 11 : Paused
     * 12 : Map Editor mode
     * 13 : Spawn Editor mode
     * 20-21 : Game end sequence
     * 22 : Edit End
     */
    public static int statusGame = 0;
    
    private int lastStatus;
    private void Awake()
    {
        statusGame = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && statusGame < 11)
        {
            lastStatus = statusGame;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 0;
            statusGame = 11;
            pauseUI.SetActive(true);
        }
    }
    public void resumeGame()
    {
        Debug.Log("clicked");
        statusGame = lastStatus;
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
        statusGame = 20;
    }

}
