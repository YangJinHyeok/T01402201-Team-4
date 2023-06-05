using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : MonoBehaviour
{
    private Transform monsterTransform;
    private Animator animator;
    public float moveInterval = 3f; // �̵� ���� ����
    private float timer = 0f;
    public float moveSpeed = 1f; // �̵� �ӵ� ����
    public float findRange = 5f;

    private Vector2 targetPosition;
    private Vector2 trackPosition;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        monsterTransform = transform;
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= moveInterval)
        {
            trackPosition = findCharacter();
            if (trackPosition != Vector2.zero)
            {
                
                moveTrack(trackPosition);
            }
            else
            {
                
                moveRandom();
            }
            timer = 0f;
        }

        if (isMoving)
        {
            // �ε巴�� �̵�
            float step = moveSpeed * Time.fixedDeltaTime;
            monsterTransform.position = Vector2.MoveTowards(monsterTransform.position, targetPosition, step);

            // �̵� �Ϸ� ���� Ȯ��
            if ((Vector2)monsterTransform.position == targetPosition)
            {
                isMoving = false;
                animator.SetBool("IsMove", false);
            }
        }
    }

    private void moveRandom()
    {
        Vector2 currentPosition = monsterTransform.position;
        Vector2[] moveDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        List<Vector2> availableDirections = new List<Vector2>();

        // ��ü �ֺ��� �ִ��� ���θ� Ȯ���ϰ� ��ü�� ���� ������ ã��
        foreach (Vector2 direction in moveDirections)
        {
            Vector2 newPosition = currentPosition + direction;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, 0.1f);

            if (colliders.Length == 0)
            {
                availableDirections.Add(direction);
            }
        }

        // ��ü�� ���� ���� �߿��� �����ϰ� �̵��ϱ�
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

        StartCoroutine(CheckCollisionDuringMove());
    }

    private IEnumerator CheckCollisionDuringMove()
    {
        while (isMoving)
        {
            // ���� �̵� ���� ��ġ���� �浹 ���� Ȯ��
            Collider2D[] colliders = Physics2D.OverlapCircleAll(monsterTransform.position, 0.1f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.CompareTag("Mob") || collider.gameObject.CompareTag("Bomb"))
                {
                    isMoving = false;
                    animator.SetBool("IsMove", false);
                    yield break;
                }
            }
            yield return null;
        }
    }

    private Vector2 findCharacter()
    {
        Vector2 currentPosition = monsterTransform.position;

        Vector2[] directions = new Vector2[] { Vector2.right, Vector2.left, Vector2.up, Vector2.down };

        foreach (Vector2 direction in directions)
        {
            Vector2 targetPosition = currentPosition + direction; // ���� ��ġ�� ���� ���͸� ���� Ÿ�� ��ġ

            RaycastHit2D hit = Physics2D.Raycast(targetPosition, direction, findRange);
            if (hit.collider == null) // ����ĳ��Ʈ ����� null�� ��� ���� �������� ����
                continue;

            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Vector2 playerPosition = hit.collider.transform.position;
                return new Vector2(Mathf.RoundToInt(playerPosition.x), Mathf.RoundToInt(playerPosition.y));
            }
        }

        return Vector2.zero;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ĳ���Ϳ��� �浹�� �����ϸ� �ʿ��� ó���� �����մϴ�.
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Game Over");
        }
    }
}
