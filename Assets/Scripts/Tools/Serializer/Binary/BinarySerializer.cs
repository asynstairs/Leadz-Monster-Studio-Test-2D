using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class BinarySerializer
{
    public static string FilePath { get; set; } = Application.streamingAssetsPath + "/LevelData";
    
    public static void Serialize(LevelData data)
    {
        BinaryFormatter formatter = new();
        using FileStream stream = File.Open(FilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
        formatter.Serialize(stream, data);
    }

    public static LevelData Deserialize()
    {
        LevelData data = new();

        BinaryFormatter formatter = new();

        using (FileStream stream = File.Open(FilePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
        {
            if (stream.Length > 0)
            {
                data = formatter.Deserialize(stream) as LevelData;
            }
            
        }
        
        return data;
    }
}
