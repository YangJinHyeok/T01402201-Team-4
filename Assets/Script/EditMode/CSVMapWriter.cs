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

public class CSVMapWriter : MonoBehaviour
{
    [SerializeField] private GameObject[] Prefabs;
    private string fileName = "UserMap.csv";
    private string prefabName;
    private int indexToSpawn;
    private float positionX;
    private float positionY;
    
    private GameObject player;
    private EditorUIController editorUIController;
    private List<string[]> data = new List<string[]>();
    private string[] tempData;
    private List<GameObject> instances = new List<GameObject>();

    private StringBuilder sb;
    void Awake()
    {
        player = GameObject.Find("Player");
        editorUIController = GameObject.Find("Canvas").GetComponent<EditorUIController>();

        data.Clear();
    }

    private void Start()
    {
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        player.GetComponent<Rigidbody2D>().useFullKinematicContacts = true;
        sb = new StringBuilder();
    }

    private bool inputData = false;
    private void Update()
    {
        if (GameManager.instance.statusGame == 12)
        {
            var input = Input.inputString;
            switch (input)
            {
                case "q":
                    indexToSpawn = 0;
                    prefabName = "Box1";
                    inputData = true;
                    break;
                case "w":
                    indexToSpawn = 1;
                    prefabName = "Box2";
                    inputData = true;
                    break;
                case "e":
                    indexToSpawn = 2;
                    prefabName = "Box3";
                    inputData = true;
                    break;
                case "r":
                    indexToSpawn = 3;
                    prefabName = "Box4";
                    inputData = true;
                    break;
                case "t":
                    indexToSpawn = 4;
                    prefabName = "Box5";
                    inputData = true;
                    break;
                case "z":
                    indexToSpawn = 5;
                    prefabName = "Solid1";
                    inputData = true;
                    break;
                case "x":
                    indexToSpawn = 6;
                    prefabName = "Solid2";
                    inputData = true;
                    break;
                case "c":
                    indexToSpawn = 7;
                    prefabName = "Solid3";
                    inputData = true;
                    break;
                case "v":
                    indexToSpawn = 8;
                    prefabName = "Solid4";
                    inputData = true;
                    break;
                case "b":
                    indexToSpawn = 9;
                    prefabName = "Solid5";
                    inputData = true;
                    break;
                case "/":
                    if (instances.Count > 0)
                    {
                        Destroy(instances[^1]);
                        instances.RemoveAt(instances.Count - 1);
                        data.Remove(tempData);
                    }
                    break;
                case "p":
                    writeOnCSV();
                    saveCSVFile();
                    transform.GetComponent<CSVSpawnWriter>().enabled = true;
                    editorUIController.NextStep();
                    break;
            }

            if (inputData)
            {
                positionX = Mathf.Round(player.transform.position.x);
                positionY = Mathf.Round(player.transform.position.y);

                if (positionX is <= -28 or >= 27 || positionY is <= -15 or >= 14)
                {
                    GameObject.Find("Canvas").GetComponent<EditorUIController>().messagingError();
                    inputData = false;
                }
                else
                {
                    Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(positionX, positionY), 0.1f);
                    if (colliders.Length < 2)
                    {
                        instances.Add(Instantiate(Prefabs[indexToSpawn],
                            new Vector3(positionX, positionY, 0), Quaternion.identity));

                        tempData = new string[4];
                        tempData[0] = prefabName;
                        tempData[1] = positionX.ToString();
                        tempData[2] = positionY.ToString();
                        data.Add(tempData);
                    }

                    inputData = false;
                }
            }
        }
    }

    public void writeOnCSV()
    {
        tempData = new string[3];
        tempData[0] = "prefapName";
        tempData[1] = "positionX";
        tempData[2] = "positionY";
        List<String[]> sortedData = data.OrderBy(tem => tem[1]).ToList();
        string[][] output = new string[sortedData.Count+1][];

        output[0] = tempData;
        for (int i = 1; i < output.Length; i++)
        {
            output[i] = sortedData[i-1];
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