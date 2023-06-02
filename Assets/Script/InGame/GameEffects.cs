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

    public void powerUp(GameObject item)
    {
        switch (item.tag)
        {
            case "ItemCount" :
                //player
                Debug.Log("Item Count");
                break;
            case "ItemPower" :
                //player
                Debug.Log("Item Power");
                break;
            case "ItemSpeed" :
                //player
                Debug.Log("Item Speed");
                break;
            case "ItemSuperPower" :
                //player
                Debug.Log("Item Super Power");
                break;
            case "Lucci" :
                Debug.Log("Lucci");
                //player
                break;
        }
        Destroy(item);
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
        Vector3 outPosition = outer.transform.position;
        Vector3[] outerPoints = new []{Vector3.up, Vector3.down, Vector3.left, Vector3.right};
        Vector3 direction;

        List<int> possible = new List<int>();

        for (int i = 0; i < 4; i++)
        {
            direction = new Vector3();
            direction = outPosition + outerPoints[i]; 
            Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(direction.x, direction.y), 0.1f);
            
            if ((direction.x is > -28 and < 27) && (direction.y is > -15 and < 14) && colliders.Length < 1)
            {
                possible.Add(i);
            }
        }
        
        if (possible.Count > 0)
        {
            direction = outerPoints[possible[Random.Range(0, possible.Count)]];
            outPosition += direction;
        }
        else
        {
            outPosition = enter.transform.position;
        }
        
        player.GetComponent<Rigidbody2D>().Sleep();
        for (int i = 0; i < 10; i++)
        {
            player.transform.localScale = player.transform.localScale - new Vector3(0.05f, 0.05f, 0.05f);
            yield return new WaitForSeconds(0.04f);
        }

        Instantiate(portalUse, outPosition, Quaternion.identity);
        player.transform.position = outPosition;
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
