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
    public float explosionDuration = 5.0f;
    private int explosionRadius;
    private GameEffects gameEffects;
    public float time = 3.0f;
    private BombController bombController;

    private void Awake()
    {
        bombController = GameObject.Find("Player").GetComponent<BombController>();
        gameEffects = GameObject.Find("GameController").GetComponent<GameEffects>();
        explosionRadius = bombController.getPlayerPower();
    }
    private void Start()
    {
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
        
        bombController.remainUp();
        Destroy(this.gameObject);
    }

    public void PlaceBomb()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
                
        StartCoroutine(ExplodeAfterDelay(position));
         
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
                    gameEffects.GetComponent<GameEffects>().touchBoxAndSolid(collider.gameObject);
                }
                if (collider.CompareTag("Bomb"))
                {
                    Bomb bomb = collider.GetComponent<Bomb>();
                    bomb.time = -1;
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
