using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class CSVMapMaker : MonoBehaviour
{
    [SerializeField] private string fileName;
    [SerializeField] private GameObject[] boxPrefabs;
    [SerializeField] private GameObject[] solidPrefabs;
    [SerializeField] private GameObject[] itemPrefabs;
    private string prefabName;
    private float positionX;
    private float positionY;

    private GameObject prefab;
    private GameObject parent;
    private GameObject boxParent;
    private GameObject solidParent;
    private Coroutine routine;
    List<Dictionary<string, object>> dicList = new List<Dictionary<string, object>>();
    int[] probability = new[] { 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 4, 5, 5, 5, 5, 5, 5 };

    private IEnumerator LoadCSVMap(int length)
    {
        while (GameManager.instance.statusGame != 1)
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
                yield return new WaitForSeconds(0.04f);
            }
            
            if (prefabName.Contains("Box"))
            {
                parent = boxParent;
                inputItem(Instantiate(boxPrefabs[int.Parse(prefabName.Remove(0, 3))-1],
                    new Vector3(positionX, positionY, 0), Quaternion.identity, parent.transform));
            }
            else if (prefabName.Contains("Solid"))
            {
                parent = solidParent;
                inputItem(Instantiate(solidPrefabs[int.Parse(prefabName.Remove(0, 5))-1],
                    new Vector3(positionX, positionY, 0), Quaternion.identity, parent.transform));
            }
        }
        if (GameManager.instance.statusGame == 2)
        {
            GameManager.instance.statusGame = 3;
        }
        if (GameManager.instance.statusGame == 1)
        {
            GameManager.instance.statusGame = 2;
        }
        
    }

    private void Start()
    {
        dicList.Clear();
        dicList = CSVReader.Read(fileName);
        
        boxParent = GameObject.FindWithTag("Box");
        solidParent = GameObject.FindWithTag("Solid");
        if (GameManager.instance.statusGame == 12)
        {
            Destroy(transform.gameObject);
        }
    }

    private void Update()
    {
        if (GameManager.instance.statusGame == 1 && routine.IsUnityNull())
        {
            routine = StartCoroutine(LoadCSVMap(dicList.Count));
        }
    }

    private void inputItem(GameObject parent)
    {
        int index = probability[Random.Range(0, probability.Length)];
        if (parent.gameObject.CompareTag("Box") && index < 5)
        {
            Instantiate(itemPrefabs[index], Vector3.zero, Quaternion.identity, parent.transform).SetActive(false);
        }
    }
}
