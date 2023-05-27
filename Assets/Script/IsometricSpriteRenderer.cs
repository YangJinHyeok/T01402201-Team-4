using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricSpriteRenderer : MonoBehaviour
{
    private Renderer render;

    // Update is called once per frame
    private void Start()
    {
        render = GetComponent<Renderer>();
        render.sortingOrder = (int)(transform.position.y * -100);
    }

    void Update()
    {
        render.sortingOrder = (int)(transform.position.y * -100);
    }
}
