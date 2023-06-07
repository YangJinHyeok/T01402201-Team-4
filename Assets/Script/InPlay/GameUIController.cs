using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private Image QImage;
    [SerializeField] private Image volume;
    [SerializeField] private GameObject boardPage;
    [SerializeField] private GameObject endImage;
    public static int times = 300;
    public Text timeText;
    
    public static int score = 30000;
    public Text scoreText;

    public static int coin = 0;
    public Text coinText;

    private CanvasGroup canvasGroup;
    private BoardScript boardScript;
    private Coroutine timerControl;
    
    private void Start()
    {

        coinText.text = string.Format("{000000} $",coin);
        timeText.text = string.Format("{000}", times);
        scoreText.text = string.Format("Score : {0000000000}", score);
        canvasGroup = transform.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        boardPage.SetActive(false);
        timerControl = StartCoroutine(timer());
        
    }

    private void Update()
    {
        timeText.text = string.Format("{000}", times);
        scoreText.text = string.Format("Score : {0000000000}", score);
        
        if (Input.GetKey(KeyCode.Escape) && GameManager.instance.statusGame is > 0 and < 11)
        {
            canvasGroup.alpha = 1;
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
                score = score - 50;

                yield return new WaitForSeconds(1);
            }
        }
    }

    public void updateScoreWIthValue(int value)
    {
        score += value;
        scoreText.text = string.Format("Score : {0000000000}", score);
    }

    public void updateCoin(int value)
    {
        coin += value;
        coinText.text = string.Format("{000000} $",coin);
        Character.Instance.setLucci(value);
    }
    public void endSequence(bool isWin)
    {
        canvasGroup.alpha = 1;
        GameManager.instance.statusGame = 21;
        StopCoroutine(timerControl);
        StartCoroutine(callEndImage(isWin));
        boardScript = boardPage.GetComponent<BoardScript>();
    }
    IEnumerator callEndImage(bool isWin)
    {
        if (isWin)
        {
            endImage.transform.Find("Win").gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(3.0f);
            endImage.transform.Find("Win").gameObject.SetActive(false);
        }
        else
        {
            endImage.transform.Find("Lose").gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(3.0f);
            endImage.transform.Find("Lose").gameObject.SetActive(false);
        }

        yield return new WaitForSecondsRealtime(1.0f);
        boardPage.SetActive(true);
        boardScript.endRoutine(score);
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
