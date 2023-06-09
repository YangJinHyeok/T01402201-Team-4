using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    private static Character instance = null; // instance 필드 추가


    // 다른 곳에서도 캐릭터의 정보를 가져올 수 있도록 static으로 선언
    public static Character Instance 
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Character>();
                if (instance == null)
                {
                    GameObject go = new GameObject("Character");
                    instance = go.AddComponent<Character>();
                }
            }
            return instance;
        }
    }

    private int speed;
    private int power;
    private int count;

    private int speedMax;
    private int countMax;
    private int powerMax;

    private int lucci = 0;
    public int getSpeed()
    {
        return speed;
    }
    public void setSpeed()
    {
        
    }
    public int getSpeedMax()
    {
        
        return speedMax;
    }

    public void setSpeedMax(int value)
    {
        Instance.speedMax = value;
    }

    public int getPower()
    {
        return power;
    }

    public void setPower()
    {
        
    }

    public int getCount()
    {
        return count;
    }

    public void setCount()
    {
        
    }

    public int getCountMax()
    {
        return countMax;
    }

    public void setCountMax(int value)
    {
        Instance.countMax = value;
    }
    
    public int getPowerMax()
    {
        return powerMax;
    }

    public void setPowerMax(int value)
    {
        Instance.powerMax = value;
    }

    public int getLucci()
    {
        return Instance.lucci;
    }

    public void setLucci(int value)
    {
        Instance.lucci += value;
    }

    public void getFromCSV()
    {
        string[] data = CSVData.Instance.readData();
        speed = int.Parse(data[0]);
        power = int.Parse(data[1]);
        count = int.Parse(data[2]);
        speedMax = int.Parse(data[3]);
        powerMax = int.Parse(data[4]);
        countMax = int.Parse(data[5]);
        lucci = int.Parse(data[6]);
    }

    public void saveToCSV()
    {
        CSVData.Instance.saveMaxAndStatus(speedMax, powerMax, countMax,lucci);
    }

}

