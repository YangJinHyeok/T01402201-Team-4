using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHandler : MonoBehaviour
{

    public GameObject prefab_child;
    public GameObject prefab_tail;

    // Start is called before the first frame update
    void Start()
    {
        prefab_child = Resources.Load("Prefabs/BombChild") as GameObject;
        prefab_tail = Resources.Load("Prefabs/BombTail") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeBombChild()
    {

    }
    public void MakeBombTail()
    {

    }
}
