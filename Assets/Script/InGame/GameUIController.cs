using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private Image QImage;
    
    public static int times = 300;
    public Text timeText;
    
    public static long score = times * 1000;
    public Text scoreText;

    public static long coin = 0;
    public Text coinText;

    
    private void Start()
    {

        coinText.text = string.Format("{000000} $",coin);
        timeText.text = string.Format("{000}", times);
        scoreText.text = string.Format("Score : {000000000}", score);
        StartCoroutine(timer());
        
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && GameManager.instance.statusGame is > 0 and < 11)
        {
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
                timeText.text = string.Format("{000}", times);
                scoreText.text = string.Format("Score : {000000000}", score);

                yield return new WaitForSeconds(1);
            }
        }
    }

    public void updateScoreWIthValue(int value)
    {
        score += value;
        scoreText.text = string.Format("Score : {000000000}", score);
    }
    
}
