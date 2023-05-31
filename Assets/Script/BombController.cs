using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class BombController : MonoBehaviour
{
    [Header("Bomb")]
    public GameObject bombPrefab;
    public KeyCode inputKey = KeyCode.Space;
    public float bombFuseTime = 3f;
    public int bombAmount = 1;
    private int bombsRemaining;

    [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;

    public Box boxPrefab;

    

    private void OnEnable()
    {
        bombsRemaining = bombAmount;
    }

    private void Update()
    {
        if (bombsRemaining > 0 && Input.GetKeyDown(inputKey))
        {
            StartCoroutine(PlaceBomb());
        }
    }
    private IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;
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
            GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
            bombsRemaining--;

            yield return new WaitForSeconds(bombFuseTime);

            position = bomb.transform.position;
            position.x = Mathf.Round(position.x);
            position.y = Mathf.Round(position.y);

            Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
            explosion.SetActiveRenderer(explosion.start);
            explosion.DestroyAfter(explosionDuration);

            Explode(position, Vector2.up, explosionRadius);
            Explode(position, Vector2.down, explosionRadius);
            Explode(position, Vector2.left, explosionRadius);
            Explode(position, Vector2.right, explosionRadius);

            Destroy(bomb);
            bombsRemaining++;
        }
    }

    private IEnumerator ShowDustAndDestroy(Collider2D collider)
    {
        float dustDuration = 1f;
        Vector3 dustPosition = collider.transform.position;

        Destroy(collider.gameObject);
        GameObject dustObject = new GameObject("Dust");
        SpriteRenderer dustRenderer = dustObject.AddComponent<SpriteRenderer>();
        string spritePath = "dust";
        Sprite dustSprite = Resources.Load<Sprite>(spritePath);
        dustRenderer.sprite = dustSprite;
        dustObject.transform.position = dustPosition;

        yield return new WaitForSeconds(dustDuration);

        Destroy(dustObject);
    }


    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0)
        {
            return;
        }
        position += direction;

        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask))
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(position, Vector2.one / 2f, 0f, explosionLayerMask);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Box"))
                {
                    StartCoroutine(ShowDustAndDestroy(collider));
                }
                else if (collider.CompareTag("Bomb"))
                {
                    bombFuseTime = 0f;
                    Invoke("Explode", bombFuseTime);
                }
            }

            return;
        }

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionDuration);

        
        Explode(position, direction, length - 1);
    }




    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false; 
        }
    }

    


}
