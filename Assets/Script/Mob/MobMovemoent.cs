using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovemoent : MonoBehaviour
{
    private Rigidbody2D mobRigidBody;
    public Collider2D footCollider;
    public float moveSpeed;
    public float jumpForce;
    
    // Start is called before the first frame update
    void Start()
    {
        mobRigidBody = GetComponent<Rigidbody2D>();        
    }

    private void FixedUpdate()
    {
        Move();
    }
    // Update is called once per frame
    private void Move()
    {
        mobRigidBody.AddForce(Vector2.right * moveSpeed, ForceMode2D.Force);
        
        GetComponent<Animator>().SetBool("IsMove", true);
        GetComponent<SpriteRenderer>().flipX = false;
    }
    void Update()
    {
        
    }

}
