using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ItemSpeed"))
        {
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
