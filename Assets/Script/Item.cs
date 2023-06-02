using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {    
       // 현재  오브젝트(아이템)과 충돌한 오브젝트의 태그가 Player라면
        if (collision.gameObject.CompareTag("Player"))
        {
            GameEffects gameEffects = GetComponent<GameEffects>();
        // 현재 오브젝트(아이템)자체를 넘겨주기
            gameEffects.powerUp(transform.gameObject);

        // 현재 오브젝트(아이템) 삭제
            Destroy(gameObject);
        }

    }   
}

