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
    private int explosionRadius;

    public float time = 3.0f;

    private void Start()
    {
        explosionRadius = Character.Instance.power;
        PlaceBomb();
    }

    private void Update()
    {
        if(time > 0)
        {
            time -= Time.deltaTime;
            
        }
    }

    private IEnumerator ExplodeAfterDelay(Vector2 position)
    {
        while (time > 0)
        {
            yield return null;
        }

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);


        Destroy(this.gameObject);
        GameObject.Find("Player").GetComponent<BombController>().bombsRemaining++;
    }

    public void PlaceBomb()
    {
        Vector2 position = GameObject.Find("Player").transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        GameObject.Find("Player").GetComponent<BombController>().bombsRemaining--;

        StartCoroutine(ExplodeAfterDelay(position));
         
    }

    private IEnumerator ShowDustAndDestroy(Collider2D collider)
    {
        float dustDuration = 1f;
        /*Vector3 dustPosition = collider.transform.position;*/

        Destroy(collider.gameObject);
        /*GameObject dustObject = new GameObject("Dust");
        SpriteRenderer dustRenderer = dustObject.AddComponent<SpriteRenderer>();
        string spritePath = "dust";
        Sprite dustSprite = Resources.Load<Sprite>(spritePath);
        dustRenderer.sprite = dustSprite;
        dustObject.transform.position = dustPosition;*/

        yield return new WaitForSeconds(dustDuration);

        /*Destroy(dustObject);*/
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
                if (collider.CompareTag("Bomb"))
                {
                    /*Bomb bomb = collider.GetComponent<Bomb>();
                    bomb.time = -1;*/
                    Debug.Log("hi");
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
