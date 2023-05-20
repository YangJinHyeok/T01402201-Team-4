using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;


public class BoxMovement : MonoBehaviour
{
    private bool isColliding = false;
    private bool isRound = true;
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
            if (isPlayer && isPressing && !doMove)
            {
                if (timer > forcedTime)
                {
                    timer = 0.0f;
                    doMove = true;
                }
                timer += Time.deltaTime;
            }

            if (!isPressing)
            {
                timer = 0.0f;
            }
                
        }

        if (doMove)
        {
            isColliding = false;
            transform.position =
                Vector3.MoveTowards(transform.position, currentPosition + pushDirection, boxMoveSpeed);
            if (currentPosition + pushDirection == transform.position)
            {
                doMove = false;
                isColliding = true;
                currentPosition = transform.position;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        isRound = false;
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayer = true;
            currentPosition = transform.position;
            Vector2 direction = (other.transform.position - transform.position).normalized;
            Ray2D ray = new Ray2D(new Vector2(transform.position.x,transform.position.y), direction);
            RaycastHit2D raycastHit = 
                Physics2D.Raycast(ray.origin, ray.direction, 0.01f, LayerMask.GetMask("Box"));
            if (raycastHit.collider != null)
            {
                direction = raycastHit.normal;
                Vector3 direction3D = raycastHit.transform.TransformDirection(direction.x, direction.y, 0);

                if (Mathf.Abs(direction3D.x) < Mathf.Abs(direction3D.y))
                {
                    pushDirection = new Vector3(0, direction3D.y, 0).normalized;
                }
                else if (Mathf.Abs(direction3D.x) > Mathf.Abs(direction3D.y))
                {
                    pushDirection = new Vector3(direction3D.x, 0, 0).normalized;
                }
                Debug.Log("Hit : " + raycastHit.collider.name + ", pushDirection : " + pushDirection);
                isColliding = true;
            }
            
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("Exit");
        timer = 0.0f;
        isPlayer = false;
        isColliding = false;
        isRound = true;
    }

    private Vector3 setRound(Vector3 position)
    {
        return new Vector3(Mathf.Round(position.x), Mathf.Round(position.y), Mathf.Round(position.z));
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
