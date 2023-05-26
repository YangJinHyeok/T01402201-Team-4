using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CSVMapMaker : MonoBehaviour
{
    public string fileName = "Map";
    public string prefabName;
    public string prefabFolder;
    public float positionX;
    public float positionY;
    public float placementDelay = 0.1f;
    public bool readyForStart = false;
    
    private GameObject prefab;
    private GameObject parent;
    private bool isEnd = false;
    List<Dictionary<string, object>> dicList = new List<Dictionary<string, object>>();
    
    
    private IEnumerator LoadCSVMap(int length)
    {
        while (!readyForStart)
        {
            yield return null;
        }

        float currentX = float.MinValue;
        for (int i = 0; i < length; i++)
        {
            prefabName = dicList[i]["prefapName"].ToString();
            positionX = float.Parse(dicList[i]["positionX"].ToString());
            positionY = float.Parse(dicList[i]["positionY"].ToString());
            if (currentX < positionX)
            {
                currentX = positionX;
                yield return new WaitForSeconds(placementDelay);
            }
                
            if (prefabName.Contains("Box"))
            {
                prefabFolder = "Box/";
                parent = GameObject.FindWithTag("Box");
            }
            else if (prefabName.Contains("Solid"))
            {
                prefabFolder = "Solid/";
                parent = GameObject.FindWithTag("Solid");
            }
            string path = "Assets/Prefabs/" + prefabFolder + prefabName + ".prefab";
            prefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)).GameObject();
            
            Instantiate(prefab, new Vector3(positionX, positionY, 0), Quaternion.identity, parent.transform);
        }

        isEnd = true;
    }

    private void Start()
    {
        List<Vector3> positions = new List<Vector3>();
        List<String> prefaps = new List<string>();
        dicList.Clear();

        dicList = CSVReader.Read(fileName);
        
        StartCoroutine(LoadCSVMap(dicList.Count));
    }

    private void Update()
    {
        if (isEnd)
        {
            Destroy(transform.gameObject);
        }
    }
}
