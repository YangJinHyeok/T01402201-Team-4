using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorUIController : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject boxAndSolidPanel;
    [SerializeField] private GameObject MobPanel;
    [SerializeField] private Image volume;

    private void Start()
    {
        boxAndSolidPanel.SetActive(true);
        MobPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            GameManager.instance.pauseGame();
            pauseUI.SetActive(true);
        }
    }
    
    public void changeVolButton()
    {
        if (GameManager.instance.masterVol == -80.0f)
        {
            volume.color = Color.red;
        }
        else
        {
            volume.color = Color.white;
        }
    }
}
