using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private Image QImage;
    [SerializeField] private Image volume;
    [SerializeField] private GameObject boardPage;
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
        
        timerControl = StartCoroutine(timer());
        
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
        else if (GameManager.instance.statusGame == 20)
        {
            Debug.Log("end status : 20");
            endSequence();
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

    public void endSequence()
    {
        canvasGroup.alpha = 1;
        GameManager.instance.statusGame = 21;
        StopCoroutine(timerControl);
        boardScript = boardPage.GetComponent<BoardScript>();
        boardPage.SetActive(true);
        boardScript.endRoutine(score);
    }
}
