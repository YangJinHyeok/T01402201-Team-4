using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EditorUIController : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject EndPanel;
    [SerializeField] private GameObject boxAndSolidPanel;
    [SerializeField] private GameObject MobPanel;
    [SerializeField] private Image volume;
    [SerializeField] private Image ErrorMessage;
    
    private void Start()
    {
        boxAndSolidPanel.SetActive(true);
        MobPanel.SetActive(false);
        pauseUI.SetActive(false);
        EndPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && GameManager.instance.statusGame != 22)
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

    public void NextStep()
    {
        if (GameManager.instance.statusGame == 12)
        {
            boxAndSolidPanel.SetActive(false);
            MobPanel.SetActive(true);
            GameManager.instance.statusGame = 13;
        }
        else if (GameManager.instance.statusGame == 13)
        {
            GameManager.instance.statusGame = 22;
            EndPanel.SetActive(true);
        }
    }

    public void messagingError()
    {
        GameObject instantiate = Instantiate(ErrorMessage.gameObject, transform);
        Destroy(instantiate, 1.0f);
    }
}
