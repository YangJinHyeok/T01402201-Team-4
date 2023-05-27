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
        main = Camera.main;
        main.GetComponent<MainCamera>().initEnd = false;
        StartCoroutine(initSequence());
    }
    
    private int currentSequntial = 0;
    private IEnumerator initSequence()
    {
        for (int i = 6; i <= 20; i++)
        {
            main.orthographicSize = i;
            yield return new WaitForSeconds(0.06f);
        }

        solidSpawner.GetComponent<CSVMapMaker>().readyForStart = true;
        boxSpawner.GetComponent<CSVMapMaker>().readyForStart = true;
        //Vector3 currentCamera = main.gameObject.transform.position;
        //float offset = Vector3.Distance(currentCamera, character.transform.position)/9;
        yield return new WaitForSeconds(4.0f);

        for (int i = 20; i >= 6; i--)
        {
            //main.gameObject.transform.position = Vector3.MoveTowards(currentCamera, character.transform.position, offset);
            main.orthographicSize = i;
            yield return new WaitForSeconds(0.06f);
        } 
        
        main.GetComponent<MainCamera>().initEnd = true;
        UIObject.gameObject.SetActive(true);
        currentSequntial++;
    }

    private void Update()
    {
        if (currentSequntial == 1)
        {
            Destroy(transform.gameObject);
        }
    }
}
