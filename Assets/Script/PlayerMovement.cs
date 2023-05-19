using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private float Playerspeed = 5f;
    private GameObject Bazzi;
    private GameObject prefab_obj;
    // Start is called before the first frame update
    void Start()
    {
        Bazzi = GameObject.FindGameObjectWithTag("Bazzi");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        prefab_obj = Resources.Load("Prefabs/Bomb") as GameObject;
    }

    // Update is called once per frame
    private void Update()
    {
        PlayerMove();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject obj = MonoBehaviour.Instantiate(prefab_obj);
            obj.AddComponent<BoxCollider2D>();
            obj.name = "clone";
            Vector2 pos = Bazzi.transform.position;
            obj.transform.position = pos;
        }

    }

    private enum MovementState { down, right, up, left, downidle, rightidle, upidle, leftidle };
    private void PlayerMove()
    {

        MovementState state = MovementState.downidle;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector2(Playerspeed * Time.deltaTime, 0));
            state = MovementState.right;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector2(-Playerspeed * Time.deltaTime, 0));
            state = MovementState.left;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector2(0, Playerspeed * Time.deltaTime));
            state = MovementState.up;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector2(0, -Playerspeed * Time.deltaTime));
            state = MovementState.down;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            state = MovementState.rightidle;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            state = MovementState.leftidle;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            state = MovementState.upidle;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            state = MovementState.downidle;
        }

        anim.SetInteger("state", (int)state);
    }


}
