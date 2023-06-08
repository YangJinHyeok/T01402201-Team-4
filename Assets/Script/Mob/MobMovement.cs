using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : MonoBehaviour
{
    private Transform monsterTransform;
    private Animator animator;
    [SerializeField]
    private float moveInterval; // �̵� ���� ����
    [SerializeField]
    private float moveSpeed; // �̵� �ӵ� ����
    [SerializeField]
    private float findRange;
    [SerializeField]
    private int mobHealth;
    private float tempInterval;
    private float timer = 0f;

    private Vector2 targetPosition;
    private Vector2 trackPosition;
    private bool isMoving = false;

    private GameEffects gameEffects;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.statusGame > 10)
        {
            Destroy(this);
        }
        
        monsterTransform = transform;
        animator = GetComponent<Animator>();
        animator.SetInteger("Health", mobHealth);
        gameEffects = GameObject.Find("GameController").GetComponent<GameEffects>();
    }

    void OnEnable()
    {
        tempInterval = moveInterval;
        moveInterval = 200f;
        StartCoroutine(WaitForStart());
    }

    IEnumerator WaitForStart()
    {
        yield return new WaitWhile(() => GameManager.instance.statusGame != 10);
        moveInterval = tempInterval;
    }



    private void Update()
    {
        timer += Time.deltaTime;
        trackPosition = findCharacter();
        
        if (trackPosition != Vector2.zero &&
            Mathf.Floor(monsterTransform.position.x) == monsterTransform.position.x &&
            Mathf.Floor(monsterTransform.position.y) == monsterTransform.position.y)
        {
            Debug.Log("Track");
            moveTrack(trackPosition);
        }

        else if (timer >= moveInterval && !isMoving)
        {
            moveRandom();
            timer = 0f;
        }

        if (isMoving)
        {
            targetPosition = new Vector2(Mathf.RoundToInt(targetPosition.x), Mathf.RoundToInt(targetPosition.y));
            float step = moveSpeed * Time.deltaTime;
            monsterTransform.position = Vector2.MoveTowards(monsterTransform.position, targetPosition, step);

            if ((Vector2)monsterTransform.position == targetPosition)
            {
                isMoving = false;
                animator.SetBool("IsMove", false);
            }
        }
    }

    private bool IsPositionBlocked(Vector2 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Mob") || collider.gameObject.CompareTag("Bomb"))
            {
                return true;
            }
        }
        return false;
    }

    private void moveRandom()
    {
        Vector2 currentPosition = monsterTransform.position;
        Vector2[] moveDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        List<Vector2> availableDirections = new List<Vector2>();

        foreach (Vector2 direction in moveDirections)
        {
            Vector2 newPosition = currentPosition + direction;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, 0.1f);

            if (colliders.Length == 0)
            {
                availableDirections.Add(direction);
            }
        }

        if (availableDirections.Count > 0)
        {
            int randomIndex = Random.Range(0, availableDirections.Count);
            Vector2 moveDirection = availableDirections[randomIndex];
            targetPosition = currentPosition + moveDirection;

            isMoving = true;
            animator.SetBool("IsMove", true);
        }
    }

    private void moveTrack(Vector2 trackPosition)
    {
        targetPosition = trackPosition;

        isMoving = true;
        animator.SetBool("IsMove", true);
    }


    private Vector2 findCharacter()
    {
        Vector2 currentPosition = monsterTransform.position;

        Vector2[] directions = new Vector2[] { Vector2.right, Vector2.left, Vector2.up, Vector2.down };

        foreach (Vector2 direction in directions)
        {
            Vector2 shotPosition = currentPosition + direction;
            RaycastHit2D hit = Physics2D.Raycast(shotPosition, direction, findRange);

            if (hit.collider == null)
                continue;

            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Vector2 playerPosition = hit.collider.transform.position;
                if (direction == Vector2.right || direction == Vector2.left)
                {
                    return new Vector2(Mathf.RoundToInt(playerPosition.x), Mathf.RoundToInt(monsterTransform.position.y));
                }
                else if (direction == Vector2.up || direction == Vector2.down)
                {
                    return new Vector2(Mathf.RoundToInt(monsterTransform.position.x), Mathf.RoundToInt(playerPosition.y));
                }
            }
        }

        return Vector2.zero;
    }

    public void mobHurt()
    {
        mobHealth -= 1;
        if (mobHealth == 0)
        {
            mobDeath();
        }
        else
        {
            animator.SetTrigger("Hurt");
        }
    }

    public void mobDeath()
    {
        GetComponent<Collider2D>().enabled = false;
        moveSpeed = 0f;
        animator.SetInteger("Health", mobHealth);
        gameEffects.touchMob(gameObject);
    }

    private void mobDestroy()
    {
        Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Game Over");
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Explosion"))
        {
            mobHurt();
        }
        if (collider.gameObject.CompareTag("Bomb") || collider.gameObject.CompareTag("Mob"))
        {
            targetPosition = new Vector2(Mathf.RoundToInt(monsterTransform.position.x), Mathf.RoundToInt(monsterTransform.position.y));
        }
    }
}
