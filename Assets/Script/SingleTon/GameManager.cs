using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    /*
     * -1 : Main menu
     * 0~9 : Init sequence
     * 10 : Now playing
     * 11 : Paused
     * 12 : Map Editor mode
     * 13 : Spawn Editor mode
     * 21 : Game end sequence
     * 22 : Edit End
     */
    public int statusGame = -1;
    
    private int lastStatus;
    public AudioMixer mixer;
    public float masterVol;
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

        mixer.GetFloat("MasterVol", out masterVol);
    }

    public void startGame()
    {
        instance.statusGame = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene("InPlay");

    }

    public void pauseGame()
    {
        instance.lastStatus = new int();
        instance.lastStatus = instance.statusGame;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0;
        instance.statusGame = 11;
    }
    
    public void resumeGame()
    {
        instance.statusGame = instance.lastStatus;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        GameObject.Find("Pause").SetActive(false);
    }

    public void replayGame()
    {
        instance.statusGame = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    
    public void EndGame()
    {
        Application.Quit();
    }

    public void EndEdit()
    {
        instance.statusGame = 22;
    }

    public void MainMenu()
    {
        Character.Instance.saveToCSV();
        instance.statusGame = -1;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 1;
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    public void changeVol()
    {
        mixer.GetFloat("MasterVol", out masterVol);
        if (masterVol == 0.0f)
        {
            masterVol = -80.0f;
        }
        else
        {
            masterVol += 20.0f;
        }

        mixer.SetFloat("MasterVol", masterVol);

    }
}
