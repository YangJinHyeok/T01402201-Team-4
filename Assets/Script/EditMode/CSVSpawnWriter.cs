using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CSVSpawnWriter : MonoBehaviour
{
    private string fileName = "UserSpawn.csv";
    private string prefabName;
    private string prefabFolder;
    private float positionX;
    private float positionY;
    private GameObject player;
    private EditorUIController editorUIController;
    private List<string[]> data = new List<string[]>();
    private string[] tempData;
    private GameObject latestInstance;

    private StringBuilder sb;
    void Awake()
    {
        player = GameObject.Find("Player");
        editorUIController = GameObject.Find("Canvas").GetComponent<EditorUIController>();

        data.Clear();
        
        tempData = new string[3];
        tempData[0] = "prefapName";
        tempData[1] = "positionX";
        tempData[2] = "positionY";
        data.Add(tempData);
    }

    private void OnEnable()
    {
        sb = new StringBuilder();
    }

    private bool inputData = false;
    private void Update()
    {
    
        var input = Input.inputString;
        switch (input)
        {
            case "a":
                prefabFolder = "Mob/";
                prefabName = "Mob1";
                inputData = true;
                break;
            case "s":
                prefabFolder = "Mob/";
                prefabName = "Mob2";
                inputData = true;
                break;
            case "z":
                Destroy(latestInstance);
                data.Remove(tempData);
                break;
            case "p":
                Debug.Log("now Saving");
                writeOnCSV();
                editorUIController.NextStep();
                saveCSVFile();
                break;
        }

        if (inputData)
        {
            positionX = Mathf.Round(player.transform.position.x);
            positionY = Mathf.Round(player.transform.position.y);
            
            Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(positionX,positionY),0.1f);
            if (colliders.Length < 2)
            {
                GameObject prefab = new GameObject();
                string path = "Assets/Prefabs/" + prefabFolder + prefabName + ".prefab";
                prefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)).GameObject();
                Debug.Log("current prefab : " + prefab.name);
                latestInstance = Instantiate(prefab,
                    new Vector3(positionX, positionY, 0), Quaternion.identity);

                tempData = new string[4];
                tempData[0] = prefabName;
                tempData[1] = positionX.ToString();
                tempData[2] = positionY.ToString();
                data.Add(tempData);
            }

            inputData = false;
        }
    
    }

    public void writeOnCSV()
    {
        List<String[]> sortedData = data.OrderByDescending(tem => tem[0]).ToList();
        string[][] output = new string[sortedData.Count][];
    
        for (int i = 0; i < output.Length; i++)
        {
            output[i] = sortedData[i];
        }
        int length = output.GetLength(0);
        string delimiter = ",";
        for (int i = 0; i < length; i++)
        {
            sb.AppendLine(string.Join(delimiter, output[i]));
        }
    }
    
    public void saveCSVFile()
    {
        string filepath = SystemPath.GetPath();

        if (!Directory.Exists(filepath))
        {
            Directory.CreateDirectory(filepath);
        }

        StreamWriter outStream = System.IO.File.CreateText(filepath + fileName);
        outStream.Write(sb);
        outStream.Close();
        Debug.Log("Save End");
    }
}