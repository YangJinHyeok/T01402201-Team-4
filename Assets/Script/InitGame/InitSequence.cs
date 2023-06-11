using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class InitSequence : MonoBehaviour
{
    private CanvasGroup UIController;
    
    private Camera main;
    
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.statusGame == 12 | GameManager.instance.statusGame == 13)
        {
            main = Camera.main;
            Destroy(transform.gameObject);
        }
        else
        {
            UIController = GameObject.Find("Canvas").GetComponent<CanvasGroup>();
            main = Camera.main;
            StartCoroutine(initSequence());
        }
    }
    
    private IEnumerator initSequence()
    {
        for (int i = 6; i <= 20; i++)
        {
            main.orthographicSize = i;
            yield return null;
        }

        GameManager.instance.statusGame = 1;
        while (GameManager.instance.statusGame != 3)
        {
            yield return null;
        }
        
        GameManager.instance.statusGame = 4;
        while (GameManager.instance.statusGame != 6)
        {
            yield return null;
        }
        
        for (int i = 20; i >= 6; i--)
        {
            main.orthographicSize = i;
            yield return new WaitForSeconds(0.04f);
        }

        UIController.alpha = 1;
        GameManager.instance.statusGame = 10;
        
    }

    private void Update()
    {
        if (GameManager.instance.statusGame == 10)
        {
            Destroy(transform.gameObject);
        }
    }
}
