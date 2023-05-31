using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameEffects : MonoBehaviour
{
    [SerializeField] private GameObject UIController;
    
    public List<GameObject> portals = new List<GameObject>();
    
    private List<int> path = new List<int>();

    public static float portalCooltime = 3.0f;

    private GameUIController gameUIController;
    
    private GameObject portalUse;

    private GameObject player;
    

    private void Awake()
    {
        portals.Clear();
        path.Clear();
        gameUIController = UIController.GetComponent<GameUIController>();
        portalCooltime = 3.0f;
        portalUse = AssetDatabase
            .LoadAssetAtPath("Assets/Prefabs/Solid/PortalUse.prefab", typeof(GameObject))
            .GameObject();
    }

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (GameEffects.portalCooltime < 3.0f)
        {
            GameEffects.portalCooltime += Time.deltaTime;
        }
        
    }

    public void destroyBox(GameObject target)
    {
        Vector3 targetPosition = target.transform.position;
        Destroy(target);
        gameUIController.updateScoreWIthValue(30);
    }

    public void touchSolid(GameObject target)
    {
        
    }

    public void itemSpawn(Vector3 position)
    {
        
    }

    public void powerUp()
    {
        
    }

    public void teleport(GameObject enter)
    {
        GameEffects.portalCooltime = 0.0f;
        int index = portals.IndexOf(enter);
        index = path[index];
        GameObject outer = portals[index];
        StartCoroutine(teleportVisualEffect(enter, outer));
    }

    IEnumerator teleportVisualEffect(GameObject enter, GameObject outer)
    {
        Instantiate(portalUse, enter.transform.position, Quaternion.identity);
        player.GetComponent<Rigidbody2D>().Sleep();
        for (int i = 0; i < 10; i++)
        {
            player.transform.localScale = player.transform.localScale - new Vector3(0.05f, 0.05f, 0.05f);
            yield return new WaitForSeconds(0.04f);
        }

        Instantiate(portalUse, outer.transform.position, Quaternion.identity);
        player.transform.position = outer.transform.position;
        for (int i = 0; i < 10; i++)
        {
            player.transform.localScale = player.transform.localScale + new Vector3(0.05f, 0.05f, 0.05f);
            yield return new WaitForSeconds(0.04f);
        }
        player.GetComponent<Rigidbody2D>().WakeUp();
    }

    public void makePortal()
    {
        int maxIndex = portals.Count;

        while (path.Count != portals.Count)
        {
            int input = Random.Range(0, maxIndex);
            if (!path.Contains(input))
            {
                path.Add(input);
            }
        }
    }

    public void endGame(bool isWin)
    {
        gameUIController.endSequence(isWin);
    }
    
}
