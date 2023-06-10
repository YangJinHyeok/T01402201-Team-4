using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource MySfx2;

    private void Update()
    {
        sound();
    }
    private void sound()
    {
        if (GameManager.instance.statusGame == 21)
        {
            MySfx2.Stop();
        }
    }
    
}
