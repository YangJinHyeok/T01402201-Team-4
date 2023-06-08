using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BoardScript : MonoBehaviour
{
    [SerializeField] private GameObject[] boards;

    [SerializeField] private InputField inputField;

    [SerializeField] private Button inputButton;
    //[SerializeField] private GameObject board1;

    private Text[] texts;
    private CSVBoard csvBoard;
    private string inputName;
    private int currentScore;

    public void endRoutine(int score)
    {
        currentScore = score;
        texts = new Text[boards.Length];
        for (int i = 0; i < boards.Length; i++)
        {
            texts[i] = boards[i].GetComponentInChildren<Text>();
        }
        csvBoard = CSVBoard.Instance;
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

        inputName = "player1";
        inputField.onEndEdit.AddListener(inputNameEnd); 
    }

    private void inputNameEnd(string nick)
    {
        if (nick.Length == 0)
        {
            inputName = "player1";
        }
        else
        {
            inputName = nick;
        }
    }
    public void inputPlayer()
    {
        inputButton.interactable = false;
        StartCoroutine(reBoard());
    } 
    

    IEnumerator reBoard()
    {
        yield return new WaitForSecondsRealtime(2f);
        string[][] nowBoard = csvBoard.saveBoard(inputName, currentScore);
        
        for (int i = 0; i < nowBoard.Count(); i++)
        {
            Vector3 eulerAngle = new Vector3(3f, 0f, 0f);

            for (int j = 0; j < 60; j++)
            {
                boards[i].transform.Rotate(eulerAngle, Space.Self);
                boards[i].transform.localRotation *= Quaternion.Euler(eulerAngle);
                if (j == 29)
                {
                    texts[i].text = nowBoard[i][0] + string.Format(" : {0000000000}", int.Parse(nowBoard[i][1]));       
                }
                
                yield return new WaitForSecondsRealtime(0.01f);
            }
        }

        for (int i = nowBoard.Count(); i < 6; i++)
        {
            string mono = "xxxxxx : 0000000000";
            texts[i].text = mono;
        }
    }
}
