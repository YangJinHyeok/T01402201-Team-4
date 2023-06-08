using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button startUsers;
    [SerializeField] private Button editUsers;
    // Start is called before the first frame update
    void Start()
    {
        Character.Instance.getFromCSV();
        if (Character.Instance.getSpeedMax() == 6 && 
            Character.Instance.getPowerMax() == 8 &&
            Character.Instance.getCountMax() == 6)
        {
            startUsers.interactable = true;
            editUsers.interactable = true;
        }

    }
}
