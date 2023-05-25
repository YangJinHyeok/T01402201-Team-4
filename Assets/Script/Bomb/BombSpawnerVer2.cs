using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using UnityEngine;

public class BombSpawnerVer2 : MonoBehaviour
{
    // 폭탄 수 
    public int maxBomb = 5;
    public int minBomb = 1;
    public int maxPower = 5;
    public int minPower = 1;
    // 폭탄 할당 
    public GameObject bombObj;
    // 타일크기 가져오기 
    static public float TileSize = 1f;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void Bombspawn()
    {

        if (minBomb != 0)
        {
            //폭탄 위치 반올림 해서 스폰 하기 
            Vector3 createPosition = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
            GameObject bomb = Instantiate(bombObj, createPosition, transform.rotation);
            minBomb -= 1;
            //4초뒤 폭탄 반환 (삭제) 
            Invoke("BombReturn", 4f);
        }

    }
    //폭탄 터진뒤 폭탄 갯수 반환 
    public void BombReturn()
    {
        minBomb += 1;
    }

    // 아이템 충돌 시 최소 폭탄 갯수 변경 
    public void Bombcount()
    {
        if (minBomb < maxBomb)
        {
            minBomb += 1;
            
        }
    }
    // 아이템 충동시 최소 폭탄  파워 반환 
    public void BombPower()
    {
        if (minPower < maxPower)
        {
            minPower += 1;
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}