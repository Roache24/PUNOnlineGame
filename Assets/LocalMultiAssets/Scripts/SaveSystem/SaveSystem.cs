using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;
    string filePath;

    private void Awake()
    {
        filePath = Application.persistentDataPath + "/save.data";

        #region singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
       #endregion
    }

    public void SaveGame(GameData saveData)
    {
        FileStream dataStream = new FileStream(filePath, FileMode.Create);

        BinaryFormatter converter = new BinaryFormatter();
        converter.Serialize(dataStream, saveData);

        dataStream.Close();

    }

    public GameData LoadGame()
    {
        if (File.Exists(filePath))
        {
            //file exists load it
            FileStream dataStream = new FileStream(filePath, FileMode.Open);
            BinaryFormatter converter = new BinaryFormatter();
            GameData saveData = converter.Deserialize(dataStream) as GameData;
            dataStream.Close();
            return saveData;
        }
        else
        {
            return GameBoss.instance.saveData;

        }
    }
}
