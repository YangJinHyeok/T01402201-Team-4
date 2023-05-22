using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEditor;
using UnityEngine;

public class BoxPositionData : Editor
{
    [MenuItem("Assets/XML Creation/Box Data")]
    public static void CreatDefaultOne()
    {
        string tFile;

        if (Selection.activeObject != null)
        {
            tFile = AssetDatabase.GetAssetPath(Selection.activeObject);

            if (Directory.Exists(tFile))
                tFile = Path.Combine(tFile, "BoxData.xml");
            else if (File.Exists(tFile))
                tFile = Path.Combine(Path.GetDirectoryName(tFile), "BoxData.xml");
        }
        else
            tFile = "Assets/Box Data.xml";

        tFile = AssetDatabase.GenerateUniqueAssetPath(tFile);

        XmlWriter tWriter = XmlWriter.Create(tFile);
        tWriter.WriteElementString("BoxData", "");
        tWriter.Close();

        AssetDatabase.ImportAsset(tFile);
    }
}


