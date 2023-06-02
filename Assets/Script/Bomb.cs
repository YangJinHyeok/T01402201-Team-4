using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

public class Bomb : MonoBehaviour
{

    [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 3;

    public float time = 3.0f;

    private void Start()
    {
        PlaceBomb();
    }

    private void Update()
    {
        
    }

    private IEnumerator ExplodeAfterDelay(GameObject bomb, Vector2 position)
    {
        while (time > 0)
        {
            Debug.Log(time);
            yield return null;
        }

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);

        Destroy(bomb);
        GameObject.Find("Player").GetComponent<BombController>().bombsRemaining++;
    }

    public void PlaceBomb()
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
            
            GameObject bombInstance = Instantiate(GameObject.Find("Player").GetComponent<BombController>().bombPrefab, position, Quaternion.identity);
            Bomb bomb = bombInstance.GetComponent<Bomb>();
            Debug.Log("Active? " + gameObject.activeInHierarchy);
            GameObject.Find("Player").GetComponent<BombController>().bombsRemaining--;

            StartCoroutine(ExplodeAfterDelay(bombInstance, position));
            
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


}
