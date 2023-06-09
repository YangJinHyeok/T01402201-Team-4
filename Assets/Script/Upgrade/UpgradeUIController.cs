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

    private int speed;
    private int power;
    private int count;
    private int firstLucci;

    private int currentSpeed;
    private int currentPower;
    private int currentCount;

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

    // Start is called before the first frame update
    void Start()
    {
        Character.Instance.getFromCSV();

        speed = Character.Instance.getSpeedMax();
        power = Character.Instance.getPowerMax();
        count = Character.Instance.getCountMax();
        firstLucci = Character.Instance.getLucci();

        speedNeedLucci = Speed.transform.Find("SpeedNeedLucci").GetComponent<Text>();
        powerNeedLucci = Power.transform.Find("PowerNeedLucci").GetComponent<Text>();
        countNeedLucci = Count.transform.Find("CountNeedLucci").GetComponent<Text>();

        SpeedUpButton.onClick.AddListener(upgradeSpeed);
        PowerUpButton.onClick.AddListener(upgradePower);
        CountUpButton.onClick.AddListener(upgradeCount);
        CommitButton.onClick.AddListener(commitAll);
        CancleButton.onClick.AddListener(cancleAll);

        updateBoard();
    }

    private int getUpgradeSpeed(int speed)
    {
        switch (speed)
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

    private int getUpgradePower(int power)
    {
        switch (power)
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

    private int getUpgradeCount(int count)
    {
        switch (count)
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

    private int getNeedLucci(int stat)
    {
        switch (stat)
        {
            case 0:
                return 100;
            case 1:
                return 200;
            case 2:
                return 500;
            case 3:
                return 1000;
            case 4:
                return 0;
            default:
                return 0;
        }
    }

    private void showMedal(Image stat, int amount)
    {
        upgrade1 = stat.transform.Find("Upgrade1").gameObject;
        upgrade2 = stat.transform.Find("Upgrade2").gameObject;
        upgrade3 = stat.transform.Find("Upgrade3").gameObject;
        upgrade4 = stat.transform.Find("Upgrade4").gameObject;
        if (amount >= 1)
        {
            upgrade1.SetActive(true);
        }
        if (amount >= 2)
        {
            upgrade2.SetActive(true);
        }
        if (amount >= 3)
        {
            upgrade3.SetActive(true);
        }
        if (amount >= 4)
        {
            upgrade4.SetActive(true);
        }
        if (amount < 1)
        {
            upgrade1.SetActive(false);
        }
        if (amount < 2)
        {
            upgrade2.SetActive(false);
        }
        if (amount < 3)
        {
            upgrade3.SetActive(false);
        }
        if (amount < 4)
        {
            upgrade4.SetActive(false);
        }
    }

    private void upgradeSpeed()
    {
        int requireLucci = getNeedLucci(currentSpeed);
        if (requireLucci <= Character.Instance.getLucci())
        {
            speed += 1;
            Character.Instance.setLucci(-requireLucci);
            updateBoard();
        }
    }

    private void upgradePower()
    {
        int requireLucci = getNeedLucci(currentPower);
        if (requireLucci <= Character.Instance.getLucci())
        {
            power += 1;
            Character.Instance.setLucci(-requireLucci);
            updateBoard();
        }
    }

    private void upgradeCount()
    {
        int requireLucci = getNeedLucci(currentCount);
        if (requireLucci <= Character.Instance.getLucci())
        {
            count += 1;
            Character.Instance.setLucci(-requireLucci);
            updateBoard();
        }
    }


    private void updateBoard()
    {
        Debug.Log("Update");
        currentSpeed = getUpgradeSpeed(speed);
        currentPower = getUpgradePower(power);
        currentCount = getUpgradeCount(count);

        speedNeedLucci.text = string.Format("{0000} $", getNeedLucci(currentSpeed));
        powerNeedLucci.text = string.Format("{0000} $", getNeedLucci(currentPower));
        countNeedLucci.text = string.Format("{0000} $", getNeedLucci(currentCount));
        CurrentLucci.text = string.Format("{000000} $", Character.Instance.getLucci());

        showMedal(Speed, currentSpeed);
        showMedal(Power, currentPower);
        showMedal(Count, currentCount);

        if (currentSpeed == 4)
        {
            SUButton = Speed.transform.Find("SpeedUpButton").gameObject;
            SUButton.SetActive(false);
            speedNeedLucci.enabled = false;
        }
        else if (currentSpeed < 4)
        {
            SUButton = Speed.transform.Find("SpeedUpButton").gameObject;
            SUButton.SetActive(true);
            speedNeedLucci.enabled = true;
        }
        if (currentPower == 4)
        {
            PUButton = Power.transform.Find("PowerUpButton").gameObject;
            PUButton.SetActive(false);
            powerNeedLucci.enabled = false;
        }
        else if (currentPower < 4)
        {
            PUButton = Power.transform.Find("PowerUpButton").gameObject;
            PUButton.SetActive(true);
            powerNeedLucci.enabled = true;
        }
        if (currentCount == 4)
        {
            CUButton = Count.transform.Find("CountUpButton").gameObject;
            CUButton.SetActive(false);
            countNeedLucci.enabled = false;
        }
        else if (currentCount < 4)
        {
            CUButton = Count.transform.Find("CountUpButton").gameObject;
            CUButton.SetActive(true);
            countNeedLucci.enabled = true;
        }
    }

    private void commitAll()
    {
        Character.Instance.setSpeedMax(speed);
        Character.Instance.setPowerMax(power);
        Character.Instance.setCountMax(count);
        Character.Instance.saveToCSV();
        SceneManager.LoadScene("MainMenu");
    }

    private void cancleAll()
    {
        speed = Character.Instance.getSpeedMax();
        power = Character.Instance.getPowerMax();
        count = Character.Instance.getCountMax();
        int returnLucci = firstLucci - Character.Instance.getLucci();
        Character.Instance.setLucci(returnLucci);
        updateBoard();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
