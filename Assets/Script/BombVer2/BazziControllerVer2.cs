using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazziControllerVer2 : MonoBehaviour
{
    // 플레이어가 죽었나 살았나 = 사망 상태
    private bool isDead = false;
    // 사용할 리지드바디 컴포넌트
    private Rigidbody2D playerRigidbody;
    // 사용할 오디오 소스 컴포넌트
    private AudioSource playerAudio;
    // 사용할 애니메이터 컴포넌트
    private Animator animator;
    // 배찌 최대 스피드
    public float maxSpeedBazzi = 5f;
    // 배찌 기본 스피드
    public float speedBazzi = 2f;
    // 배찌 추가 스피드
    public float minSpeedBazzi = 2f;
    // 물풍선 상태 스피드
    public float hurtSpeed = 0.5f;
    // 캐릭터 최대 생명력
    public int maxLife = 3;
    // 캐릭터 초기 생명력
    public int life = 3;
    // 폭탄 스키립트 연결
    public GameObject bomb;

    private bool inBubble = false;
    void Start()
    {
        // 각 전역변수의 초기화
        // 게임 오브젝트로부터 사용할 컴포넌트들을 가져와 변수에 할당
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAudio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        //실행시 플레이어가 리지드바디 2D를 찾아서 가져온다
        playerRigidbody = GetComponent<Rigidbody2D>();
        // 캐릭터 라이프를 최대로 설정
        life = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        // 캐릭터 생명력이 0인지 확인
        //(추후 물풍선 터지는걸로 수정예정)                    ///////////////////////////////////////////////////////////////**********
        if (life == 0)
        {
            if (!isDead)
            {

            }
        }
        // 이동 구현
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * speedBazzi * Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * speedBazzi * Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * speedBazzi * Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * speedBazzi * Time.deltaTime);
        }

        animator.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));

        //클릭시 폭탄 소환

        if (Input.GetMouseButtonDown(0) && !inBubble)
        {
            bomb.GetComponent<BombSpawnerVer2>().Bombspawn();
        }

    }
    void BombSpawn(int newbomb)
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bubble")
        {
            inBubble = true;
        }


        //아이템 부분
        if (collision.tag == "Speed")
        {
            if (speedBazzi < maxSpeedBazzi)
            {
                speedBazzi++;
                minSpeedBazzi++;
            }
        }
        if (collision.tag == "Count")
        {
            bomb.GetComponent<BombSpawnerVer2>().Bombcount();
        }
        if (collision.tag == "Power")
        {
            bomb.GetComponent<BombSpawnerVer2>().BombPower();
        }
        if (collision.tag == "Life")
        {
            if (life < maxLife)
            {
                life++;
            }
        }
        //몬스터 및 물줄기 충돌부분
        if (collision.tag == "Enemy")
        {
            Die();
        }
        if (collision.tag == "Water")
        {
            Die();
        }

    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Bubble")
        {
            inBubble = false;
        }
    }
    void Die()
    {
        speedBazzi = 0.5f;

    }

}