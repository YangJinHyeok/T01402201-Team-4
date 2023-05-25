using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricSpriteRenderer : MonoBehaviour
{
    private Renderer renderer;

    // Update is called once per frame
    private void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.sortingOrder = (int)(transform.position.y * -100);
    }

    void Update()
    {
        renderer.sortingOrder = (int)(transform.position.y * -100);
    }
}
