using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class CameraInit : MonoBehaviour
{
    private Camera main;

    public GameObject boxSpawner;

    public GameObject solidSpawner;
    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main;
        main.GetComponent<MainCamera>().initEnd = false;
        StartCoroutine(cameraZoom());
        main.GetComponent<MainCamera>().initEnd = true;

        //transform.gameObject.SetActive(false);
    }
    private int currentSequntial = 0;
    private IEnumerator cameraZoom()
    {
        for (int i = 6; i <= 20; i++)
        {
            main.orthographicSize = i;
            yield return new WaitForSeconds(0.06f);
        }

        solidSpawner.GetComponent<CSVMapMaker>().readyForStart = true;
        boxSpawner.GetComponent<CSVMapMaker>().readyForStart = true;
        yield return new WaitForSeconds(4.0f);

        for (int i = 20; i >= 6; i--)
        {
            main.orthographicSize = i;
            yield return new WaitForSeconds(0.06f);
        }

        currentSequntial++;
    }
    /*private IEnumerator CameraMoving()
    {
        
    }*/

    private void Update()
    {
        if (currentSequntial == 1)
        {
            Destroy(transform.gameObject);
        }
    }
}
