using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private Vector2 lastMovement = new Vector2(0, 0);
    
    private float playerSpeed;
    private float playerSpeedMax;
    private int playerPower;
    private int playerPowerMax;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        playerSpeed = Character.Instance.getSpeed();
        playerPower = Character.Instance.getPower();
        playerSpeedMax = Character.Instance.getSpeedMax();
        playerPowerMax = Character.Instance.getPowerMax();

    }

    // Update is called once per frame
    private void Update()
    {
        PlayerMove();
    }


    private enum MovementState {down, right, up, left, downidle, rightidle, upidle, leftidle };
    private void PlayerMove()
    {

        MovementState state = MovementState.downidle;
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            lastMovement = new Vector2(1,0);
            state = MovementState.right;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            lastMovement = new Vector2(-1,0);
            state = MovementState.left;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            lastMovement = new Vector2(0,1);
            state = MovementState.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            lastMovement = new Vector2(0,-1);
            state = MovementState.down;
        }
        else{
            
            if (lastMovement.y >0 ){
                state = MovementState.upidle;
            }
            else if (lastMovement.y <0){
                state = MovementState.downidle;
            }
            else if (lastMovement.x >0){
                state = MovementState.rightidle;
            }
            else if (lastMovement.x <0){
                state = MovementState.leftidle;
            }
            lastMovement =Vector2.zero;
            
        }
        rb.MovePosition(rb.position + lastMovement * playerSpeed * Time.fixedDeltaTime);

        anim.SetInteger("state", (int)state);
    
    }
    
    public void powerUp()
    {
        if (playerPower < playerPowerMax)
        {
            playerPower++;
        }
    }

    public void speedUp()
    {
        if (playerSpeed < playerSpeedMax)
        {
            playerSpeed++;
        }
    }

    public void maxPowerUp()
    {
        playerPower = playerPowerMax;
        
    }

}
