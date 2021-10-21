using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MenuDataPersistence : MonoBehaviour
{
    // Start is called before the first frame update

    public static MenuDataPersistence instance;
    public string playerName, currentPlayer;
    public int highPoint;

    public int rowNo, coloumNo;

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(instance);

        LoadName();
    }

    [System.Serializable]
    class PlayerName
    {
        public string player;
        public int hScore;
        public int row, col;
    }

    public void SaveName()
    {
        PlayerName data = new PlayerName();
        data.player = playerName;
        data.hScore = MainDataPersistence.instance.highScore;
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void SaveTiles()
    {
        PlayerName data1 = new PlayerName();
        data1.row = rowNo;
        data1.col = coloumNo;

        string json1 = JsonUtility.ToJson(data1);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json1", json1);
    }

    public void LoadName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerName data = JsonUtility.FromJson<PlayerName>(json);

            playerName = data.player;
            highPoint = data.hScore;

        }

        string path2 = Application.persistentDataPath + "/savefile.json1";
        if (File.Exists(path2))
        {
            string json = File.ReadAllText(path2);
            PlayerName data = JsonUtility.FromJson<PlayerName>(json);

            rowNo = data.row;
            coloumNo = data.col;
        }
    }


}
