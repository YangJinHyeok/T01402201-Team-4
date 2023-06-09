using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private Vector2 lastMovement = new Vector2(0, 0);
    private GameEffects gameEffects;
    public AudioSource MySfx;
    public AudioClip MySfxClip;
    public AudioClip MySfxClip2;
    public AudioClip MySfxClip3;

    private float playerSpeed;
    private float playerSpeedMax;
    public bool isTrapTriggered = true;
    /*private float sadf
*/

    // Start is called before the first frame update
    void Start()
    {
        Character.Instance.getFromCSV();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        playerSpeed = Character.Instance.getSpeed();
        playerSpeedMax = Character.Instance.getSpeedMax();
        gameEffects = GameObject.Find("GameController").GetComponent<GameEffects>();

    }

    // Update is called once per frame
    private void Update()
    {
        PlayerMove();
        
    }
    

    private enum MovementState {down, right, up, left, downidle, rightidle, upidle, leftidle};
    private void PlayerMove()
    {
        if (GameManager.instance.statusGame is 10 or 12 or 13)
        {
            anim.enabled = true;
            MovementState state = MovementState.downidle;

            if (Input.GetKey(KeyCode.RightArrow))
            {
                lastMovement = new Vector2(1, 0);
                state = MovementState.right;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                lastMovement = new Vector2(-1, 0);
                state = MovementState.left;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                lastMovement = new Vector2(0, 1);
                state = MovementState.up;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                lastMovement = new Vector2(0, -1);
                state = MovementState.down;
            }
            else
            {

                if (lastMovement.y > 0)
                {
                    state = MovementState.upidle;
                }
                else if (lastMovement.y < 0)
                {
                    state = MovementState.downidle;
                }
                else if (lastMovement.x > 0)
                {
                    state = MovementState.rightidle;
                }
                else if (lastMovement.x < 0)
                {
                    state = MovementState.leftidle;
                }
                lastMovement = Vector2.zero;

            }
            rb.MovePosition(rb.position + lastMovement * (playerSpeed * Time.fixedDeltaTime));

            anim.SetInteger("state", (int)state);
        }
        else
        {
            anim.enabled = false;
            return;
        }


    }

    public void speedUp()
    {
        if (playerSpeed < playerSpeedMax)
        {
            playerSpeed++;
        }
    }

    private IEnumerator playerDeath(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        anim.SetTrigger("death");
        playerSpeed = 0f;

        delayTime = 2f;
        yield return new WaitForSeconds(delayTime);

        gameEffects.endGame(false);
        MySfx.PlayOneShot(MySfxClip2);
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }

    private IEnumerator monsterTouch(float delayTime)
    {
        anim.SetTrigger("monsterTouch");
        playerSpeed = 0f;
        GetComponent<Collider2D>().enabled = false;

        delayTime = 2f;
        yield return new WaitForSeconds(delayTime);

        gameEffects.endGame(false);
        MySfx.PlayOneShot(MySfxClip3);
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }

    public void PlayerDie()
    {
        float delayTime = 4.0f;

        anim.SetTrigger("trap");
        isTrapTriggered = false;

        playerSpeed = 0.5f;
        StartCoroutine(playerDeath(delayTime));
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);

        if (currentState.IsName("TrapBazzi"))
        {
            return;
        }
        if (collision.gameObject.CompareTag("Explosion"))
        {
            PlayerDie();
        }
        if (collision.gameObject.CompareTag("ItemCount"))
        {
            itemEat();
        }
        if (collision.gameObject.CompareTag("ItemSuperPower"))
        {
            itemEat();
        }
        if (collision.gameObject.CompareTag("ItemPower"))
        {
            itemEat();
        }
        if (collision.gameObject.CompareTag("ItemSpeed"))
        {
            itemEat();
        }
        if (collision.gameObject.CompareTag("Lucci"))
        {
            itemEat();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mob"))
        {
            StartCoroutine(monsterTouch(2.0f));
        }
    }

    public void itemEat()
    {
        MySfx.PlayOneShot(MySfxClip);
    }

}