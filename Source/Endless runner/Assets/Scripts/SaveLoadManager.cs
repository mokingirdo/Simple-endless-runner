using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoadManager : MonoBehaviour
{
    string filePath;

    public GameManager GM;

    public static SaveLoadManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        GM = FindObjectOfType<GameManager>();
        filePath = Application.persistentDataPath + "/save.runnerrecord";

        SaveGame();
        LoadGame();
    }

    public void SaveGame()
    {
        if (GM.CurrentHightScore > GM.HightScore)
        {
            BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filePath, FileMode.Create);

        Save save = new Save();
            save.hightScore = GM.CurrentHightScore;
            bf.Serialize(fs, save);
        
        fs.Close();
        }
    }

    public void LoadGame()
    {
        if(!File.Exists(filePath))
        {
            GM.HightScore = 0;
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filePath, FileMode.Open);

        Save save = (Save)bf.Deserialize(fs);
        fs.Close();

        GM.HightScore = save.hightScore;
        GM.RefreshText();
    }

}

[System.Serializable]
public class Save
{
    public float hightScore;

}
