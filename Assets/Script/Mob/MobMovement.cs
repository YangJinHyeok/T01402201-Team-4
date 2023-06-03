using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : MonoBehaviour
{
    private Transform monsterTransform;
    private Animator animator;
    public float moveInterval = 3f; // 이동 간격 조정
    private float timer = 0f;
    public float moveSpeed = 1f; // 이동 속도 조정
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
                Debug.Log("Tracking!");
                moveTrack(trackPosition);
            }
            else
            {
                Debug.Log("Random!");
                moveRandom();
            }
            timer = 0f;
        }

        if (isMoving)
        {
            // 부드럽게 이동
            float step = moveSpeed * Time.fixedDeltaTime;
            monsterTransform.position = Vector2.MoveTowards(monsterTransform.position, targetPosition, step);

            // 이동 완료 여부 확인
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

        // 물체 주변에 있는지 여부를 확인하고 물체가 없는 방향을 찾기
        foreach (Vector2 direction in moveDirections)
        {
            Vector2 newPosition = currentPosition + direction;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, 0.1f);

            if (colliders.Length == 0)
            {
                availableDirections.Add(direction);
            }
        }

        // 물체가 없는 방향 중에서 랜덤하게 이동하기
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
            // 현재 이동 중인 위치에서 충돌 여부 확인
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
            Vector2 targetPosition = currentPosition + direction; // 현재 위치에 방향 벡터를 더한 타겟 위치

            RaycastHit2D hit = Physics2D.Raycast(targetPosition, direction, findRange);
            if (hit.collider == null) // 레이캐스트 결과가 null인 경우 다음 방향으로 진행
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
        // 캐릭터와의 충돌을 감지하면 필요한 처리를 수행합니다.
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Game Over");
        }
    }
}
