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
    public static float portalCooltime = 3.0f;
    public List<GameObject> portals = new List<GameObject>();
    
    private List<int> path = new List<int>();
    private GameUIController gameUIController;
    private GameObject portalUse;
    private GameObject player;
    private GameObject dust;
    private int mobCount;

    private MobMovement mobMovement;

    private void Awake()
    {
        portals.Clear();
        path.Clear();
        gameUIController = UIController.GetComponent<GameUIController>();
        portalCooltime = 3.0f;
        portalUse = AssetDatabase
            .LoadAssetAtPath("Assets/Prefabs/Solid/PortalUse.prefab", typeof(GameObject))
            .GameObject();
        dust = AssetDatabase
            .LoadAssetAtPath("Assets/Prefabs/dust.prefab", typeof(GameObject))
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

    public void touchBoxAndSolid(GameObject target)
    {
        Vector3 targetPosition = target.transform.position;
        Instantiate(dust, targetPosition, Quaternion.identity);
        target.GetComponent<BoxAndSolid>().effectByBomb();
        gameUIController.updateScoreWIthValue(30);
    }

    public void touchItems(GameObject item)
    {
        Destroy(item);
    }

    public void touchMob(GameObject target)
    {
        gameUIController.updateScoreWIthValue(100);
        lowerMobCount();
    }

    public void powerUp(GameObject item)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        BombController bombController = player.GetComponent<BombController>();
        switch (item.tag)
        {
            case "ItemCount":
                bombController.countUp();
                break;

            case "ItemSpeed":
                playerMovement.speedUp();
                break;

            case "ItemPower":
                bombController.powerUp();
                break;

            case "ItemSuperPower":
                bombController.maxPowerUp();
                break;

            case "Lucci":
                Debug.Log("Lucci");
                gameUIController.updateCoin(Random.Range(100, 300));
                break;
        }

        /*
         * <<sound method will place in this Line>>
         */
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

    public void setMobCount(int value)
    {
        mobCount = value;
    }

    public void lowerMobCount()
    {
        if (mobCount == 1)
        {
            endGame(true);
        }
        else
        {
            mobCount--;
        }
    }
    
}
