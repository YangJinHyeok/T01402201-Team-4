using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ItemSpeed"))
        {
            Debug.Log("스피드 충;"); 
            Destroy(collision.gameObject);
            IncreaseSpeed();
        }
        else if (collision.gameObject.CompareTag("ItemCount"))
        {
            Destroy(collision.gameObject);
            IncreaseCount();
        }
        else if (collision.gameObject.CompareTag("ItemPower"))
        {
            Destroy(collision.gameObject);
            IncreasePower();
        }
        else if (collision.gameObject.CompareTag("ItemSuperPower"))
        {
            Destroy(collision.gameObject);
            
        }
        else if (collision.gameObject.CompareTag("Lucci"))
        {
            Destroy(collision.gameObject);
            
        }
    }


    public void IncreaseSpeed()
    {

    }
    public void IncreaseCount()
    {

    }
    public void IncreasePower()
    {

    }

    
}
