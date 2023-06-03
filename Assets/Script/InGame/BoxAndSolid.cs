using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxAndSolid : MonoBehaviour
{
    public void effectByBomb()
    {
        int childs = transform.childCount;
        if (transform.CompareTag("Box"))
        {
            if (childs > 2)
            {
                for (int i = 0; i < childs; i++)
                {
                    Transform child = transform.GetChild(i);
                    string childTag = child.tag;
                    if (childTag.Equals("ItemCount") | childTag.Equals("ItemPower") |
                        childTag.Equals("ItemSpeed") | childTag.Equals("ItemSuperPower") | childTag.Equals("Lucci"))
                    {
                        child.gameObject.SetActive(true);
                        child.SetParent(null);
                        child.position = transform.position;
                        break;
                    }
                }
            }
            Destroy(transform.gameObject);
        }
        else
        {
            StartCoroutine(shake());
        }
    }

    IEnumerator shake()
    {
        float time = 0.5f;
        float force = 0.3f;
        Vector3 origin = transform.position;

        while (time > 0.0f)
        {
            time -= 0.05f;
            transform.position = origin + (Vector3)Random.insideUnitCircle * (force * time);
            yield return null;
        }

        transform.position = origin;
    }
}
