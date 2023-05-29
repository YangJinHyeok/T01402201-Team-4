using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private Image QImage;
    [SerializeField] private Image volume;
    public static int times = 300;
    public Text timeText;
    
    public static long score = times * 1000;
    public Text scoreText;

    public static long coin = 0;
    public Text coinText;

    private CanvasGroup canvasGroup;
    
    private void Start()
    {

        coinText.text = string.Format("{000000} $",coin);
        timeText.text = string.Format("{000}", times);
        scoreText.text = string.Format("Score : {0000000000}", score);
        canvasGroup = transform.GetComponent<CanvasGroup>();
        
        StartCoroutine(timer());
        
    }

    private void Update()
    {
        if (GameManager.instance.masterVol == -80.0f)
        {
            volume.color = Color.red;
        }
        else
        {
            volume.color = Color.white;
        }
        

        if (GameManager.instance.statusGame < 10)
        {
            canvasGroup.alpha = 0;
        }
        
        timeText.text = string.Format("{000}", times);
        scoreText.text = string.Format("Score : {0000000000}", score);   
        
        if (Input.GetKey(KeyCode.Escape) && GameManager.instance.statusGame is > 0 and < 11)
        {
            if (canvasGroup.alpha == 0)
            {
                canvasGroup.alpha = 1;
            }
            GameManager.instance.pauseGame();
            pauseUI.SetActive(true);
        }

        QImage.fillAmount = GameEffects.portalCooltime / 3.0f;
    }

    IEnumerator timer()
    {
        while (true)
        {
            if (GameManager.instance.statusGame != 10)
            {
                yield return null;
            }
            else
            {
                times--;
                score = score - 1000;

                yield return new WaitForSeconds(1);
            }
        }
    }

    public void updateScoreWIthValue(int value)
    {
        score += value;
        scoreText.text = string.Format("Score : {0000000000}", score);
    }
    
}
