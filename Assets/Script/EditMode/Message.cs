using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{ 
    void Update()
    {
        transform.position = 
            Vector3.MoveTowards(transform.position, transform.position + Vector3.up, 0.05f);
    }
}
