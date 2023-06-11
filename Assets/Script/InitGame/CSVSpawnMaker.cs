using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CSVSpawnMaker : MonoBehaviour
{
    [SerializeField] private string fileName;
    [SerializeField] private GameObject mob1Prefab;
    [SerializeField] private GameObject mob2Prefab;
    private string prefabName;
    private float positionX;
    private float positionY;
    private Camera main;
    private GameObject parent;
    private GameEffects gameEffects;
    List<Dictionary<string, object>> dicList = new List<Dictionary<string, object>>();
    
    
    private IEnumerator LoadCSVMap(int length)
    {
        while (GameManager.instance.statusGame != 4)
        {
            yield return null;
        }
        List<Dictionary<string, object>> mob1 = new List<Dictionary<string, object>>();
        List<Dictionary<string, object>> mob2 = new List<Dictionary<string, object>>();

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
        }
        parent = GameObject.FindWithTag("Mob");
        for (int i = 0; i < mob1.Count; i++)
        {
            prefabName = mob1[i]["prefapName"].ToString();
            positionX = float.Parse(mob1[i]["positionX"].ToString());
            positionY = float.Parse(mob1[i]["positionY"].ToString());
            
            Instantiate(mob1Prefab, new Vector3(positionX, positionY, 0), Quaternion.identity, parent.transform);
            
            yield return new WaitForSeconds(0.06f);
        }
        for (int i = 0; i < mob2.Count; i++)
        {
            prefabName = mob2[i]["prefapName"].ToString();
            positionX = float.Parse(mob2[i]["positionX"].ToString());
            positionY = float.Parse(mob2[i]["positionY"].ToString());

            Instantiate(mob2Prefab, new Vector3(positionX, positionY, 0), Quaternion.identity, parent.transform);
            
            yield return new WaitForSeconds(0.06f);
        }
        gameEffects.setMobCount(mob1.Count + mob2.Count);
        GameManager.instance.statusGame = 5;

    }

    private void Start()
    {
        List<Vector3> positions = new List<Vector3>();
        List<String> prefaps = new List<string>();
        main = Camera.main;
        gameEffects = GameObject.Find("GameController").GetComponent<GameEffects>();
        dicList.Clear();

        dicList = CSVReader.Read(fileName);
        if (GameManager.instance.statusGame == 12)
        {
            Destroy(transform.gameObject);
        }
            
        StartCoroutine(LoadCSVMap(dicList.Count));
    }

    private void Update()
    {
        if (GameManager.instance.statusGame == 10)
        {
            Destroy(transform.gameObject);
        }
    }
}
