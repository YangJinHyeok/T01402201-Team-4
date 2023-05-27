using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CSVSpawnMaker : MonoBehaviour
{
    public string fileName = "Spawn";
    public string prefabName;
    public string prefabFolder;
    public float positionX;
    public float positionY;
    public float placementDelay = 0.1f;
    private Camera main;
    
    private GameObject prefab;
    private GameObject parent;
    private bool isEnd = false;
    List<Dictionary<string, object>> dicList = new List<Dictionary<string, object>>();
    
    
    private IEnumerator LoadCSVMap(int length)
    {
        while (GameLogic.statusGame != 4)
        {
            yield return null;
        }
        List<Dictionary<string, object>> mob1 = new List<Dictionary<string, object>>();
        List<Dictionary<string, object>> mob2 = new List<Dictionary<string, object>>();
        //List<Dictionary<string, object>> player = new List<Dictionary<string, object>>();

        for (int i = 0; i < length; i++)
        {
            prefabName = dicList[i]["prefapName"].ToString();

            if (prefabName.Equals("Mob1"))
            {
                mob1.Add(dicList[i]);
            }
            if (prefabName.Equals("Mob2"))
            {
                mob2.Add(dicList[i]);
            }
            /*if (prefabName.Equals("Mob1"))
            {
                mob1.Add(dicList[i]);
            }*/
        }

        for (int i = 0; i < mob1.Count; i++)
        {
            prefabName = mob1[i]["prefapName"].ToString();
            positionX = float.Parse(mob1[i]["positionX"].ToString());
            positionY = float.Parse(mob1[i]["positionY"].ToString());
            parent = GameObject.FindWithTag("Mob");
            prefabFolder = "Mob/";
            
            string path = "Assets/Prefabs/" + prefabFolder + prefabName + ".prefab";
            prefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)).GameObject();
            
            Instantiate(prefab, new Vector3(positionX, positionY, 0), Quaternion.identity, parent.transform);
            
            yield return new WaitForSeconds(placementDelay);
        }
        for (int i = 0; i < mob2.Count; i++)
        {
            prefabName = mob2[i]["prefapName"].ToString();
            positionX = float.Parse(mob2[i]["positionX"].ToString());
            positionY = float.Parse(mob2[i]["positionY"].ToString());
            parent = GameObject.FindWithTag("Mob");
            prefabFolder = "Mob/";
            
            string path = "Assets/Prefabs/" + prefabFolder + prefabName + ".prefab";
            prefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)).GameObject();
            
            Instantiate(prefab, new Vector3(positionX, positionY, 0), Quaternion.identity, parent.transform);
            
            yield return new WaitForSeconds(placementDelay);
        }
        /*
         * select player spawn position by random
         * main.gameObject.transform.position = new Vector3(positionX, positionY, -10);
         */

        GameLogic.statusGame = 5;

    }

    private void Start()
    {
        List<Vector3> positions = new List<Vector3>();
        List<String> prefaps = new List<string>();
        main = Camera.main;
        dicList.Clear();

        dicList = CSVReader.Read(fileName);
        if (GameLogic.statusGame == 12)
        {
            Destroy(transform.gameObject);
        }
            
        StartCoroutine(LoadCSVMap(dicList.Count));
    }

    private void Update()
    {
        if (GameLogic.statusGame == 10)
        {
            Destroy(transform.gameObject);
        }
    }
}
