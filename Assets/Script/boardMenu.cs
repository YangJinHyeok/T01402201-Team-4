using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class boardMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] boards;

    [SerializeField] private string nowBoard;

    private Text[] texts;
    private CSVBoard csvBoard;

    public void OnEnable()
    {
        csvBoard = CSVBoard.Instance;
        csvBoard.setName(nowBoard);
        texts = new Text[boards.Length];
        for (int i = 0; i < boards.Length; i++)
        {
            texts[i] = boards[i].GetComponentInChildren<Text>();
        }
        List<string[]> currentBoard = csvBoard.readBoard();

        if (currentBoard == null)
        {
            for (int i = 0; i < 6; i++)
            {
                texts[i].text = "xxxxxx : 0000000000";
            }
        }
        else
        {
            for (int i = 0; i < currentBoard.Count(); i++)
            {
                Debug.Log(currentBoard[i][0] + " - " + currentBoard[i][1]);
                texts[i].text = currentBoard[i][0] + string.Format(" : {0000000000}", int.Parse(currentBoard[i][1]));
            }

            for (int i = currentBoard.Count(); i < 6; i++)
            {
                string mono = "xxxxxx : 0000000000";
                texts[i].text = mono;
            }
        }
    }
}