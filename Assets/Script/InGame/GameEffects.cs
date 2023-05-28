using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameEffects : MonoBehaviour
{
    [SerializeField] private GameObject UIController;
    
    public List<GameObject> portals = new List<GameObject>();
    
    private List<int> path = new List<int>();

    public static float portalCooltime = 3.0f;

    private GameUIController gameUIController;

    private void Awake()
    {
        portals.Clear();
        path.Clear();
        gameUIController = UIController.GetComponent<GameUIController>();
        portalCooltime = 3.0f;
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
        GameObject player = GameObject.Find("Player");
        int index = portals.IndexOf(enter);
        index = path[index];
        GameObject outer = portals[index];
        player.transform.position = outer.transform.position;

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
    
}
