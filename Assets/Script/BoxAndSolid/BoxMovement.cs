using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BoxMovement : MonoBehaviour
{
    private bool isColliding = false;
    [SerializeField] private float forcedTime = 0.4f;
    [SerializeField] private float boxMoveSpeed = 0.015f;
    private float timer = 0.0f;
    private bool isPlayer = false;
    private bool doMove = false;
    private Vector3 pushDirection;
    private Vector3 currentPosition;
    private void Update()
    {
        if (isColliding)
        {
            bool isPressing = isMatchWithKey(pushDirection);
            Collider2D[] colliders = 
                Physics2D.OverlapCircleAll(
                    new Vector2(transform.position.x + pushDirection.x, transform.position.y + pushDirection.y),
                    0.1f);
            
            if (isPlayer && isPressing && !doMove && (colliders.Length < 1))
            {
                if (timer > forcedTime)
                {
                    timer = 0.0f;
                    doMove = true;
                }
                else
                {
                    timer += Time.deltaTime;
                }
            }
            else
            {
                timer = 0.0f;
            }
        }

        if (doMove)
        {
            isColliding = false;
            Vector3 roundPosition = new Vector3(Mathf.Round(currentPosition.x + pushDirection.x), 
                Mathf.Round(currentPosition.y + pushDirection.y), 0);
            transform.position =
                Vector3.MoveTowards(transform.position, roundPosition, boxMoveSpeed);
            if (roundPosition == transform.position)
            {
                doMove = false;
                isColliding = true;
                currentPosition = transform.position;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayer = true;
            currentPosition = transform.position;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !doMove)
        {
            isColliding = true;
            float distance = Vector3.Distance(other.transform.position, transform.position);
            Vector2 contactPoint = other.GetContact(0).point;
            Vector3 contactPosition = new Vector3(contactPoint.x, contactPoint.y, 0);
            Vector3 direction = (transform.position - contactPosition).normalized;
            
            if (distance < 1.0f)
            {
                if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
                {
                    pushDirection = new Vector3(0, direction.y, 0).normalized;
                }
                else if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {
                    pushDirection = new Vector3(direction.x, 0, 0).normalized;
                }
                isColliding = true;
            }
            else
            {
                timer = 0.0f;
                isColliding = false;                
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        timer = 0.0f;
        isPlayer = false;
        isColliding = false;
    }
    
    private bool isMatchWithKey(Vector3 direction)
    {
        if (Input.GetKey(KeyCode.UpArrow) && direction.Equals(new Vector3(0, 1, 0)))
        {
            return true;
        }
        if (Input.GetKey(KeyCode.DownArrow) && direction.Equals(new Vector3(0, -1, 0)))
        {
            return true;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && direction.Equals(new Vector3(-1, 0, 0)))
        {
            return true;
        }
        if (Input.GetKey(KeyCode.RightArrow) && direction.Equals(new Vector3(1, 0, 0)))
        {
            return true;
        }
        return false;
    }
}
