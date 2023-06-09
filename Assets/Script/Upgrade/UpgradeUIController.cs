using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpgradeUIController : MonoBehaviour
{
    [SerializeField]
    private Text CurrentLucci;
    [SerializeField]
    private Image Speed;
    [SerializeField]
    private Image Power;
    [SerializeField]
    private Image Count;
    [SerializeField]
    private Button SpeedUpButton;
    [SerializeField]
    private Button PowerUpButton;
    [SerializeField]
    private Button CountUpButton;
    [SerializeField]
    private Button CommitButton;
    [SerializeField]
    private Button CancleButton;

    private Text speedNeedLucci;
    private Text powerNeedLucci;
    private Text countNeedLucci;

    private GameObject upgrade1;
    private GameObject upgrade2;
    private GameObject upgrade3;
    private GameObject upgrade4;
    private GameObject SUButton;
    private GameObject PUButton;
    private GameObject CUButton;

    private int maxSpeed;
    private int maxPower;
    private int maxCount;
    private int firstLucci;

    private int currentMaxSpeedLevel;
    private int currentMaxPowerLevel;
    private int currentMaxCountLevel;

    // Start is called before the first frame update
    void Start()
    {
        Character.Instance.getFromCSV();

        maxSpeed = Character.Instance.getSpeedMax();
        maxPower = Character.Instance.getPowerMax();
        maxCount = Character.Instance.getCountMax();
        firstLucci = Character.Instance.getLucci();

        speedNeedLucci = Speed.transform.Find("SpeedNeedLucci").GetComponent<Text>();
        powerNeedLucci = Power.transform.Find("PowerNeedLucci").GetComponent<Text>();
        countNeedLucci = Count.transform.Find("CountNeedLucci").GetComponent<Text>();

        SpeedUpButton.onClick.AddListener(upgradeMaxSpeed);
        PowerUpButton.onClick.AddListener(upgradeMaxPower);
        CountUpButton.onClick.AddListener(upgradeMaxCount);
        CommitButton.onClick.AddListener(commitAll);
        CancleButton.onClick.AddListener(cancleAll);

        updateBoard();
    }

    private int getSpeedUpgradeLevel(int currentMaxSpeed)
    {
        switch (currentMaxSpeed)
        {
            case 6:
                return 0;
            case 7:
                return 1;
            case 8:
                return 2;
            case 9:
                return 3;
            case 10:
                return 4;
            default:
                return 0;
        }
    }

    private int getPowerUpgradeLevel(int currentMaxPower)
    {
        switch (currentMaxPower)
        {
            case 8:
                return 0;
            case 9:
                return 1;
            case 10:
                return 2;
            case 11:
                return 3;
            case 12:
                return 4;
            default:
                return 0;
        }
    }

    private int getCountUpgradeLevel(int currentMaxCount)
    {
        switch (currentMaxCount)
        {
            case 6:
                return 0;
            case 7:
                return 1;
            case 8:
                return 2;
            case 9:
                return 3;
            case 10:
                return 4;
            default:
                return 0;
        }
    }

    private int getNeedLucci(int statLevel)
    {
        switch (statLevel)
        {
            case 0:
                return 5000;
            case 1:
                return 10000;
            case 2:
                return 20000;
            case 3:
                return 50000;
            case 4:
                return 0;
            default:
                return 0;
        }
    }

    private void showMedal(Image stat, int UpgradeLevel)
    {
        upgrade1 = stat.transform.Find("Upgrade1").gameObject;
        upgrade2 = stat.transform.Find("Upgrade2").gameObject;
        upgrade3 = stat.transform.Find("Upgrade3").gameObject;
        upgrade4 = stat.transform.Find("Upgrade4").gameObject;
        if (UpgradeLevel >= 1)
        {
            upgrade1.SetActive(true);
        }
        if (UpgradeLevel >= 2)
        {
            upgrade2.SetActive(true);
        }
        if (UpgradeLevel >= 3)
        {
            upgrade3.SetActive(true);
        }
        if (UpgradeLevel >= 4)
        {
            upgrade4.SetActive(true);
        }
        if (UpgradeLevel < 1)
        {
            upgrade1.SetActive(false);
        }
        if (UpgradeLevel < 2)
        {
            upgrade2.SetActive(false);
        }
        if (UpgradeLevel < 3)
        {
            upgrade3.SetActive(false);
        }
        if (UpgradeLevel < 4)
        {
            upgrade4.SetActive(false);
        }
    }

    private void upgradeMaxSpeed()
    {
        int requireLucci = getNeedLucci(currentMaxSpeedLevel);
        if (requireLucci <= Character.Instance.getLucci())
        {
            maxSpeed += 1;
            Character.Instance.setLucci(-requireLucci);
            updateBoard();
        }
    }

    private void upgradeMaxPower()
    {
        int requireLucci = getNeedLucci(currentMaxPowerLevel);
        if (requireLucci <= Character.Instance.getLucci())
        {
            maxPower += 1;
            Character.Instance.setLucci(-requireLucci);
            updateBoard();
        }
    }

    private void upgradeMaxCount()
    {
        int requireLucci = getNeedLucci(currentMaxCountLevel);
        if (requireLucci <= Character.Instance.getLucci())
        {
            maxCount += 1;
            Character.Instance.setLucci(-requireLucci);
            updateBoard();
        }
    }


    private void updateBoard()
    {
        currentMaxSpeedLevel = getSpeedUpgradeLevel(maxSpeed);
        currentMaxPowerLevel = getPowerUpgradeLevel(maxPower);
        currentMaxCountLevel = getCountUpgradeLevel(maxCount);

        speedNeedLucci.text = string.Format("{0000} $", getNeedLucci(currentMaxSpeedLevel));
        powerNeedLucci.text = string.Format("{0000} $", getNeedLucci(currentMaxPowerLevel));
        countNeedLucci.text = string.Format("{0000} $", getNeedLucci(currentMaxCountLevel));
        CurrentLucci.text = string.Format("{000000} $", Character.Instance.getLucci());

        showMedal(Speed, currentMaxSpeedLevel);
        showMedal(Power, currentMaxPowerLevel);
        showMedal(Count, currentMaxCountLevel);

        if (currentMaxSpeedLevel == 4)
        {
            SUButton = Speed.transform.Find("SpeedUpButton").gameObject;
            SUButton.SetActive(false);
            speedNeedLucci.enabled = false;
        }
        else if (currentMaxSpeedLevel < 4)
        {
            SUButton = Speed.transform.Find("SpeedUpButton").gameObject;
            SUButton.SetActive(true);
            speedNeedLucci.enabled = true;
        }
        if (currentMaxPowerLevel == 4)
        {
            PUButton = Power.transform.Find("PowerUpButton").gameObject;
            PUButton.SetActive(false);
            powerNeedLucci.enabled = false;
        }
        else if (currentMaxPowerLevel < 4)
        {
            PUButton = Power.transform.Find("PowerUpButton").gameObject;
            PUButton.SetActive(true);
            powerNeedLucci.enabled = true;
        }
        if (currentMaxCountLevel == 4)
        {
            CUButton = Count.transform.Find("CountUpButton").gameObject;
            CUButton.SetActive(false);
            countNeedLucci.enabled = false;
        }
        else if (currentMaxCountLevel < 4)
        {
            CUButton = Count.transform.Find("CountUpButton").gameObject;
            CUButton.SetActive(true);
            countNeedLucci.enabled = true;
        }
    }

    private void commitAll()
    {
        Character.Instance.setSpeedMax(maxSpeed);
        Character.Instance.setPowerMax(maxPower);
        Character.Instance.setCountMax(maxCount);
        Character.Instance.saveToCSV();
        SceneManager.LoadScene("MainMenu");
    }

    private void cancleAll()
    {
        maxSpeed = Character.Instance.getSpeedMax();
        maxPower = Character.Instance.getPowerMax();
        maxCount = Character.Instance.getCountMax();
        int returnLucci = firstLucci - Character.Instance.getLucci();
        Character.Instance.setLucci(returnLucci);
        updateBoard();
    }
}
