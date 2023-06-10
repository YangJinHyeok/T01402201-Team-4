using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class BombController : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    private int playerCount;
    private int playerRemaining;
    private int playerCountMax;
    private int playerPower;
    private int playerPowerMax;
    private PlayerMovement playerMovement;
    public AudioSource mySfx3;

    public AudioClip setsFx;

    private void Start()
    {
        playerCount = Character.Instance.getCount();
        playerRemaining = playerCount;
        playerCountMax = Character.Instance.getCountMax();
        playerPower = Character.Instance.getPower();
        playerPowerMax = Character.Instance.getPowerMax();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
    
    private void Update()
    {
        if (GameManager.instance.statusGame == 10)
        {
            if (playerRemaining > 0 && Input.GetKeyDown(KeyCode.Space) && playerMovement.isTrapTriggered)
            {
                Vector2 position = GameObject.Find("Player").transform.position;
                position.x = Mathf.Round(position.x);
                position.y = Mathf.Round(position.y);

                Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.1f);
                bool isBombPresent = false;
                foreach (Collider2D collider in colliders)
                {
                    if (collider.CompareTag("Bomb"))
                    {
                        isBombPresent = true;
                        break;
                    }
                }

                if (!isBombPresent)
                {
                    playerRemaining--;
                    Instantiate(bombPrefab, position, Quaternion.identity);
                    setSound();
                }

            }
        }
        else
        {
            return;
        }

    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Bomb"))
        {
            other.isTrigger = false;
        }
    }

    public void countUp()
    {
        if (playerCount < playerCountMax)
        {
            playerCount++;
            playerRemaining++;
        }
    }

    public void remainUp()
    {
        if (playerRemaining < playerCount)
        {
            playerRemaining++;
        }
        else
        {
            //동시에 폭탄 여러개 터지면 remain값이 한계 넘어갈 수 잇음
            playerRemaining = playerCount;
        }
    }
    
    public void powerUp()
    {
        if (playerPower < playerPowerMax)
        {
            playerPower++;
        }
    }
    public void maxPowerUp()
    {
        playerPower = playerPowerMax;
        
    }

    public int getPlayerPower()
    {
        return playerPower;
    }

    public void setSound()
    {
        mySfx3.PlayOneShot(setsFx);
    }
}
