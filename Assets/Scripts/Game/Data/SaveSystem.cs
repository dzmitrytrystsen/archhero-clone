using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveGameData(int level)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        string saveDataPath = Application.persistentDataPath + "/SaveData.save";

        FileStream fileStream = new FileStream(saveDataPath, FileMode.Create);

        GameData gameData = new GameData(level);

        binaryFormatter.Serialize(fileStream, gameData);

        fileStream.Close();
    }

    public static GameData LoadGameData()
    {
        string saveDataPath = Application.persistentDataPath + "/SaveData.save";

        if (File.Exists(saveDataPath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(saveDataPath, FileMode.Open);

            GameData gameData = binaryFormatter.Deserialize(fileStream) as GameData;
            fileStream.Close();

            return gameData;
        }
        else
        {
            SaveGameData(0);
            return LoadGameData();
        }
    }
}
