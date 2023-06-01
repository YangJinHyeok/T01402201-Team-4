using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class CSVBoard
{
    private static CSVBoard instance;

    public static CSVBoard Instance
    {
        get
        {
            if (null == instance)
            {
                instance = new CSVBoard();
            }

            return instance;
        }
    }
    
    private string fileName = "Board";

    List<Dictionary<string, object>> dicList = new List<Dictionary<string, object>>();
    
    public List<string[]> readBoard()
    {
        dicList.Clear();
        dicList = CSVReader.Read(fileName);
        if (dicList == null)
        {
            makeBoard();
            dicList = CSVReader.Read(fileName);
        }
        
        List<string[]> result = new List<string[]>();
        result.Clear();
        for (int i = 0; i < dicList.Count(); i++)
        {
            string[] data = new string[2];
            data[0] = dicList[i]["playerName"].ToString();
            data[1] = dicList[i]["score"].ToString();
            result.Add(data);
        }

        return result;
    }
    
    
    public string fileNameToSave = "Board.csv"; //write
    string[] tempData;
    private StringBuilder sb;

    public string[][] saveBoard(string player, int newScore)
    {
        string filepath = SystemPath.GetPath();
        string delimiter = ",";
        sb = new StringBuilder();
        sb.Clear();
        
        string[] col = new String[2];
        col[0] = "playerName";
        col[1] = "score";
        string[][] firstOut = new string[1][];
        firstOut[0] = col;
        sb.AppendLine(string.Join(delimiter, firstOut[0]));

        tempData = new String[2];
        tempData[0] = player;
        tempData[1] = newScore.ToString();
        
        List<string[]> data = readBoard();
        data.Add(tempData);
        
        List<String[]> sortedData = data.OrderByDescending(tem => int.Parse(tem[1])).ToList();

        if (sortedData.Count > 6)
        {
            sortedData.RemoveRange(6,sortedData.Count-6);
        }

        string[][] output = new string[sortedData.Count][];
        
        for (int i = 0; i < sortedData.Count; i++)
        {
            output[i] = sortedData[i];
        }

        int length = output.GetLength(0);
        for (int i = 0; i < length; i++)
        {
            sb.AppendLine(string.Join(delimiter, output[i]));
        }

        StreamWriter outStream = System.IO.File.CreateText(filepath + fileNameToSave);
        outStream.Write(sb);
        outStream.Close();

        return output;

    }

    public void makeBoard()
    {
        string filepath = SystemPath.GetPath();
        string delimiter = ",";

        if (!Directory.Exists(filepath))
        {
            Directory.CreateDirectory(filepath);
        }

        List<string[]> column = new List<string[]>();
        sb = new StringBuilder();
        column.Clear();
        tempData = new String[2];
        tempData[0] = "playerName";
        tempData[1] = "score";
        column.Add(tempData);
        string[][] firstOut = new string[1][];
        firstOut[0] = column[0];
            
        sb.AppendLine(string.Join(delimiter, firstOut[1]));

        StreamWriter newOutStream = System.IO.File.CreateText(filepath + fileNameToSave);
        newOutStream.Write(sb);
        newOutStream.Close();
    }
}
