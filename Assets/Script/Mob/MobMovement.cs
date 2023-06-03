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

    private Vector2 targetPosition;
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
            MoveRandom();
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

    private void MoveRandom()
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 캐릭터와의 충돌을 감지하면 필요한 처리를 수행합니다.
        if (collision.gameObject.CompareTag("Player"))
        {
            // 충돌 처리 예시: 게임 오버 등
            Debug.Log("Game Over");
        }
    }
}
