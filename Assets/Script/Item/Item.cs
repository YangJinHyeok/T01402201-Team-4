using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;


public class Item : MonoBehaviour
{
    private GameObject gameController;
    private void Start()
    {
        gameController = GameObject.Find("GameController");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {    
       // 현재  오브젝트(아이템)과 충돌한 오브젝트의 태그가 Player라면
        if (collision.gameObject.CompareTag("Player"))
        {
            GameEffects gameEffects = gameController.GetComponent<GameEffects>();
            // 현재 오브젝트(아이템)자체를 넘겨주기
            gameEffects.powerUp(gameObject);
        }

        if (collision.gameObject.CompareTag("Explosion"))
        {
            Destroy(gameObject);
        }

    }   
}

