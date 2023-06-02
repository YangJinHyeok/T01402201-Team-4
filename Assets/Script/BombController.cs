using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class BombController : MonoBehaviour
{


    [Header("Bomb")]
    public GameObject bomb;
    public KeyCode inputKey = KeyCode.Space;
    public Box boxPrefab;


    private void OnEnable()
    {
        Character.Instance.remaining = Character.Instance.count;
        
    }

    private void Update()
    {
        
        if (Character.Instance.remaining > 0 && Input.GetKeyDown(inputKey))
        {
            Vector2 position = GameObject.Find("Player").transform.position;
            position.x = Mathf.Round(position.x);
            position.y = Mathf.Round(position.y);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.1f);
            bool isBombPresent = false;
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Bomb"))
                {
                    isBombPresent = true;
                    break;
                }
            }

            if (!isBombPresent)
            {
                string path = "Assets/Prefabs/Bomb/" + "Bomb" + ".prefab";
                GameObject prefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)).GameObject();
                bomb = Instantiate(prefab, position, Quaternion.identity);
            }
            
        }
    }
     

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false; 
        }
    }








}
