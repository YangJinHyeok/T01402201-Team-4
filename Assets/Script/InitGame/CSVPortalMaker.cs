using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CSVPortalMaker : MonoBehaviour
{
    [SerializeField] private float placementDelay = 1.0f; 
    [SerializeField] private GameObject GameController;
    [SerializeField] private string fileName;
    
    private string prefabName;
    private float positionX;
    private float positionY;
    
    private GameObject prefab;
    private GameObject portalParent;
    private Coroutine routine;

    private GameEffects gameEffects;
    List<Dictionary<string, object>> dicList = new List<Dictionary<string, object>>();
    
    private IEnumerator LoadCSVMap(int length)
    {
        GameObject portalInstance;
        for (int i = 0; i < length; i++)
        {
            prefabName = dicList[i]["prefapName"].ToString();
            positionX = float.Parse(dicList[i]["positionX"].ToString());
            positionY = float.Parse(dicList[i]["positionY"].ToString());
            
            if (prefabName.Equals("Solid5"))
            {
                portalInstance = Instantiate(prefab, new Vector3(positionX, positionY, 0),
                    Quaternion.identity, portalParent.transform);
                gameEffects.portals.Add(portalInstance);
            }

        }
        
        yield return new WaitForSeconds(placementDelay);
        gameEffects.makePortal();
        GameManager.instance.statusGame = 6;
    }

    private void Start()
    {
        List<Vector3> positions = new List<Vector3>();
        List<String> prefaps = new List<string>();
        dicList.Clear();

        dicList = CSVReader.Read(fileName);
        if (GameManager.instance.statusGame >= 12)
        {
            Destroy(transform.gameObject);
        }

        gameEffects = GameController.GetComponent<GameEffects>();
        portalParent = GameObject.Find("Portal");
        prefab = AssetDatabase.
            LoadAssetAtPath("Assets/Prefabs/Solid/PortalParticle.prefab",typeof(GameObject)).GameObject();
    }

    private void Update()
    {
        if (GameManager.instance.statusGame == 5 && routine.IsUnityNull())
        {
            routine = StartCoroutine(LoadCSVMap(dicList.Count));
        }
    }
}
