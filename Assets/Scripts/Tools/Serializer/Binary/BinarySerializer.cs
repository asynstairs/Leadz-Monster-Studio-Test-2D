using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine;

public static class BinarySerializer
{
    public static string FilePath { get; set; } = Application.streamingAssetsPath + "/LevelData";
    
    public static async Task SerializeAsync(LevelData data)
    {
        Debug.Log($"Starting serialization of {data}");
        
        BinaryFormatter formatter = new();
        
        await Task.Run(() =>
        {
            using FileStream stream = File.Open(FilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
            formatter.Serialize(stream, data);
        });
        
        Debug.Log($"Successfully serialized {data}");
    }

    public static LevelData Deserialize()
    {
        Debug.Log($"Starting deserialization");
        
        LevelData data = new();
        
        BinaryFormatter formatter = new();

        using (FileStream stream = File.Open(FilePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
        {
            data = formatter.Deserialize(stream) as LevelData;
        }

        Debug.Log($"Successfully deserialized {data}");
        return data;
    }
}
