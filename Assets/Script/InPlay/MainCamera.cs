using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Transform character;
    //[SerializeField] private GameObject character; 
    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("Player").transform;
    }

    // Update is called once per frame


    public Vector3 offset;
    void Update()
    {
        if (GameManager.instance.statusGame is >= 10 and < 20)
        {
            Vector3 position = character.position;
            transform.position = new Vector3(position.x + offset.x, position.y + offset.y, offset.z);
        }
    }
}
