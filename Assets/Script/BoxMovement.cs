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
    public float forcedTime = 1.5f;
    private float timer = 0.0f;
    private bool isPlayer = false;
    private Vector3 pushDirection;
    private void Update()
    {
        if (isColliding)
        {
            if (isPlayer && isMatchWithKey(pushDirection))
            {
                if (timer > forcedTime)
                {
                    timer = 0.0f;
                    transform.position = moveBoxbyCharacter(transform.position, pushDirection);
                }

                timer += Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        isRound = false;
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayer = true;
            Vector2 direction = (other.transform.position - transform.position).normalized;
            Debug.Log("direction : " + direction);
            Ray2D ray = new Ray2D(new Vector2(transform.position.x,transform.position.y), direction);
            Debug.Log("ray : " + ray);
            RaycastHit2D raycastHit = 
                Physics2D.Raycast(ray.origin, ray.direction, 0.01f, LayerMask.GetMask("Box"));
            if (raycastHit.collider != null)
            {
                direction = raycastHit.normal;
                Debug.Log("direction : " + direction);
                Vector3 direction3D = raycastHit.transform.TransformDirection(direction.x, direction.y, 0);
                Debug.Log("direction3D : " + direction3D);

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
    
    [SerializeField] private float boxMoveSpeed = 0.5f; 
    
    private Vector3 moveBoxbyCharacter(Vector3 original, Vector3 direction)
    {
        return Vector3.MoveTowards(original, original + direction, boxMoveSpeed);
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
