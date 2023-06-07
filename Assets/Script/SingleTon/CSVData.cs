using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class CSVData
{
    private static CSVData instance;

    public static CSVData Instance
    {
        get
        {
            if (null == instance)
            {
                instance = new CSVData();
            }

            return instance;
        }
    }
    
    private string fileName = "Data";

    List<Dictionary<string, object>> dicList = new List<Dictionary<string, object>>();
    
    public string[] readData()
    {
        dicList.Clear();
        dicList = CSVReader.Read(fileName);
        if (dicList == null)
        {
            makeData();
            dicList = CSVReader.Read(fileName);
        }
        
        string[] result = new string[7];
        result[0] = dicList[0]["speed"].ToString();
        result[1] = dicList[0]["power"].ToString();
        result[2] = dicList[0]["count"].ToString();
        result[3] = dicList[0]["speedMax"].ToString();
        result[4] = dicList[0]["powerMax"].ToString();
        result[5] = dicList[0]["countMax"].ToString();
        result[6] = dicList[0]["lucci"].ToString();

        return result;
    }
    
    
    public string fileNameToSave = "Data.csv"; //write
    string[] tempData;
    private StringBuilder sb;

    public void saveMaxAndStatus(int speedMax, int powerMax, int countMax, int lucci)
    {
        string filepath = SystemPath.GetPath();
        string delimiter = ",";
        sb = new StringBuilder();
        sb.Clear();
        
        string[] col = new String[7];
        col[0] = "speed";
        col[1] = "power";
        col[2] = "count";
        col[3] = "speedMax";
        col[4] = "powerMax";
        col[5] = "countMax";
        col[6] = "lucci";
        string[][] firstOut = new string[1][];
        firstOut[0] = col;
        sb.AppendLine(string.Join(delimiter, firstOut[0]));

        tempData = new String[4];
        tempData[0] = speedMax.ToString();
        tempData[1] = powerMax.ToString();
        tempData[2] = countMax.ToString();
        tempData[3] = lucci.ToString();
        
        string[] data = readData();
        data[3] = tempData[0];
        data[4] = tempData[1];
        data[5] = tempData[2];
        data[6] = tempData[3];

        int length = data.Length; 
        sb.AppendLine(string.Join(delimiter, data));

        StreamWriter outStream = System.IO.File.CreateText(filepath + fileNameToSave);
        outStream.Write(sb);
        outStream.Close();
    }

    public void makeData()
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
        tempData = new String[7];
        tempData[0] = "speed";
        tempData[1] = "power";
        tempData[2] = "count";
        tempData[3] = "speedMax";
        tempData[4] = "powerMax";
        tempData[5] = "countMax";
        tempData[6] = "lucci";
        column.Add(tempData);

        tempData = new String[7];
        tempData[0] = "2";
        tempData[1] = "1";
        tempData[2] = "1";
        tempData[3] = "6";
        tempData[4] = "8";
        tempData[5] = "6";
        tempData[6] = "0";
        column.Add(tempData);
        
        string[][] firstOut = new string[2][];
        firstOut[0] = column[0];
        firstOut[1] = column[1];
            
        sb.AppendLine(string.Join(delimiter, firstOut[0]));
        sb.AppendLine(string.Join(delimiter, firstOut[1]));

        StreamWriter newOutStream = System.IO.File.CreateText(filepath + fileNameToSave);
        newOutStream.Write(sb);
        newOutStream.Close();
    }
}
