using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class InitSequence : MonoBehaviour
{
    [SerializeField] private GameObject UIObject;
    
    private Camera main; //나중에 serializeField로 바꿀 예정

    //[SerializeField] private GameObject character;

    [SerializeField] private GameObject boxSpawner;

    [SerializeField] private GameObject solidSpawner;
    
    // Start is called before the first frame update
    void Start()
    {
        if (GameLogic.statusGame == 12 | GameLogic.statusGame == 13)
        {
            main = Camera.main;
            main.GetComponent<MainCamera>().initEnd = true;
            Destroy(transform.gameObject);
        }
        else
        {
            main = Camera.main;
            main.GetComponent<MainCamera>().initEnd = false;
            StartCoroutine(initSequence());
        }
    }
    
    private IEnumerator initSequence()
    {
        for (int i = 6; i <= 20; i++)
        {
            main.orthographicSize = i;
            yield return new WaitForSeconds(0.06f);
        }

        GameLogic.statusGame = 1;
        while (GameLogic.statusGame != 3)
        {
            yield return null;
        }
        
        GameLogic.statusGame = 4;
        while (GameLogic.statusGame != 5)
        {
            yield return null;
        }
        
        for (int i = 20; i >= 6; i--)
        {
            main.orthographicSize = i;
            yield return new WaitForSeconds(0.06f);
        }
        
        main.GetComponent<MainCamera>().initEnd = true;
        UIObject.gameObject.SetActive(true);
        GameLogic.statusGame = 10;
        
    }

    private void Update()
    {
        if (GameLogic.statusGame == 10)
        {
            Destroy(transform.gameObject);
        }
    }
}
