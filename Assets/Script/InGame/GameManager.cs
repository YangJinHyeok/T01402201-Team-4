using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] private int setStatusForTest;
    
    /*
     * -1 : Main menu
     * 0~9 : Init sequence
     * 10 : Now playing
     * 11 : Paused
     * 12 : Map Editor mode
     * 13 : Spawn Editor mode
     * 20-21 : Game end sequence
     * 22 : Edit End
     */
    public int statusGame = -1;
    
    private int lastStatus;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        
        if (setStatusForTest != 0)
        {
            statusGame = setStatusForTest;
        }
        
    }

    public void startGame()
    {
        statusGame = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void pauseGame()
    {
        lastStatus = statusGame;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0;
        statusGame = 11;
    }
    
    public void resumeGame()
    {
        Debug.Log("clicked");
        statusGame = lastStatus;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        GameObject.Find("Pause").SetActive(false);
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

    public void EndEdit()
    {
        statusGame = 22;
    }
    
}
