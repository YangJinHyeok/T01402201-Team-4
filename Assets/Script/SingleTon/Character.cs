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

    public float speed = 4.0f;
    public int power = 3;
    public int count = 1;
    public int remaining;

    public float speedMax = 8.0f;
    public int countMax = 10;
    public int powerMax = 14;


    public void isTrap()
    {
        speed = 1.0f;
    }
    public float getSpeed()
    {
        return speed;
    }
    public void setSpeed()
    {
        
    }
    public float getSpeedMax()
    {
        
        return speedMax;
    }

    public void setSpeedMax()
    {
        
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

    public void setCountMax()
    {
        
    }
    
    public int getPowerMax()
    {
        return powerMax;
    }

    public void setPowerMax()
    {
        
    }

    private void getFromCSV()
    {
        
    }

    private void saveToCSV()
    {
        
    }

}

