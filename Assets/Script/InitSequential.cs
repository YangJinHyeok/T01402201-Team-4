using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;


public class InitSequential : MonoBehaviour
{
    public float placementDelay = 0.1f;
    private GameObject boxParent;
    private GameObject solidParent;
    // Start is called before the first frame update
    void Start()
    {
        GameObject boxParent = GameObject.Find("Box");
        GameObject solidParent = GameObject.Find("Solid");
        //카메라 size 증가
        StartCoroutine(placeBoxAndSolid());
        //카메라 정상화
    }

    private IEnumerator placeBoxAndSolid()
    {
        Vector3 leftBottomPoint = new Vector3(-28, -15, 0);
        Vector3 leftTopPoint = new Vector3(-28, 14, 0);
        Vector3 rightBottomPoint = new Vector3(27, -15, 0);
        Vector3 rightUpPoint = new Vector3(27, 14, 0);
        
        yield return new WaitForSeconds(placementDelay);
    }
}
