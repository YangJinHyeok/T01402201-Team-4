using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    private static Item thisItem;

    private void Awake()
    {
        thisItem = this;
    }

    public static Item GetItem()
    {
        return thisItem;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
    

        // 현재  오브젝트(아이템)과 충돌한 오브젝트의 태그가 Player라면
        if (collision.gameObject.CompareTag("Player"))
        {
            
        // 현재 오브젝트(아이템) 리턴


        // 현재 오브젝트(아이템) 삭제
            Destroy(gameObject);

    
            
        }



    }   
}
