using System.IO;
using UnityEditor;
#if UNITY_EDITOR
using UnityEngine;
#endif

public class FileWriter
{
    public static void WriteBeatMap(string name, string contents)
    {
        string folderPath = "Assets/BeatMaps";

        using (StreamWriter sw = File.CreateText(folderPath + $"/{name}.txt"))
        {
            sw.WriteLine(contents);
        }
    }

    #if UNITY_EDITOR
    [MenuItem("Assets/Create/BeatMap/Create New BeatMap File", priority = 100)]
    public static void WriteFile()
    {
        string folderGUID = Selection.assetGUIDs[0];
        string projectFolderPath = AssetDatabase.GUIDToAssetPath(folderGUID);
        string folderDirectory = Path.GetFullPath(projectFolderPath);
        using (StreamWriter sw = File.CreateText(folderDirectory + "/NewTextFile.txt"))
        {
            sw.WriteLine("This is a new text file!");
        }

        AssetDatabase.Refresh();
    }
    #endif
}
