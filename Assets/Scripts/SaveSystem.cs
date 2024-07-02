using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SavePlayer(InventoryManager inventoryManager, Character character)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "playerData.save";
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            Debug.Log(path);
            PlayerData data = new PlayerData(inventoryManager, character);

            formatter.Serialize(stream, data);

        }
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "playerData.save";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                return data;

            }

        }
        else
        {
            Debug.Log("Save file not found in " + path);
            
            return null;
        }
    }
}
