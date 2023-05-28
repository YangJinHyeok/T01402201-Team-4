using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEffects : MonoBehaviour
{
    [SerializeField] private GameObject UIController;
    
    public void destroyBox()
    {
        UIController.GetComponent<GameUIController>().updateScoreWIthValue(30);
    }

    public void itemSpawn()
    {
        
    }

    public void teleport()
    {
        
    }
}
