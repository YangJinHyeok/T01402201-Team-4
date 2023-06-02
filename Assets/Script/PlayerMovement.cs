using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private float playerSpeed;
    private Vector2 lastMovement = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        playerSpeed = Character.Instance.speed;

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
    

}
